using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodPlanner.Models;
using Identity.Models;

namespace FoodPlanner.Controllers
{
    public class SoupFrequenciesController : Controller
    {
        private readonly AppIdentityDbContext _context;

        public SoupFrequenciesController(AppIdentityDbContext context)
        {
            _context = context;
        }

        // GET: SoupFrequencies
        public async Task<IActionResult> Index()
        {
            return View(await _context.SoupFrequency.ToListAsync());
        }

        // GET: SoupFrequencies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soupFrequency = await _context.SoupFrequency
                .FirstOrDefaultAsync(m => m.Id == id);
            if (soupFrequency == null)
            {
                return NotFound();
            }

            return View(soupFrequency);
        }

        // GET: SoupFrequencies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SoupFrequencies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SoupCount")] SoupFrequency soupFrequency)
        {
            if (ModelState.IsValid)
            {
                _context.Add(soupFrequency);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(soupFrequency);
        }

        // GET: SoupFrequencies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soupFrequency = await _context.SoupFrequency.FindAsync(id);
            if (soupFrequency == null)
            {
                return NotFound();
            }
            return View(soupFrequency);
        }

        // POST: SoupFrequencies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SoupCount")] SoupFrequency soupFrequency)
        {
            if (id != soupFrequency.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(soupFrequency);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SoupFrequencyExists(soupFrequency.Id))
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
            return View(soupFrequency);
        }

        // GET: SoupFrequencies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soupFrequency = await _context.SoupFrequency
                .FirstOrDefaultAsync(m => m.Id == id);
            if (soupFrequency == null)
            {
                return NotFound();
            }

            return View(soupFrequency);
        }

        // POST: SoupFrequencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var soupFrequency = await _context.SoupFrequency.FindAsync(id);
            _context.SoupFrequency.Remove(soupFrequency);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SoupFrequencyExists(int id)
        {
            return _context.SoupFrequency.Any(e => e.Id == id);
        }
    }
}
