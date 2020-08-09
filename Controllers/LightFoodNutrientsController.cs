using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodPlanner.Models.LightFood;
using Identity.Models;
using FoodPlanner.Util;
using NToastNotify;

namespace FoodPlanner.Controllers
{
    public class LightFoodNutrientsController : Controller
    {
        private readonly AppIdentityDbContext _context;
        private readonly IToastNotification _toastNotification;

        public LightFoodNutrientsController(AppIdentityDbContext context, IToastNotification toastNotification)
        {
            _toastNotification = toastNotification;
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
        public async Task<IActionResult> Create([Bind("Id,LightFoodName,LightFoodMainIngredient")]
        LightFoodNutrient lightFoodNutrient, int lightfoodmain, int lightfoodas)
        {
            if (!ModelState.IsValid)
            {
                string msg = (ModelState.FirstOrDefault(x => x.Value.Errors.Any()).Value.Errors.FirstOrDefault().ErrorMessage).Replace("'", "");
                _toastNotification.AddErrorToastMessage(msg);

                return View(lightfoodmain);
            }
            if (ModelState.IsValid)
            {
                var checkExist = _context.LightFoodNutrient.Where(x => x.LightFoodName == lightfoodas).Where(x => x.LightFoodMainIngredient != 0).Count();
                if (checkExist == 0)
                {
                    lightFoodNutrient.LightFoodName = lightfoodas;
                    lightFoodNutrient.LightFoodMainIngredient = lightfoodmain;
                    _context.Add(lightFoodNutrient);
                    await _context.SaveChangesAsync();
                    _toastNotification.AddSuccessToastMessage(ResponseMessageUtilities.CREATED_SUCESSFUL);

                    return RedirectToAction(nameof(Index));

                }
                _toastNotification.AddErrorToastMessage(ResponseMessageUtilities.ITEM_EXIST);

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
        public async Task<IActionResult> Edit(long id, [Bind("Id,LightFoodName,LightFoodMainIngredient")]
        LightFoodNutrient lightFoodNutrient, int lightfoodmain, int lightfoodas)
        {
            if (!ModelState.IsValid)
            {
                string msg = (ModelState.FirstOrDefault(x => x.Value.Errors.Any()).Value.Errors.FirstOrDefault().ErrorMessage).Replace("'", "");
                _toastNotification.AddErrorToastMessage(msg);

                return View(lightfoodmain);
            }
            if (ModelState.IsValid)
            {
                var checkCount = _context.LightFoodNutrient.Where(x => x.Id == id).Where(x => x.LightFoodName == lightfoodas
                && x.LightFoodMainIngredient != 0).Count();
                if (checkCount == 0)
                {
                    _toastNotification.AddErrorToastMessage(ResponseMessageUtilities.ITEM_EXIST);
                    return RedirectToAction(nameof(Index));
                }

                var model = _context.LightFoodNutrient.FirstOrDefault(x => x.Id == id);
                model.LightFoodName = lightfoodas;
                model.LightFoodMainIngredient = lightfoodmain;
                await _context.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage(ResponseMessageUtilities.UPDATE_SUCESSFUL);

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
            _toastNotification.AddSuccessToastMessage(ResponseMessageUtilities.DELETED_SUCESSFUL);

            return RedirectToAction(nameof(Index));
        }

        private bool LightFoodNutrientExists(long id)
        {
            return _context.LightFoodNutrient.Any(e => e.Id == id);
        }
    }
}
