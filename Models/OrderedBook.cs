namespace FPTBook.Models;

public class OrderedBook
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public Book Book { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; }
    public int Quantity { get; set; }
    public decimal Subtotal { get; set; }
}