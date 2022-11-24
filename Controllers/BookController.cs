using FPTBook.Data;
using FPTBook.Models;
using FPTBook.ViewModels.Book;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FPTBook.Controllers;

public class BookController : Controller
{
    private ApplicationDbContext _db;

    public BookController(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public IActionResult Index()
    {
        var books = _db.Books.ToList();
        return View(books);
    }
    
    [HttpGet]
    public IActionResult Detail(int id)
    {
        /*var books = _db.Books.Find(bookId);
        if (books == null)
        {
            return Content("This book doesn't exist");
        }
        return View(books);*/
        var book = _db.Books
            .Include(c => c.Category)
            .FirstOrDefault(d => d.Id == id);
        if (book != null)
        {
            var bookRelated = _db.Books.Where(c => c.CategoryId == book.CategoryId).ToList();
            bookRelated.Remove(book);
            BookDetail bookDetail = new BookDetail()
            {
                Book = book,
                Books = bookRelated
            };
            return View(bookDetail);
        }

        return Content("This book doesn't exist");
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
    public IActionResult Create(int id)
    {
        BookCreate formBookCreate = new BookCreate()
        {
            CategoryId = id
        };
        return View(formBookCreate);
    }
    
    [HttpPost]
    public IActionResult Create(BookCreate bookCreate)
    {
        Category? categoryInDb = _db.Categories.Find(bookCreate.CategoryId);
        if (categoryInDb == null)
        {
            return Content("Error!");
        }

        Book newBook = new Book()
        {
            CategoryId = bookCreate.CategoryId,
            Category = categoryInDb,
            Title = bookCreate.Title,
            Author = bookCreate.Author,
            Image = bookCreate.Image,
            Summary = bookCreate.Summary,
            Price = bookCreate.Price,
            UpdateDate = DateTime.Now
        };
        _db.Books.Add(newBook);
        _db.SaveChanges();
        return RedirectToAction("Detail", "Book", new { id = bookCreate.CategoryId });
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
    }
}