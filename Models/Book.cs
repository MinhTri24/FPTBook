using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FPTBook.Models;

public class Book
{
    public int Id { get; set; }
    [DisplayName("Title")]
    public string Title { get; set; }
    [DisplayName("Author")]
    public string Author { get; set; }
    public string Image { get; set; }
    public string Summary { get; set; }
    [DisplayName("Price")]
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