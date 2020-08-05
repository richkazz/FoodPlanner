using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodPlanner.Models.SoupCategory;
using Identity.Models;

namespace FoodPlanner.Controllers
{
    public class SwallowSoupsController : Controller
    {
        private readonly AppIdentityDbContext _context;

        public SwallowSoupsController(AppIdentityDbContext context)
        {
            _context = context;
        }

        // GET: SwallowSoups
        public async Task<IActionResult> Index()
        {
            var graindishsoup = await _context.Soup.ToListAsync();
            ViewData["graindishsoup"] = graindishsoup;
            return View(await _context.SwallowSoup.ToListAsync());
        }

        // GET: SwallowSoups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var result = await _context.SwallowSoup.FirstOrDefaultAsync(x => x.Id == id);
            var graindishsoup = await _context.Soup.ToListAsync();
            ViewData["graindishsoup"] = graindishsoup;


            int indx2 = 0;



            foreach (var graFood in graindishsoup)
            {

                if (result.SwallowSoupId == graFood.Id)
                {
                    ViewData["indGrainDishSoup"] = indx2;
                    break;
                }
                indx2++;
            }
            if (id == null)
            {
                return NotFound();
            }

            var swallowSoup = await _context.SwallowSoup
                .FirstOrDefaultAsync(m => m.Id == id);
            if (swallowSoup == null)
            {
                return NotFound();
            }

            return View(swallowSoup);
        }

        // GET: SwallowSoups/Create
        public async Task<IActionResult> Create()
        {
            var graindishsoup = await _context.Soup.ToListAsync();
            ViewData["graindishsoup"] = graindishsoup;
            return View();
        }

        // POST: SwallowSoups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SwallowSoupId")] SwallowSoup swallowSoup, int grainselect)
        {
            swallowSoup.SwallowSoupId = grainselect;
            if (ModelState.IsValid)
            {
                _context.Add(swallowSoup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(swallowSoup);
        }

        // GET: SwallowSoups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var graindishsoup = await _context.Soup.ToListAsync();
            ViewData["graindishsoup"] = graindishsoup;
            if (id == null)
            {
                return NotFound();
            }

            var swallowSoup = await _context.SwallowSoup.FindAsync(id);
            if (swallowSoup == null)
            {
                return NotFound();
            }
            return View(swallowSoup);
        }

        // POST: SwallowSoups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SwallowSoupId")] SwallowSoup swallowSoup, int grainselect)
        {
            swallowSoup.SwallowSoupId = grainselect;
            if (id != swallowSoup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(swallowSoup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SwallowSoupExists(swallowSoup.Id))
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
            return View(swallowSoup);
        }

        // GET: SwallowSoups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var result = await _context.SwallowSoup.FirstOrDefaultAsync(x => x.Id == id);
            var graindishsoup = await _context.Soup.ToListAsync();
            ViewData["graindishsoup"] = graindishsoup;


            int indx2 = 0;



            foreach (var graFood in graindishsoup)
            {

                if (result.SwallowSoupId == graFood.Id)
                {
                    ViewData["indGrainDishSoup"] = indx2;
                    break;
                }
                indx2++;
            }
            if (id == null)
            {
                return NotFound();
            }

            var swallowSoup = await _context.SwallowSoup
                .FirstOrDefaultAsync(m => m.Id == id);
            if (swallowSoup == null)
            {
                return NotFound();
            }

            return View(swallowSoup);
        }

        // POST: SwallowSoups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var swallowSoup = await _context.SwallowSoup.FindAsync(id);
            _context.SwallowSoup.Remove(swallowSoup);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SwallowSoupExists(int id)
        {
            return _context.SwallowSoup.Any(e => e.Id == id);
        }
    }
}