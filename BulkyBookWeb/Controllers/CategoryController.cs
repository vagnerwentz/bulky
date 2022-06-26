
using BulkyBook.DataAccess;
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
    
    /// <summary>
    /// We can remove the .ToList() when we do IEnurable Category
    /// </summary>
    public IActionResult Index()
    {
        IEnumerable<Category> categoryList = _db.Categories.ToList();

        return View(categoryList);
    }

    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
            return NotFound();

        var category = _db.Categories.Find(id);
        // var categoryFromFirst = _db.Categories.FirstOrDefault(category => category.Id == id);
        // var categoryFromSingle = _db.Categories.SingleOrDefault(category => category.Id == id);

        if (category == null)
            return NotFound();
        
        return View(category);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category category)
    {
        if (category.Name == category.DisplayOrderType.ToString())
            ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
        
        if (ModelState.IsValid)
        {
            _db.Categories.Update(category);
            _db.SaveChanges();
            TempData["success"] = "Category updated succesfuly";
            return RedirectToAction("Index");    
        }

        return View(category);
    }
    
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category category)
    {
        if (category.Name == category.DisplayOrderType.ToString())
            ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
        
        if (ModelState.IsValid)
        {
            _db.Categories.Add(category);
            _db.SaveChanges();
            TempData["success"] = "Category created succesfuly";
            return RedirectToAction("Index");    
        }

        return View(category);
    }
    
    
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
            return NotFound();

        var category = _db.Categories.Find(id);
        // var categoryFromFirst = _db.Categories.FirstOrDefault(category => category.Id == id);
        // var categoryFromSingle = _db.Categories.SingleOrDefault(category => category.Id == id);

        if (category == null)
            return NotFound();
        
        return View(category);
    }
    
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePOST(int? id)
    {
        var category = _db.Categories.Find(id);
        
        if (category == null)
            return NotFound();

        _db.Categories.Remove(category);
        _db.SaveChanges();
        TempData["success"] = "Category deleted succesfuly";

        return RedirectToAction("Index");
    }
}