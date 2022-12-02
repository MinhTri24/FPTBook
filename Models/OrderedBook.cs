using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace FPTBook.Models;

public class OrderedBook
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public Book Book { get; set; }
    [DisplayName("User Id:")]
    [ForeignKey("ApplicationUser")]
    public string UserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
    [DisplayName("Quantity: ")]
    public int Quantity { get; set; }
    [DisplayName("Is Ordered: ")]
    public bool IsOrdered { get; set; }
    /*public virtual ICollection<Order> Orders { get; set; }*/
    public ICollection<OrderOrderedBook> OrderOrderedBooks { get; set; }
}