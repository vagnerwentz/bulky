using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
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
}