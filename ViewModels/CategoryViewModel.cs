using Microsoft.AspNetCore.Mvc.Rendering;

namespace FPTBook.ViewModels;

public class CategoryViewModel
{
    public SelectList? Categories { get; set; }
    public string? Category { get; set; }
}