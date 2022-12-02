using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FPTBook.Models;

public class Order
{
    [DisplayName("Order Id:")]
    public int Id { get; set; }
    [DisplayName("User Id:")]
    [ForeignKey("ApplicationUser")]
    public string UserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
    [Required]
    /*public virtual ICollection<OrderedBook> OrderedBooks { get; set; }*/
    public ICollection<OrderOrderedBook> OrderOrderedBooks { get; set; }
    [DisplayName("Total Price:")]
    public decimal TotalPrice { get; set; }
    [DisplayName("Is Checked Out:")]
    public bool IsCheckedOut { get; set; }
    [DisplayName("Created At")]
    public DateTime CreatedAt { get; set; }

    public Order()
    {
        CreatedAt = DateTime.Now;
    }

    
}