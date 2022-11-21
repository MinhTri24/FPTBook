using System.ComponentModel.DataAnnotations;

namespace FPTBook.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Image { get; set; }
    public string Summary { get; set; }
    public int Price { get; set; }
    public DateTime UpdateDate { get; set; }
    
    [Required]
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    
    public Book()
    {
        UpdateDate = DateTime.Now;
    }
    
    
}