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
    [HttpGet]
    [HttpPost]
    public IActionResult CheckOut(string userId, int totalPrice)
    {
        var orderedBooks = _context.OrderedBooks
            .Where(u => u.UserId == userId && u.IsOrdered == false).ToList();
        foreach (var orderedBook in orderedBooks)
        {
            orderedBook.IsOrdered = true;
        }

        var newOrder = new Order()
        {
            UserId = userId,
            TotalPrice = totalPrice,
            IsCheckedOut = true
        };
        var saveOrder = _context.Orders.Add(newOrder);
        
        _context.SaveChanges();
        int orderId = newOrder.Id;
        // var order = _context.Orders.Where(o => o.UserId == userId);
        foreach (var orderedBook in orderedBooks)
        {
            var newOrderOrderedBook = new OrderOrderedBook()
            {
                OrderId = orderId,
                OrderedBookId = orderedBook.Id
            };
            var saveOrderOrderedBook = _context.OrderOrderedBooks.Add(newOrderOrderedBook);
        }
        _context.SaveChanges();
        TempData["SUCCESS"] = "Document created successfully";
        return RedirectToAction("GoCheckOut");
    }
}