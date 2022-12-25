using Microsoft.AspNetCore.Mvc.Rendering;

namespace FPTBook.ViewModels;

public class BookCategoryViewModels
{
    public List<Models.Book>? Books { get; set; }
    public SelectList? Categories { get; set; }
    public string? BookCategory { get; set; }
    public string? SearchString { get; set; }
}