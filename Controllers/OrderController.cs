using System.Security.Claims;
using FPTBook.Data;
using FPTBook.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FPTBook.Controllers;

public class OrderController : Controller
{
    private readonly ApplicationDbContext _context;
    private UserManager<ApplicationUser> _userManager;

    public OrderController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpGet]
    public IActionResult GoCheckOut(string id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var orderedBook = _context.OrderedBooks
            .Where(u => u.UserId == userId).Include(x => x.Book).ToList();
        return View(orderedBook);
    }

    [HttpPost]
    public IActionResult CheckOut(string id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var orderedBook = _context.OrderedBooks
            .Where(u => u.UserId == userId).Include(x => x.Book).ToList();
        return View();
    }
}