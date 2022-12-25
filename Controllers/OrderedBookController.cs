using System.Security.Claims;
using FPTBook.Data;
using FPTBook.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FPTBook.Controllers;

[Authorize]
[AutoValidateAntiforgeryToken]
public class OrderedBookController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    

    public OrderedBookController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var orderedBook = await _context.OrderedBooks
             .Where(u => u.UserId == userId).Include(x => x.Book).ToListAsync();

        return View(orderedBook);
    }
    [HttpGet]
    [HttpPost]
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Create(int bookId, string userId)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("Index", "Book");
        }
        var existingBook = await _context.OrderedBooks.FirstOrDefaultAsync(x => x.BookId == bookId && x.UserId == userId && x.IsOrdered == false);
        if (existingBook == null)
        {
            OrderedBook orderedBook = new OrderedBook()
            { 
                UserId = userId,
                BookId = bookId,
                Quantity = 1,
                IsOrdered = false
            };
            var saveBook = await _context.OrderedBooks.AddAsync(orderedBook);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Book");
        }
        existingBook.Quantity += 1;
        await _context.SaveChangesAsync();
        return RedirectToAction("Index", "Book");
    }

    [HttpGet]
    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var orderedBook = await _context.OrderedBooks.FindAsync(id);
        var deleteBook = _context.OrderedBooks.Remove(orderedBook);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
    
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> IncreaseQuantity(int id)
	{
		var cartBookInDb = await _context.OrderedBooks.SingleOrDefaultAsync(t => t.Id==id);
		if (cartBookInDb == null)
		{
			return NotFound();
		}

		cartBookInDb.Quantity ++;
		await _context.SaveChangesAsync();
		return RedirectToAction("Index");
	}

    [AutoValidateAntiforgeryToken]
	public async Task<IActionResult> DecreaseQuantity(int id)
	{
		var cartBookInDb = await _context.OrderedBooks.SingleOrDefaultAsync(t => t.Id == id);
		if (cartBookInDb == null)
		{
			return NotFound();
		}
		if (cartBookInDb.Quantity > 1)
		{
			cartBookInDb.Quantity--;
		}else 
		{
			_context.OrderedBooks.Remove(cartBookInDb);
		}
		
		await _context.SaveChangesAsync();
		return RedirectToAction("Index");
	}
}