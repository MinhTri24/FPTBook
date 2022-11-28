using System.Security.Claims;
using FPTBook.Data;
using FPTBook.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FPTBook.Controllers;

public class OrderedBookController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    

    public OrderedBookController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    

    public IActionResult Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var orderedBook = _context.OrderedBooks
             .Where(u => u.UserId == userId).Include(x => x.Book).ToList();

        return View(orderedBook);
    }
    [HttpGet]
    [HttpPost]
    public IActionResult Create(int bookId, string userId)
    {
        var existingBook = _context.OrderedBooks.FirstOrDefault(x => x.BookId == bookId && x.UserId == userId && x.IsOrdered == false);
        if (existingBook == null)
        {
            OrderedBook orderedBook = new OrderedBook()
            { 
                UserId = userId,
                BookId = bookId,
                Quantity = 1,
                IsOrdered = false
            };
            var saveBook = _context.OrderedBooks.Add(orderedBook);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        existingBook.Quantity += 1;
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpGet]
    [HttpPost]
    public IActionResult Delete(int id)
    {
        var orderedBook = _context.OrderedBooks.Find(id);
        var deleteBook = _context.OrderedBooks.Remove(orderedBook);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
}