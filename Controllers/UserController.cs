using FPTBook.Data;
using FPTBook.Models;
using FPTBook.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FPTBook.Controllers;
[Authorize]
public class UserController : Controller
{
    private readonly ApplicationDbContext _db;
    private UserManager<ApplicationUser> _userManager;
    
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
}