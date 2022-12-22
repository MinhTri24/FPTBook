using FPTBook.Data;
using FPTBook.Models;
using FPTBook.ViewModels;
using FPTBook.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FPTBook.Controllers;
[Authorize]
public class UserController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
    
    public UserController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
    {
        _db = db;
        _userManager = userManager;
    }
    
    /*[AllowAnonymous]*/
    /*[Authorize(Roles = "Owner")]*/
    [HttpGet]
    public IActionResult Index()
    {
	    var users = from e in _db.ApplicationUsers select e;
	    UsersDetail usersDetail = new UsersDetail()
	    {
		    Users = users.ToList()
	    };
	    return View(usersDetail);
    }

    public IActionResult ViewCustomer()
    {
	    var user = (from u in _db.ApplicationUsers
		    join userRole in _db.UserRoles on u.Id equals userRole.UserId
		    join role in _db.Roles on userRole.RoleId equals role.Id
		    where role.Name == "CUSTOMER"
		    select u).ToList();
	    return View(user);
    }
    
    public IActionResult ViewOwner()
    {
	    var user = (from u in _db.ApplicationUsers
		    join userRole in _db.UserRoles on u.Id equals userRole.UserId
		    join role in _db.Roles on userRole.RoleId equals role.Id
		    where role.Name == "OWNER"
		    select u).ToList();
	    return View(user);
    }
    
    [HttpPost]
    public IActionResult Index(string searchString)
    {
	    var users = from e in _db.ApplicationUsers select e;
	    if (!string.IsNullOrEmpty(searchString))
	    {
		    users = users.Where(s => s.Email.Contains(searchString));
	    }

	    UsersDetail usersDetail = new UsersDetail()
	    {
		    Users = users.ToList()
	    };
        
	    return View(usersDetail);
    }

	[HttpGet] 
	public IActionResult ChangeOwnerPassword(string id)
	{
		var userInDb = _db.ApplicationUsers.FirstOrDefault(t => t.Id == id);

		if (_userManager.IsInRoleAsync(userInDb, Role.Owner) != null)
		{
			UpdatePassword newForm = new UpdatePassword(){
				Id = userInDb.Id,
				Password = userInDb.PasswordHash
			};
			return View(newForm);
		}
		return NotFound();
	}
     
	[HttpPost]
	public IActionResult ChangeOwnerPassword(UpdatePassword user)
	{
		var userInDb = _db.ApplicationUsers.FirstOrDefault(t => t.Id == user.Id);
		
		userInDb.PasswordHash = _userManager.PasswordHasher.HashPassword(userInDb,user.Password);
        var result = _userManager.UpdateAsync(userInDb);
		// _db.SaveChanges();
		return RedirectToAction("ViewOwner");
	}
	
	[HttpGet] 
	public IActionResult ChangeCustomerPassword(string id)
	{
		var userInDb = _db.ApplicationUsers.FirstOrDefault(t => t.Id == id);

		if (_userManager.IsInRoleAsync(userInDb, Role.Customer) != null)
		{
			UpdatePassword newForm = new UpdatePassword(){
				Id = userInDb.Id,
				Password = userInDb.PasswordHash
			};
			return View(newForm);
		}
		return NotFound();
	}
     
	[HttpPost]
	public IActionResult ChangeCustomerPassword(UpdatePassword user)
	{
		var userInDb = _db.ApplicationUsers.FirstOrDefault(t => t.Id == user.Id);
		
		userInDb.PasswordHash = _userManager.PasswordHasher.HashPassword(userInDb,user.Password);
		var result = _userManager.UpdateAsync(userInDb);
		// _db.SaveChanges();
		return RedirectToAction("ViewCustomer");
	}
}