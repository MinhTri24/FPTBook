using Microsoft.AspNetCore.Mvc.Rendering;

namespace FPTBook.ViewModels.Book;

public class BookUpdate
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Image { get; set; }
    public string Summary { get; set; }
    public int Price { get; set; }
    public SelectList? Categories { get; set; }
    public string? Category { get; set; }
}