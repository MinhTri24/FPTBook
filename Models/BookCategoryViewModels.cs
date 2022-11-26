using Microsoft.AspNetCore.Mvc.Rendering;

namespace FPTBook.Models;

public class BookCategoryViewModels
{
    public List<Book> Books { get; set; }
    public SelectList? Categories { get; set; }
    public string? BookCategory { get; set; }
    public string? SearchString { get; set; }
}