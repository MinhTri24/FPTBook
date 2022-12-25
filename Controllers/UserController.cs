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
[AutoValidateAntiforgeryToken]
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
    [AutoValidateAntiforgeryToken]
    public IActionResult Index()
    {
	    var users = from e in _db.ApplicationUsers select e;
	    UsersDetail usersDetail = new UsersDetail()
	    {
		    Users = users.ToList()
	    };
	    return View(usersDetail);
    }

    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> ViewCustomer()
    {
	    var user = await (from u in _db.ApplicationUsers
		    join userRole in _db.UserRoles on u.Id equals userRole.UserId
		    join role in _db.Roles on userRole.RoleId equals role.Id
		    where role.Name == "CUSTOMER"
		    select u).ToListAsync();
	    return View(user);
    }
    
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> ViewOwner()
    {
	    var user = await (from u in _db.ApplicationUsers
		    join userRole in _db.UserRoles on u.Id equals userRole.UserId
		    join role in _db.Roles on userRole.RoleId equals role.Id
		    where role.Name == "OWNER"
		    select u).ToListAsync();
	    return View(user);
    }
    
    [HttpPost]
    [AutoValidateAntiforgeryToken]
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
	[AutoValidateAntiforgeryToken]
	public async Task<IActionResult> ChangeOwnerPassword(string id)
	{
		var userInDb = await _db.ApplicationUsers.FirstOrDefaultAsync(t => t.Id == id);

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
	[AutoValidateAntiforgeryToken]
	public async Task<IActionResult> ChangeOwnerPassword(UpdatePassword user)
	{
		var userInDb = await _db.ApplicationUsers.FirstOrDefaultAsync(t => t.Id == user.Id);
		
		userInDb.PasswordHash = _userManager.PasswordHasher.HashPassword(userInDb,user.Password);
        var result = await _userManager.UpdateAsync(userInDb);
		// _db.SaveChanges();
		return RedirectToAction("ViewOwner");
	}
	
	[HttpGet] 
	[AutoValidateAntiforgeryToken]
	public async Task<IActionResult> ChangeCustomerPassword(string id)
	{
		var userInDb = await _db.ApplicationUsers.FirstOrDefaultAsync(t => t.Id == id);

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
	[AutoValidateAntiforgeryToken]
	public async Task<IActionResult> ChangeCustomerPassword(UpdatePassword user)
	{
		var userInDb = await _db.ApplicationUsers.FirstOrDefaultAsync(t => t.Id == user.Id);
		
		userInDb.PasswordHash = _userManager.PasswordHasher.HashPassword(userInDb,user.Password);
		var result = await _userManager.UpdateAsync(userInDb);
		// _db.SaveChanges();
		return RedirectToAction("ViewCustomer");
	}
}