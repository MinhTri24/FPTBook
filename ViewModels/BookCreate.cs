using Microsoft.AspNetCore.Mvc.Rendering;

namespace FPTBook.ViewModels;

public class BookCreate
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string Image { get; set; }
    public string Summary { get; set; }
    public int Price { get; set; }
    public SelectList? Categories { get; set; }
    public string? Category { get; set; }
}