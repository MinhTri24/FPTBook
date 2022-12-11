using FPTBook.Models;

namespace FPTBook.ViewModels;

public class OrdersDetail
{
    public IEnumerable<ApplicationUser> Users { get; set; }
    public IEnumerable<Order> Orders { get; set; }
}