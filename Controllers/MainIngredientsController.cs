using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodPlanner.Models.MainIngredients;
using Identity.Models;

namespace FoodPlanner.Controllers
{
    public class MainIngredientsController : Controller
    {
        private readonly AppIdentityDbContext _context;

        public MainIngredientsController(AppIdentityDbContext context)
        {
            _context = context;
        }

        // GET: MainIngredients
        public async Task<IActionResult> Index()
        {
            return View(await _context.MainIngredient.ToListAsync());
        }

        // GET: MainIngredients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mainIngredient = await _context.MainIngredient
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mainIngredient == null)
            {
                return NotFound();
            }

            return View(mainIngredient);
        }

        // GET: MainIngredients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MainIngredients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ClassOfFood")] MainIngredient mainIngredient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mainIngredient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mainIngredient);
        }

        // GET: MainIngredients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mainIngredient = await _context.MainIngredient.FindAsync(id);
            if (mainIngredient == null)
            {
                return NotFound();
            }
            return View(mainIngredient);
        }

        // POST: MainIngredients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ClassOfFood")] MainIngredient mainIngredient)
        {
            if (id != mainIngredient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mainIngredient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MainIngredientExists(mainIngredient.Id))
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
            return View(mainIngredient);
        }

        // GET: MainIngredients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mainIngredient = await _context.MainIngredient
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mainIngredient == null)
            {
                return NotFound();
            }

            return View(mainIngredient);
        }

        // POST: MainIngredients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mainIngredient = await _context.MainIngredient.FindAsync(id);
            _context.MainIngredient.Remove(mainIngredient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MainIngredientExists(int id)
        {
            return _context.MainIngredient.Any(e => e.Id == id);
        }
    }
}
