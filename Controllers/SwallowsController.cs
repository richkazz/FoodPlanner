using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodPlanner.Models.Swallows;
using Identity.Models;

namespace FoodPlanner.Controllers
{
    public class SwallowsController : Controller
    {
        private readonly AppIdentityDbContext _context;

        public SwallowsController(AppIdentityDbContext context)
        {
            _context = context;
        }

        // GET: Swallows
        public async Task<IActionResult> Index()
        {
            return View(await _context.Swallow.ToListAsync());
        }

        // GET: Swallows/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var swallow = await _context.Swallow
                .FirstOrDefaultAsync(m => m.Id == id);
            if (swallow == null)
            {
                return NotFound();
            }

            return View(swallow);
        }

        // GET: Swallows/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Swallows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Swallow swallow)
        {
            if (ModelState.IsValid)
            {
                _context.Add(swallow);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(swallow);
        }

        // GET: Swallows/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var swallow = await _context.Swallow.FindAsync(id);
            if (swallow == null)
            {
                return NotFound();
            }
            return View(swallow);
        }

        // POST: Swallows/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Swallow swallow)
        {
            if (id != swallow.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(swallow);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SwallowExists(swallow.Id))
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
            return View(swallow);
        }

        // GET: Swallows/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var swallow = await _context.Swallow
                .FirstOrDefaultAsync(m => m.Id == id);
            if (swallow == null)
            {
                return NotFound();
            }

            return View(swallow);
        }

        // POST: Swallows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var swallow = await _context.Swallow.FindAsync(id);
            _context.Swallow.Remove(swallow);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SwallowExists(int id)
        {
            return _context.Swallow.Any(e => e.Id == id);
        }
    }
}
