using FPTBook.Data;
using FPTBook.Models;
using FPTBook.ViewModels.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FPTBook.Controllers;

[Authorize]
[AutoValidateAntiforgeryToken]
public class CategoryController : Controller
{
    private ApplicationDbContext _db;

    public CategoryController(ApplicationDbContext db)
    {
        _db = db;
    }

    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Index()
    {
        var categories = await _db.Categories.ToListAsync();
        return View(categories);
    }

    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Detail(string id)
    {
        int categoryId = Int32.Parse(id);
        var categories = await _db.Categories.FindAsync(categoryId);

        if (categories == null)
        {
            return Content("This categories doesn't exist");
        }

        return View(categories);
    }

    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Delete(string id)
    {
        int categoryId = Int32.Parse(id);
        var categories = await _db.Categories.FindAsync(categoryId);

        if (categories != null)
        {
            _db.Categories.Remove(categories);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        return RedirectToAction("Index");
    }

    [HttpGet]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Create()
    {
        CategoryCreate formCreateCategory = new CategoryCreate();
        return View(formCreateCategory);
    }

    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Create(CategoryCreate categoryCreate)
    {
        if (ModelState.IsValid)
        {
            Category newCategory = new Category()
            {
                Name = categoryCreate.Name
            };
            var savedCategory = await _db.Categories.AddAsync(newCategory);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        return RedirectToAction("Create");
    }

    [HttpGet]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Update(string id)
    {
        int categoryId = Int32.Parse(id);
        var categories = await _db.Categories.FindAsync(categoryId);

        CategoryUpdate formUpdateCategory = new CategoryUpdate()
        {
            Id = categories.Id,
            Name = categories.Name
        };
        return View(formUpdateCategory);
    }

    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Update(CategoryUpdate categoryUpdate)
    {
        var category = await _db.Categories.FindAsync(categoryUpdate.Id);
        category.Name = categoryUpdate.Name;
        await _db.SaveChangesAsync();
        string cateId = $"{categoryUpdate.Id}";
        return RedirectToAction("Detail", new { id = cateId });
    }
}