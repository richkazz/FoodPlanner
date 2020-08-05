using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodPlanner.Models.Soups;
using Identity.Models;

namespace FoodPlanner.Controllers
{
    public class SoupsController : Controller
    {
        private readonly AppIdentityDbContext _context;

        public SoupsController(AppIdentityDbContext context)
        {
            _context = context;
        }

        // GET: Soups
        public async Task<IActionResult> Index()
        {
            return View(await _context.Soup.ToListAsync());
        }

        // GET: Soups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soup = await _context.Soup
                .FirstOrDefaultAsync(m => m.Id == id);
            if (soup == null)
            {
                return NotFound();
            }

            return View(soup);
        }

        // GET: Soups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Soups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Soup soup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(soup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(soup);
        }

        // GET: Soups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soup = await _context.Soup.FindAsync(id);
            if (soup == null)
            {
                return NotFound();
            }
            return View(soup);
        }

        // POST: Soups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Soup soup)
        {
            if (id != soup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(soup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SoupExists(soup.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(soup);
        }

        // GET: Soups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soup = await _context.Soup
                .FirstOrDefaultAsync(m => m.Id == id);
            if (soup == null)
            {
                return NotFound();
            }

            return View(soup);
        }

        // POST: Soups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var soup = await _context.Soup.FindAsync(id);
            _context.Soup.Remove(soup);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SoupExists(int id)
        {
            return _context.Soup.Any(e => e.Id == id);
        }
    }
}
