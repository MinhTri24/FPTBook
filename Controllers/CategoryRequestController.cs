using FPTBook.Data;
using FPTBook.Models;
using FPTBook.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FPTBook.Controllers;

public class CategoryRequestController : Controller
{
    private ApplicationDbContext _db;

    public CategoryRequestController(ApplicationDbContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        var categories = _db.CategoryRequests.ToList();
        return View(categories);
    }
    
    public IActionResult Reject(int id)
    {
        var categories = _db.CategoryRequests.Find(id);

        if (categories != null)
        {
            _db.CategoryRequests.Remove(categories);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        CategoryRequestCreate formCreateCategory = new CategoryRequestCreate();
        return View(formCreateCategory);
    }

    [HttpPost]
    public IActionResult Create(CategoryRequestCreate categoryCreate)
    {
        if (ModelState.IsValid)
        {
            CategoryRequest newCategory = new CategoryRequest()
            {
                Name = categoryCreate.Name,
                CreatedAt = DateTime.Now,
                Is_Approved = false
            };
            var savedCategory = _db.CategoryRequests.Add(newCategory);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        return RedirectToAction("Create");
    }
    
    [HttpGet]
    [HttpPost]
    public IActionResult Confirm(int id)
    {
        var categories = _db.CategoryRequests.Find(id);
        var category = _db.Categories.FirstOrDefault(c => categories != null && c.Name == categories.Name);
        /*var cate = _db.Categories.OrderByDescending(c => c.Id).FirstOrDefault(c => c.Name == categories.Name);*/
        var cate = _db.Categories.OrderByDescending(c => c.Id).FirstOrDefault();
        int categoryId = cate!.Id;

        if (categories != null && category == null)
        {
            var newCate = new Category()
            {
                Id = categoryId + 1,
                Name = categories.Name
            };
            var saveCate = _db.Categories.Add(newCate);
        }

        var removeRequest = _db.CategoryRequests.Remove(categories);
        _db.SaveChanges();
        return RedirectToAction("Index", "CategoryRequest");
    }
}