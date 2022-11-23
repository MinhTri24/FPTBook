using FPTBook.Data;
using FPTBook.Models;
using FPTBook.ViewModels.Category;
using Microsoft.AspNetCore.Mvc;

namespace FPTBook.Controllers;

public class CategoryController : Controller
{
    private ApplicationDbContext _db;

    public CategoryController(ApplicationDbContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        var categories = _db.Categories.ToList();
        return View(categories);
    }

    public IActionResult Detail(string id)
    {
        int categoryId = Int32.Parse(id);
        var categories = _db.Categories.Find(categoryId);

        if (categories == null)
        {
            return Content("This categories doesn't exist");
        }

        return View(categories);
    }

    public IActionResult Delete(string id)
    {
        int categoryId = Int32.Parse(id);
        var categories = _db.Categories.Find(categoryId);

        if (categories != null)
        {
            _db.Categories.Remove(categories);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Create()
    {
        CategoryCreate formCreateCategory = new CategoryCreate();
        return View(formCreateCategory);
    }

    [HttpPost]
    public IActionResult Create(CategoryCreate categoryCreate)
    {
        if (ModelState.IsValid)
        {
            Category newCategory = new Category()
            {
                Name = categoryCreate.Name
            };
            var savedCategory = _db.Categories.Add(newCategory);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        return RedirectToAction("Create");
    }

    [HttpGet]
    public IActionResult Update(string id)
    {
        int categoryId = Int32.Parse(id);
        var categories = _db.Categories.Find(categoryId);

        CategoryUpdate formUpdateCategory = new CategoryUpdate()
        {
            Id = categories.Id,
            Name = categories.Name
        };
        return View(formUpdateCategory);
    }

    [HttpPost]
    public IActionResult Update(CategoryUpdate categoryUpdate)
    {
        var category = _db.Categories.Find(categoryUpdate.Id);
        category.Name = categoryUpdate.Name;
        _db.SaveChanges();
        string cateId = $"{categoryUpdate.Id}";
        return RedirectToAction("Detail", new { id = cateId });
    }
}