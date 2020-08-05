using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodPlanner.Models.LightFood;
using Identity.Models;

namespace FoodPlanner.Controllers
{
    public class LightFoodsController : Controller
    {
        private readonly AppIdentityDbContext _context;

        public LightFoodsController(AppIdentityDbContext context)
        {
            _context = context;
        }

        // GET: LightFoods
        public async Task<IActionResult> Index()
        {
            return View(await _context.LightFood.ToListAsync());
        }

        // GET: LightFoods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lightFood = await _context.LightFood
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lightFood == null)
            {
                return NotFound();
            }

            return View(lightFood);
        }

        // GET: LightFoods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LightFoods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] LightFood lightFood)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lightFood);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lightFood);
        }

        // GET: LightFoods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lightFood = await _context.LightFood.FindAsync(id);
            if (lightFood == null)
            {
                return NotFound();
            }
            return View(lightFood);
        }

        // POST: LightFoods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] LightFood lightFood)
        {
            if (id != lightFood.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lightFood);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LightFoodExists(lightFood.Id))
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
            return View(lightFood);
        }

        // GET: LightFoods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lightFood = await _context.LightFood
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lightFood == null)
            {
                return NotFound();
            }

            return View(lightFood);
        }

        // POST: LightFoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lightFood = await _context.LightFood.FindAsync(id);
            _context.LightFood.Remove(lightFood);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LightFoodExists(int id)
        {
            return _context.LightFood.Any(e => e.Id == id);
        }
    }
}
