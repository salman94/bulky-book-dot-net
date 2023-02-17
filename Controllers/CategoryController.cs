using BulkyBook.DatAcess.Data;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers;

public class CategoryController : Controller
{
    private readonly ApplicationDbContext _db;

    public CategoryController(ApplicationDbContext db)
    {
        _db = db;
    }

    // GET
    public IActionResult Index()
    {
        IEnumerable<Category> categoriesList = _db.Categories;
        return View(categoriesList);
    }
    
    // GET
    public IActionResult Create()
    {
        return View();
    }
        
    // POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category category)
    {
        if (category.Name.Equals(category.DisplayOrder.ToString()))
        {
            ModelState.AddModelError("name", "Model name and display order can not be same");
        }
        if (!ModelState.IsValid) return View(category);
        _db.Categories.Add(category);
        _db.SaveChanges();
        TempData["success"] = "Category added successfully";
        return RedirectToAction("Index");
    }
    
    // GET
    public IActionResult Edit(int? id)
    {
        if (id is null or 0)
        {
            return NotFound();
        }

        Category category = _db.Categories.Find(id);
        if (category == null) return NotFound();
        return View(category);
    }
        
    // POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category category)
    {
        if (category.Name.Equals(category.DisplayOrder.ToString()))
        {
            ModelState.AddModelError("name", "Model name and display order can not be same");
        }
        if (!ModelState.IsValid) return View(category);
        _db.Categories.Update(category);
        _db.SaveChanges();
        TempData["success"] = "Category updated successfully";
        return RedirectToAction("Index");
    }
        
    // GET
    public IActionResult Delete(int? id)
    {
        if (id is null or 0)
        {
            return NotFound();
        }

        Category category = _db.Categories.Find(id);
        if (category == null) return NotFound();
        return View(category);
    }
        
    [HttpPost,ActionName("Delete")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePOST(int? id)
    {
        var obj = _db.Categories.Find(id);
        if (obj == null)
        {
            return NotFound();
        }

        _db.Categories.Remove(obj);
        _db.SaveChanges();
        TempData["success"] = "Category deleted successfully";
        return RedirectToAction("Index");
    }
}