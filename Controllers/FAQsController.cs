using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodPlanner.Models.FAQ;
using Identity.Models;

namespace FoodPlanner.Controllers
{
    public class FAQsController : Controller
    {
        private readonly AppIdentityDbContext _context;

        public FAQsController(AppIdentityDbContext context)
        {
            _context = context;
        }

        // GET: FAQs
        public async Task<IActionResult> Index()
        {
            return View(await _context.FAQs.ToListAsync());
        }
        public async Task<IActionResult> UserIndex()
        {
            return View(await _context.FAQs.ToListAsync());
        }

        // GET: FAQs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fAQs = await _context.FAQs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fAQs == null)
            {
                return NotFound();
            }

            return View(fAQs);
        }

        // GET: FAQs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FAQs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Question,Answer")] FAQs fAQs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fAQs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fAQs);
        }

        // GET: FAQs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fAQs = await _context.FAQs.FindAsync(id);
            if (fAQs == null)
            {
                return NotFound();
            }
            return View(fAQs);
        }

        // POST: FAQs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Question,Answer,Status")] FAQs fAQs)
        {
            if (id != fAQs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fAQs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FAQsExists(fAQs.Id))
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
            return View(fAQs);
        }
       
        // GET: FAQs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fAQs = await _context.FAQs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fAQs == null)
            {
                return NotFound();
            }

            return View(fAQs);
        }

        // POST: FAQs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fAQs = await _context.FAQs.FindAsync(id);
            _context.FAQs.Remove(fAQs);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FAQsExists(int id)
        {
            return _context.FAQs.Any(e => e.Id == id);
        }
    }
}
