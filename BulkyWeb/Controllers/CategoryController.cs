using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext db)
        {
            _context = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _context.Categories.OrderBy(u=>u.DisplayOrder).ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString()) {
                ModelState.AddModelError("Name", "Name and Display Order can't be same");
            }

            if (ModelState.IsValid)
            {
                _context.Categories.Add(obj);
                _context.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id==null || id == 0)
            {
                return NotFound();
            }
            Category? selectedCategoryObject = _context.Categories.FirstOrDefault(u=>u.Id==id);

            return selectedCategoryObject == null ? NotFound() : View(selectedCategoryObject);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Update(obj);
                _context.SaveChanges();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? selectedCategoryObject = _context.Categories.FirstOrDefault(u => u.Id == id);

            return selectedCategoryObject == null ? NotFound() : View(selectedCategoryObject);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult Delete(Category obj)
        {
            Category? instance = _context.Categories.FirstOrDefault(u => u.Id == obj.Id);
            if (instance == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(instance);
            _context.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }

    }

}

