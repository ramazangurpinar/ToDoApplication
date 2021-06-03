using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApplication.Infrastructure;
using ToDoApplication.Models;

namespace ToDoApplication.Controllers
{
    public class ToDoController : Controller
    {
        private readonly ToDoAppContext _context;
        public ToDoController(ToDoAppContext context)
        {
            _context = context;
        }

        // GET
        public async Task<ActionResult> Index()
        {
            var list = await _context.TodoLists.Include(x=> x.Category).ToListAsync();
            return View(list);
        }

        //GET create /todo/create
        public async Task<ActionResult> Create()
        {
            IQueryable<Category> items = from i in _context.Categories orderby i.CategoryId select i;
            List<Category> categories = await items.ToListAsync();
            ViewBag.Categories = categories;
            return View();
        }

        // POST /todo/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TodoList item)
        {
            if(item.CategoryId != 0 && item.Name.Length != 0)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                TempData["Success"] = "The item has been added!";
                return RedirectToAction("Index");
            }
            return View(item);
        }
        // GET /todo/edit/5
        public async Task<ActionResult> Edit(int id)
        {
            IQueryable<Category> items = from i in _context.Categories orderby i.CategoryId select i;
            List<Category> categories = await items.ToListAsync();
            ViewBag.Categories = categories;

            TodoList item = await _context.TodoLists.FindAsync(id);
            if (item == null)
            {
                return NotFound(item);
            }
            return View(item);
        }

        // POST /todo/edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(TodoList item)
        {
            if (item.CategoryId != 0 && item.Name.Length != 0)
            {
                _context.Update(item);
                await _context.SaveChangesAsync();
                TempData["Success"] = "The item has been updated!";
                return RedirectToAction("Index");
            }
            return View(item);
        }
        // GET /todo/delete/5
        public async Task<ActionResult> Delete(int id)
        {
            TodoList item = await _context.TodoLists.FindAsync(id);
            if (item == null)
            {
                TempData["Error"] = "The item does not exist!";
            }
            else
            {
                _context.TodoLists.Remove(item);
                await _context.SaveChangesAsync();
                TempData["Success"] = "The item ( "+item.Name+" ) has been deleted!";
            }
            return RedirectToAction("Index");
        }
    }
}
