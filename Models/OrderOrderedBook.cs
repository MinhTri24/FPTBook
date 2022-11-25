namespace FPTBook.Models;

public class OrderOrderedBook
{
    public int OrderId { get; set; }
    public Order Order { get; set; }
    
    public int OrderedBookId { get; set; }
    public OrderedBook OrderedBook { get; set; }
}