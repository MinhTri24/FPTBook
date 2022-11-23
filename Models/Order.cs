using System.ComponentModel.DataAnnotations.Schema;

namespace FPTBook.Models;

public class Order
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime CreateAt { get; set; }

    public Order()
    {
        CreateAt = DateTime.Now;
    }

    
}