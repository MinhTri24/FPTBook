
using System.Security.Claims;
using FPTBook.Data;
using FPTBook.Models;
using FPTBook.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FPTBook.Areas.Identity.Pages.Account.Manage;

    public class RecordModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RecordModel> _logger;
        
        public RecordModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ILogger<RecordModel> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }
        
        public List<Order> Orders { get; set; }

        public async Task OnGet()
        {
            // var users = await from u in _context.ApplicationUsers select u;
            var orders = from o in _context.Orders select o;
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            // Users = users.ToList().Where(u => u.Id == userId);
            Orders = await orders.Where(o => o.UserId == userId).ToListAsync();
        }
    }