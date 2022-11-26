using FPTBook.Data;
using FPTBook.Models;
using FPTBook.ViewModels.Book;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FPTBook.Controllers;

public class BookController : Controller
{
    private ApplicationDbContext _db;

    public BookController(ApplicationDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    // public IActionResult Index()
    // {
    //     var booksListAsync = _db.Books.Include(c => c.Category).ToList();
    //     var categoriesName = from n in _db.Categories
    //         select n.Name;
    //     SelectList categories = new SelectList(categoriesName);
    //     BookCategoryViewModels bookCategoryViewModels = new BookCategoryViewModels()
    //     {
    //         Books = booksListAsync,
    //         Categoies = categories
    //     };
    //     return View(bookCategoryViewModels);
    // }
    [HttpGet]
    public IActionResult Index()
    {
        var booksListAsync = _db.Books.Include(c => c.Category).ToList();
        var categoriesName = from n in _db.Categories
            select n.Name;
        BookCategoryViewModels bookCategory = new BookCategoryViewModels()
        {
            Books = booksListAsync,
            Categories = new SelectList(categoriesName)
        };
        return View(bookCategory);
    }
    [HttpPost]
    public IActionResult Index(string category,string searchString)
    {
        if (_db.Books == null)
        {
            return Problem("Entity set 'MvcMovieContext.Book'  is null.");
        }
        IQueryable<string> categoryQuery = from m in _db.Books
            orderby m.Category.Name
            select m.Category.Name;
        var books = from b in _db.Books
            select b;
        if (!string.IsNullOrEmpty(searchString))
        {
            books = books.Include(c => c.Category)
                .Where(s => s.Title!.ToLower().Contains(searchString.ToLower()));
        }
        if (!string.IsNullOrEmpty(category))
        {
            books = books.Where(s => s.Category.Name.Equals(category));
        }
        BookCategoryViewModels bookCategoryViewModels = new BookCategoryViewModels()
        {
            Books = books.ToList(),
            Categories = new SelectList(categoryQuery.ToList()),
        };
        return View(bookCategoryViewModels);
        
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