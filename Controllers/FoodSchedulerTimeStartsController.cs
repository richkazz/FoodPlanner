using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodPlanner.Models.FoodSchedulerTimeStarts;
using Identity.Models;

namespace FoodPlanner.Controllers
{
    public class FoodSchedulerTimeStartsController : Controller
    {
        private readonly AppIdentityDbContext _context;

        public FoodSchedulerTimeStartsController(AppIdentityDbContext context)
        {
            _context = context;
        }

        // GET: FoodSchedulerTimeStarts
        public async Task<IActionResult> Index()
        {
            return View(await _context.FoodSchedulerTimeStarts.ToListAsync());
        }

        // GET: FoodSchedulerTimeStarts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodSchedulerTimeStarts = await _context.FoodSchedulerTimeStarts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (foodSchedulerTimeStarts == null)
            {
                return NotFound();
            }

            return View(foodSchedulerTimeStarts);
        }

        // GET: FoodSchedulerTimeStarts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FoodSchedulerTimeStarts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] FoodSchedulerTimeStarts foodSchedulerTimeStarts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(foodSchedulerTimeStarts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(foodSchedulerTimeStarts);
        }

        // GET: FoodSchedulerTimeStarts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodSchedulerTimeStarts = await _context.FoodSchedulerTimeStarts.FindAsync(id);
            if (foodSchedulerTimeStarts == null)
            {
                return NotFound();
            }
            return View(foodSchedulerTimeStarts);
        }

        // POST: FoodSchedulerTimeStarts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] FoodSchedulerTimeStarts foodSchedulerTimeStarts)
        {
            if (id != foodSchedulerTimeStarts.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(foodSchedulerTimeStarts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodSchedulerTimeStartsExists(foodSchedulerTimeStarts.Id))
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
            return View(foodSchedulerTimeStarts);
        }

        // GET: FoodSchedulerTimeStarts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodSchedulerTimeStarts = await _context.FoodSchedulerTimeStarts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (foodSchedulerTimeStarts == null)
            {
                return NotFound();
            }

            return View(foodSchedulerTimeStarts);
        }

        // POST: FoodSchedulerTimeStarts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var foodSchedulerTimeStarts = await _context.FoodSchedulerTimeStarts.FindAsync(id);
            _context.FoodSchedulerTimeStarts.Remove(foodSchedulerTimeStarts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodSchedulerTimeStartsExists(int id)
        {
            return _context.FoodSchedulerTimeStarts.Any(e => e.Id == id);
        }
    }
}
