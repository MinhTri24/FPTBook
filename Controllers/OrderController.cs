using System.Security.Claims;
using FPTBook.Data;
using FPTBook.Models;
using FPTBook.ViewModels;
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

    public IActionResult Index()
    {
        var order = _context.Orders.ToList();
        return View(order);
    }

    /*[HttpGet]
    public IActionResult Detail(int orderId, string userId)
    {
        
        var orderOrderedBooks = _context.OrderOrderedBooks.Where(o => o.OrderId == orderId);
        foreach (var VARIABLE in COLLECTION)
        {
            
        }
        /*var orderedId = from o in _context.OrderOrderedBooks select o.OrderId;
        var order = from o in _context.Orders select o.Id;
        
        

        var ordered = _context.OrderOrderedBooks.Where(o => o.OrderId == orderedId);

        if (ordered == null)
        {
            return Content("Error");
        }

        /*var orders = _context.Orders
            .Include(o => o.OrderedBooks)
            .FirstOrDefault(d => d.Id == id);
        var orderedBook = _context.OrderOrderedBooks
            .Include(o => o.Order)
            .ThenInclude(b => b!.OrderedBooks)
            .FirstOrDefault(d => d.OrderId == id);#2#

        /*if (order != null)
        {
            var orderRelated = _context.OrderOrderedBooks.Where(o => o.OrderId == orderedBook!.OrderId);
            OrderDetail orderDetail = new OrderDetail()
            {
                OrderOrderedBook = orderedBook,
                OrderOrderedBooks = new List<OrderOrderedBook>(orderRelated)
            };
            return View(orderDetail);
        }#2#

        OrderDetail orderDetail = new OrderDetail()
        {
            Order = ordered.ToList()
        };#1#
        return Content("adada");
    }

    
    /*public OrderViewModels GetOrderId(int id)
    {
        var _orders = _context.Orders.Select(order => new OrderViewModels()
        {
            BookTitles = order.O
        })
    }#1#*/

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
        TempData["SUCCESS"] = "Checked out successfully";
        return RedirectToAction("Index", "Home");
    }
}