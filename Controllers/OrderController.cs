using System.Security.Claims;
using FPTBook.Data;
using FPTBook.Models;
using FPTBook.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FPTBook.Controllers;

[Authorize]
[AutoValidateAntiforgeryToken]
public class OrderController : Controller
{
    private readonly ApplicationDbContext _context;
    private UserManager<ApplicationUser> _userManager;

    public OrderController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [AutoValidateAntiforgeryToken]
    public IActionResult Index()
    {
        if (HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
        {
            if (HttpContext.User.IsInRole(Role.Owner))
            {
                var user = from u in _context.ApplicationUsers select u;
                var order = from o in _context.Orders select o;
                OrdersDetail orderDetail = new OrdersDetail()
                {
                    Users = user.ToList(),
                    Orders = order.ToList()
                };
                return View(orderDetail);
            }
        }
        var users = from u in _context.ApplicationUsers select u;
        var orders = from o in _context.Orders select o;
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        OrdersDetail ordersDetail = new OrdersDetail()
        {
            Users = users.ToList().Where(u => u.Id == userId),
            Orders = orders.ToList().Where(o => o.UserId == userId)
        };
        return View(ordersDetail);
    }

    [HttpGet]
    [AutoValidateAntiforgeryToken]
    public IActionResult Detail(int orderId)
    {
        var data = _context.Orders
            .Include(x => x.OrderOrderedBooks)
            .ThenInclude(y => y.OrderedBook)
            .ThenInclude(z => z.Book)
            .Where(o => o.Id == orderId);

        return View(data);
    }

    [HttpGet]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> GoCheckOut(string id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var orderedBook = await _context.OrderedBooks
            .Where(u => u.UserId == userId).Include(x => x.Book).ToListAsync();
        return View(orderedBook);
    }
    
    [HttpGet]
    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> CheckOut(string userId, int totalPrice)
    {
        var orderedBooks = await _context.OrderedBooks
            .Where(u => u.UserId == userId && u.IsOrdered == false).ToListAsync();
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
        var saveOrder = await _context.Orders.AddAsync(newOrder);
        
        await _context.SaveChangesAsync();
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
        await _context.SaveChangesAsync();
        TempData["SUCCESS"] = "Checked out successfully";
        return RedirectToAction("Success");
    }
        
    [HttpGet]
    [AutoValidateAntiforgeryToken]
    public IActionResult Success()
    {
        return View();
    }
}