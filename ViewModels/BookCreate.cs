﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace FPTBook.ViewModels;

public class BookCreate
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string Image { get; set; }
    public string Summary { get; set; }
    public int Price { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    
    public List<Models.Book> Books { get; set; }
    public SelectList? Categories { get; set; }
    public string? BookCategory { get; set; }
    public string? SearchString { get; set; }
}