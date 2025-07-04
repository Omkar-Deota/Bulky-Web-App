using BulkyWebRazor.Data;
using BulkyWebRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor.Pages.Categories
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public Category Category { get; set; }
        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet(int? id)
        {
            if (id != null && id != 0)
            {
            Category = _context.Categories.FirstOrDefault(c => c.Id == id);
            }
           
        }

        public IActionResult OnPost()
        {
            Category? obj = _context.Categories.Find(Category.Id);
            if (obj == null) 
            {
                return NotFound(); 
            }
            _context.Categories.Remove(obj);
            _context.SaveChanges();
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToPage("Index");

        }
    }
}
