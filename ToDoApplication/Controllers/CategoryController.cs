using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApplication.Infrastructure;
using ToDoApplication.Models;

namespace ToDoApplication.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ToDoAppContext _context;
        public CategoryController(ToDoAppContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> Index()
        {
            IQueryable<Category> items = from i in _context.Categories orderby i.CategoryId select i;
            List<Category> categories = await items.ToListAsync();
            return View(categories);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Category item)
        {
            if(ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                TempData["Success"] = "The item has been added!";
                return RedirectToAction("Index");
            }
            return View(item);
        }

        public async Task<ActionResult> Edit(int id)
        {
            Category item = await _context.Categories.FindAsync(id);
            if(item == null)
            {
                return NotFound(item);
            }
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Category item)
        {
            if (ModelState.IsValid)
            {
                _context.Update(item);
                await _context.SaveChangesAsync();
                TempData["Success"] = "The item has been updated!";
                return RedirectToAction("Index");
            }
            return View(item);
        }

        public async Task<ActionResult> Delete(int id)
        {
            Category item = await _context.Categories.FindAsync(id);
            if (item == null)
            {
                TempData["Error"] = "The item does not exist!";
            }
            else
            {
                _context.Categories.Remove(item);
                await _context.SaveChangesAsync();
                TempData["Success"] = "The item ( "+item.Name+" ) has been deleted!";
            }
            return RedirectToAction("Index");
        }
    }
}
