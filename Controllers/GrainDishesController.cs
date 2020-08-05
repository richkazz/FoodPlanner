using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodPlanner.Models.GrainDishes;
using Identity.Models;

namespace FoodPlanner.Controllers
{
    public class GrainDishesController : Controller
    {
        private readonly AppIdentityDbContext _context;

        public GrainDishesController(AppIdentityDbContext context)
        {
            _context = context;
        }

        // GET: GrainDishes
        public async Task<IActionResult> Index()
        {
            return View(await _context.GrainDish.ToListAsync());
        }

        // GET: GrainDishes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grainDish = await _context.GrainDish
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grainDish == null)
            {
                return NotFound();
            }

            return View(grainDish);
        }

        // GET: GrainDishes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GrainDishes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] GrainDish grainDish)
        {
            if (ModelState.IsValid)
            {
                _context.Add(grainDish);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(grainDish);
        }

        // GET: GrainDishes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grainDish = await _context.GrainDish.FindAsync(id);
            if (grainDish == null)
            {
                return NotFound();
            }
            return View(grainDish);
        }

        // POST: GrainDishes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] GrainDish grainDish)
        {
            if (id != grainDish.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grainDish);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GrainDishExists(grainDish.Id))
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
            return View(grainDish);
        }

        // GET: GrainDishes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grainDish = await _context.GrainDish
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grainDish == null)
            {
                return NotFound();
            }

            return View(grainDish);
        }

        // POST: GrainDishes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grainDish = await _context.GrainDish.FindAsync(id);
            _context.GrainDish.Remove(grainDish);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GrainDishExists(int id)
        {
            return _context.GrainDish.Any(e => e.Id == id);
        }
    }
}
