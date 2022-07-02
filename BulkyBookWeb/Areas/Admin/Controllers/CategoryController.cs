
using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers;

[Area("Admin")]
public class CategoryController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    /// <summary>
    /// We can remove the .ToList() when we do IEnurable Category
    /// </summary>
    public IActionResult Index()
    {
        IEnumerable<Category> categoryList = _unitOfWork.Category.GetAll();

        return View(categoryList);
    }

    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
            return NotFound();

        // var category = _db.Categories.Find(id);
        var categoryFromFirst = _unitOfWork.Category.GetFirstOrDefault(category => category.Id == id);
        // var categoryFromSingle = _db.Categories.SingleOrDefault(category => category.Id == id);

        if (categoryFromFirst == null)
            return NotFound();
        
        return View(categoryFromFirst);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category category)
    {
        if (category.Name == category.DisplayOrderType.ToString())
            ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
        
        if (ModelState.IsValid)
        {
            _unitOfWork.Category.Update(category);
            _unitOfWork.Save();
            TempData["success"] = "Category updated succesfully";
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
            _unitOfWork.Category.Add(category);
            _unitOfWork.Save();
            TempData["success"] = "Category created succesfuly";
            return RedirectToAction("Index");    
        }

        return View(category);
    }
    
    
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
            return NotFound();

        // var category = _db.Categories.Find(id);
        var categoryFromFirst = _unitOfWork.Category.GetFirstOrDefault(category => category.Id == id);
        // var categoryFromSingle = _db.Categories.SingleOrDefault(category => category.Id == id);

        if (categoryFromFirst == null)
            return NotFound();
        
        return View(categoryFromFirst);
    }
    
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePOST(int? id)
    {
        var category = _unitOfWork.Category.GetFirstOrDefault(category => category.Id == id);
        
        if (category == null)
            return NotFound();

        _unitOfWork.Category.Remove(category);
        _unitOfWork.Save();
        TempData["success"] = "Category deleted succesfuly";

        return RedirectToAction("Index");
    }
}