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
    public class LightFoodNutrientsController : Controller
    {
        private readonly AppIdentityDbContext _context;

        public LightFoodNutrientsController(AppIdentityDbContext context)
        {
            _context = context;
        }

        // GET: LightFoodNutrients
        public async Task<IActionResult> Index()
        {
            var lightfoodmainIngrdient = await _context.MainIngredient.ToListAsync();
            ViewData["lightfoodmainIngrdient"] = lightfoodmainIngrdient;
            var lightfood = await _context.LightFood.ToListAsync();
            ViewData["lightfood"] = lightfood;
            return View(await _context.LightFoodNutrient.ToListAsync());
        }

        // GET: LightFoodNutrients/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            var result = await _context.LightFoodNutrient.FirstOrDefaultAsync(x => x.Id == id);
            var lightfoodmainIngrdient = await _context.MainIngredient.ToListAsync();
            ViewData["lightfoodmainIngrdient"] = lightfoodmainIngrdient;
            var lightfood = await _context.LightFood.ToListAsync();
            ViewData["lightfood"] = lightfood;
            int indx = 0;
            int indx2 = 0;

            foreach (var ligMainIng in lightfoodmainIngrdient)
            {

                if (result.LightFoodMainIngredient == ligMainIng.Id)
                {
                    ViewData["indFoodNutrient"] = indx;
                    break;
                }
                indx++;
            }


            foreach (var ligFood in lightfood)
            {

                if (result.LightFoodName == ligFood.Id)
                {
                    ViewData["indFood"] = indx2;
                    break;
                }
                indx2++;
            }


            if (id == null)
            {
                return NotFound();
            }

            var lightFoodNutrient = await _context.LightFoodNutrient
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lightFoodNutrient == null)
            {
                return NotFound();
            }

            return View(lightFoodNutrient);
        }

        // GET: LightFoodNutrients/Create
        public async Task<IActionResult> Create()
        {
            var lightfoodmainIngrdient = await _context.MainIngredient.ToListAsync();
            ViewData["lightfoodmainIngrdient"] = lightfoodmainIngrdient;
            var lightfood = await _context.LightFood.ToListAsync();
            ViewData["lightfood"] = lightfood;
            return View();
        }

        // POST: LightFoodNutrients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LightFoodName,LightFoodMainIngredient")] LightFoodNutrient lightFoodNutrient, int lightfoodmain, int lightfoodas)
        {
            lightFoodNutrient.LightFoodName = lightfoodas;
            lightFoodNutrient.LightFoodMainIngredient = lightfoodmain;
            if (ModelState.IsValid)
            {
                _context.Add(lightFoodNutrient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lightFoodNutrient);
        }

        // GET: LightFoodNutrients/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            var lightfoodmainIngrdient = await _context.MainIngredient.ToListAsync();
            ViewData["lightfoodmainIngrdient"] = lightfoodmainIngrdient;
            var lightfood = await _context.LightFood.ToListAsync();
            ViewData["lightfood"] = lightfood;

            if (id == null)
            {
                return NotFound();
            }

            var lightFoodNutrient = await _context.LightFoodNutrient.FindAsync(id);
            if (lightFoodNutrient == null)
            {
                return NotFound();
            }
            return View(lightFoodNutrient);
        }

        // POST: LightFoodNutrients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,LightFoodName,LightFoodMainIngredient")] LightFoodNutrient lightFoodNutrient, int lightfoodmain, int lightfoodas)
        {
            lightFoodNutrient.LightFoodName = lightfoodas;
            lightFoodNutrient.LightFoodMainIngredient = lightfoodmain;

            if (id != lightFoodNutrient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lightFoodNutrient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LightFoodNutrientExists(lightFoodNutrient.Id))
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
            return View(lightFoodNutrient);
        }

        // GET: LightFoodNutrients/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            var result = await _context.LightFoodNutrient.FirstOrDefaultAsync(x => x.Id == id);
            var lightfoodmainIngrdient = await _context.MainIngredient.ToListAsync();
            ViewData["lightfoodmainIngrdient"] = lightfoodmainIngrdient;
            var lightfood = await _context.LightFood.ToListAsync();
            ViewData["lightfood"] = lightfood;
            int indx = 0;
            int indx2 = 0;

            foreach (var ligMainIng in lightfoodmainIngrdient)
            {

                if (result.LightFoodMainIngredient == ligMainIng.Id)
                {
                    ViewData["indFoodNutrient"] = indx;
                    break;
                }
                indx++;
            }


            foreach (var ligFood in lightfood)
            {

                if (result.LightFoodName == ligFood.Id)
                {
                    ViewData["indFood"] = indx2;
                    break;
                }
                indx2++;
            }
            if (id == null)
            {
                return NotFound();
            }

            var lightFoodNutrient = await _context.LightFoodNutrient
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lightFoodNutrient == null)
            {
                return NotFound();
            }

            return View(lightFoodNutrient);
        }

        // POST: LightFoodNutrients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var lightFoodNutrient = await _context.LightFoodNutrient.FindAsync(id);
            _context.LightFoodNutrient.Remove(lightFoodNutrient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LightFoodNutrientExists(long id)
        {
            return _context.LightFoodNutrient.Any(e => e.Id == id);
        }
    }
}
