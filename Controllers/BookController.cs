using FPTBook.Data;
using FPTBook.Models;
using FPTBook.ViewModels.Book;
using Microsoft.AspNetCore.Mvc;

namespace FPTBook.Controllers;

public class BookController : Controller
{
    private ApplicationDbContext _db;/*

    public BookController(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public IActionResult Index()
    {
        var books = _db.Books.ToList();
        return View(books);
    }
    
    public IActionResult Detail(string id)
    {
        int bookId = Int32.Parse(id);
        var books = _db.Books.Find(bookId);
        if (books == null)
        {
            return Content("This book doesn't exist");
        }
        return View(books);
    }
    
    public IActionResult Delete(string id)
    {
        int bookId = Int32.Parse(id);
        var books = _db.Books.Find(bookId);
        if (books != null)
        {
            _db.Books.Remove(books);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        BookCreate formBookCreate = new BookCreate();
        return View(formBookCreate);
    }
    
    [HttpPost]
    public IActionResult Create(BookCreate bookCreate)
    {
        if (ModelState.IsValid)
        {
            Book newbBook = new Book()
            {
                Title = bookCreate.Title,
                Author = bookCreate.Author,
                Image = bookCreate.Image,
                Summary = bookCreate.Summary,
                Price = bookCreate.Price,
                UpdateDate = DateTime.Now
            };
            var savedBook = _db.Books.Add(newbBook);
            _db.SaveChanges();
            TempData["SUCCESS"] = "Book created successfully";
            return RedirectToAction("Index");
        }
        TempData["ERROR"] = "Book created failed";
        return RedirectToAction("Create");
    }
    
    [HttpGet]
    public IActionResult Update(string id)
    {
        int bookId = Int32.Parse(id);
        var books = _db.Books.Find(bookId);
        BookUpdate formBookUpdate = new BookUpdate()
        {
            Id = books.Id,
            Title = books.Title,
            Author = books.Author,
            Image = books.Image,
            Summary = books.Summary,
            Price = books.Price
        };
        return View(formBookUpdate);
    }

    [HttpPost]
    public IActionResult Update(BookUpdate bookUpdate)
    {
        var book = _db.Books.Find(bookUpdate.Id);
        book.Title = bookUpdate.Title;
        book.Author = bookUpdate.Author;
        book.Image = bookUpdate.Image;
        book.Summary = bookUpdate.Summary;
        book.Price = bookUpdate.Price;
        book.UpdateDate = DateTime.Now;
        _db.SaveChanges();
        string bookId = $"{bookUpdate.Id}";
        return RedirectToAction("Detail", new { id = bookId });
    }*/
}