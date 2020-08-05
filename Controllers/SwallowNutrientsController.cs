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
    public class SwallowNutrientsController : Controller
    {
        private readonly AppIdentityDbContext _context;

        public SwallowNutrientsController(AppIdentityDbContext context)
        {
            _context = context;
        }

        // GET: SwallowNutrients
        public async Task<IActionResult> Index()
        {
            var swallows = await _context.Swallow.ToListAsync();
            ViewData["swallows"] = swallows;
            var mainIngredients = await _context.MainIngredient.ToListAsync();
            ViewData["mainIngredients"] = mainIngredients;
            return View(await _context.SwallowNutrient.ToListAsync());
        }

        // GET: SwallowNutrients/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            var result = await _context.SwallowNutrient.FirstOrDefaultAsync(x => x.Id == id);
            var swallows = await _context.Swallow.ToListAsync();
            ViewData["swallows"] = swallows;
            var mainIngredients = await _context.MainIngredient.ToListAsync();
            ViewData["mainIngredients"] = mainIngredients;

            int indx = 0;
            int indx2 = 0;

            foreach (var swoMainIng in mainIngredients)
            {

                if (result.MainIngredientsId == swoMainIng.Id)
                {
                    ViewData["indSwallow"] = indx;
                    break;
                }
                indx++;
            }


            foreach (var swoFood in swallows)
            {

                if (result.SwallowName == swoFood.Id)
                {
                    ViewData["indSwallowNutrient"] = indx2;
                    break;
                }
                indx2++;
            }


            if (id == null)
            {
                return NotFound();
            }

            var swallowNutrient = await _context.SwallowNutrient
                .FirstOrDefaultAsync(m => m.Id == id);
            if (swallowNutrient == null)
            {
                return NotFound();
            }

            return View(swallowNutrient);
        }

        // GET: SwallowNutrients/Create
        public async Task<IActionResult> Create()
        {
            var swallows = await _context.Swallow.ToListAsync();
            ViewData["swallows"] = swallows;
            var mainIngredients = await _context.MainIngredient.ToListAsync();
            ViewData["mainIngredients"] = mainIngredients;
            return View();
        }

        // POST: SwallowNutrients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SwallowName,MainIngredientsId")] SwallowNutrient swallowNutrient
            , int ingswallowings, int ingredient)
        {
            swallowNutrient.SwallowName = ingswallowings;
            swallowNutrient.MainIngredientsId = ingredient;
            if (ModelState.IsValid)
            {
                _context.Add(swallowNutrient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(swallowNutrient);
        }

        // GET: SwallowNutrients/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            var swallows = await _context.Swallow.ToListAsync();
            ViewData["swallows"] = swallows;
            var mainIngredients = await _context.MainIngredient.ToListAsync();
            ViewData["mainIngredients"] = mainIngredients;
            if (id == null)
            {
                return NotFound();
            }

            var swallowNutrient = await _context.SwallowNutrient.FindAsync(id);
            if (swallowNutrient == null)
            {
                return NotFound();
            }
            return View(swallowNutrient);
        }

        // POST: SwallowNutrients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,SwallowName,MainIngredientsId")] SwallowNutrient swallowNutrient, int ingswallowings, int ingredient)
        {
            swallowNutrient.SwallowName = ingswallowings;
            swallowNutrient.MainIngredientsId = ingredient;
            if (id != swallowNutrient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(swallowNutrient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SwallowNutrientExists(swallowNutrient.Id))
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
            return View(swallowNutrient);
        }

        // GET: SwallowNutrients/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            var result = await _context.SwallowNutrient.FirstOrDefaultAsync(x => x.Id == id);
            var swallows = await _context.Swallow.ToListAsync();
            ViewData["swallows"] = swallows;
            var mainIngredients = await _context.MainIngredient.ToListAsync();
            ViewData["mainIngredients"] = mainIngredients;

            int indx = 0;
            int indx2 = 0;

            foreach (var swoMainIng in mainIngredients)
            {

                if (result.MainIngredientsId == swoMainIng.Id)
                {
                    ViewData["indSwallow"] = indx;
                    break;
                }
                indx++;
            }


            foreach (var swoFood in swallows)
            {

                if (result.SwallowName == swoFood.Id)
                {
                    ViewData["indSwallowNutrient"] = indx2;
                    break;
                }
                indx2++;
            }


            if (id == null)
            {
                return NotFound();
            }

            var swallowNutrient = await _context.SwallowNutrient
                .FirstOrDefaultAsync(m => m.Id == id);
            if (swallowNutrient == null)
            {
                return NotFound();
            }

            return View(swallowNutrient);
        }

        // POST: SwallowNutrients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var swallowNutrient = await _context.SwallowNutrient.FindAsync(id);
            _context.SwallowNutrient.Remove(swallowNutrient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SwallowNutrientExists(long id)
        {
            return _context.SwallowNutrient.Any(e => e.Id == id);
        }
    }
}
