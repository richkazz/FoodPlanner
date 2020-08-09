using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodPlanner.Models.GrainDishNutrients;
using Identity.Models;
using FoodPlanner.Util;
using NToastNotify;

namespace FoodPlanner.Controllers
{
    public class GrainDishNutrientsController : Controller
    {
        private readonly AppIdentityDbContext _context;
        private readonly IToastNotification _toastNotification;

        public GrainDishNutrientsController(AppIdentityDbContext context, IToastNotification toastNotification)
        {
            _toastNotification = toastNotification;
            _context = context;
        }
        // GET: GrainDishNutrients
        public async Task<IActionResult> Index()
        {
            var graindish = await _context.GrainDish.ToListAsync();
            ViewData["graindish"] = graindish;

            var karomainIngredients = await _context.MainIngredient.ToListAsync();
            ViewData["karomainIngredients"] = karomainIngredients;
            return View(await _context.GrainDishNutrient.ToListAsync());
        }

        // GET: GrainDishNutrients/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            var result = await _context.GrainDishNutrient.FirstOrDefaultAsync(x => x.Id == id);
            var graindish = await _context.GrainDish.ToListAsync();
            ViewData["graindish"] = graindish;

            var karomainIngredients = await _context.MainIngredient.ToListAsync();
            ViewData["karomainIngredients"] = karomainIngredients;
            int indx = 0;
            int indx2 = 0;

            foreach (var graMainIng in karomainIngredients)
            {

                if (result.KaroMainIngredientsId == graMainIng.Id)
                {
                    ViewData["indGrainDish"] = indx;
                    break;
                }
                indx++;
            }


            foreach (var graFood in graindish)
            {

                if (result.GrainName == graFood.Id)
                {
                    ViewData["indGrain"] = indx2;
                    break;
                }
                indx2++;
            }
            if (id == null)
            {
                return NotFound();
            }

            var grainDishNutrient = await _context.GrainDishNutrient
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grainDishNutrient == null)
            {
                return NotFound();
            }

            return View(grainDishNutrient);
        }

        // GET: GrainDishNutrients/Create
        public async Task<IActionResult> CreateAsync()
        {
            var graindish = await _context.GrainDish.ToListAsync();
            ViewData["graindish"] = graindish;
            var karomainIngredients = await _context.MainIngredient.ToListAsync();
            ViewData["karomainIngredients"] = karomainIngredients;
            return View();
        }

        // POST: GrainDishNutrients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GrainName,SoupRequired,KaroMainIngredientsId")]
        GrainDishNutrient grainDishNutrient, int karoingredient, int grainselect, bool soupRequired)
        {
            if (!ModelState.IsValid)
            {
                string msg = (ModelState.FirstOrDefault(x => x.Value.Errors.Any()).Value.Errors.FirstOrDefault().ErrorMessage).Replace("'", "");
                _toastNotification.AddErrorToastMessage(msg);

                return View(karoingredient);
            }
            if (ModelState.IsValid)
            {
                var checkExist = _context.GrainDishNutrient.Where(x => x.GrainName == grainselect).Where(x => x.KaroMainIngredientsId != 0).Count();
                if (checkExist == 0)
                {
                    grainDishNutrient.GrainName = grainselect;
                    grainDishNutrient.KaroMainIngredientsId = karoingredient;
                    _context.Add(grainDishNutrient);
                    await _context.SaveChangesAsync();
                    _toastNotification.AddSuccessToastMessage(ResponseMessageUtilities.CREATED_SUCESSFUL);

                    return RedirectToAction(nameof(Index));

                }
                _toastNotification.AddErrorToastMessage(ResponseMessageUtilities.ITEM_EXIST);

                return RedirectToAction(nameof(Index));
            }

            return View(grainDishNutrient);
        }

        // GET: GrainDishNutrients/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            var graindish = await _context.GrainDish.ToListAsync();
            ViewData["graindish"] = graindish;

            var karomainIngredients = await _context.MainIngredient.ToListAsync();
            ViewData["karomainIngredients"] = karomainIngredients;
            if (id == null)
            {
                return NotFound();
            }

            var grainDishNutrient = await _context.GrainDishNutrient.FindAsync(id);
            if (grainDishNutrient == null)
            {
                return NotFound();
            }
            return View(grainDishNutrient);
        }

        // POST: GrainDishNutrients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,GrainName,SoupRequired,KaroMainIngredientsId")] GrainDishNutrient grainDishNutrient,
            int karoingredient, int grainselect, bool soupRequired)
        {
            if (!ModelState.IsValid)
            {
                string msg = (ModelState.FirstOrDefault(x => x.Value.Errors.Any()).Value.Errors.FirstOrDefault().ErrorMessage).Replace("'", "");
                _toastNotification.AddErrorToastMessage(msg);

                return View(karoingredient);
            }
            if (ModelState.IsValid)
            {
                var checkCount = _context.GrainDishNutrient.Where(x => x.Id == id).Where(x => x.GrainName == grainselect
                && x.KaroMainIngredientsId != 0).Count();
                if (checkCount == 0)
                {
                    _toastNotification.AddErrorToastMessage(ResponseMessageUtilities.ITEM_EXIST);
                    return RedirectToAction(nameof(Index));
                }

                var model = _context.GrainDishNutrient.FirstOrDefault(x => x.Id == id);
                model.GrainName = grainselect;
                model.KaroMainIngredientsId = karoingredient;
                await _context.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage(ResponseMessageUtilities.UPDATE_SUCESSFUL);

                return RedirectToAction(nameof(Index));
            }
            return View(grainDishNutrient);
        }

        // GET: GrainDishNutrients/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {

            var result = await _context.GrainDishNutrient.FirstOrDefaultAsync(x => x.Id == id);
            var graindish = await _context.GrainDish.ToListAsync();
            ViewData["graindish"] = graindish;

            var karomainIngredients = await _context.MainIngredient.ToListAsync();
            ViewData["karomainIngredients"] = karomainIngredients;
            int indx = 0;
            int indx2 = 0;

            foreach (var graMainIng in karomainIngredients)
            {

                if (result.KaroMainIngredientsId == graMainIng.Id)
                {
                    ViewData["indGrainDish"] = indx;
                    break;
                }
                indx++;
            }


            foreach (var graFood in graindish)
            {

                if (result.GrainName == graFood.Id)
                {
                    ViewData["indGrain"] = indx2;
                    break;
                }
                indx2++;
            }
            if (id == null)
            {
                return NotFound();
            }

            var grainDishNutrient = await _context.GrainDishNutrient
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grainDishNutrient == null)
            {
                return NotFound();
            }

            return View(grainDishNutrient);
        }

        // POST: GrainDishNutrients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var grainDishNutrient = await _context.GrainDishNutrient.FindAsync(id);
            _context.GrainDishNutrient.Remove(grainDishNutrient);
            await _context.SaveChangesAsync();
            _toastNotification.AddSuccessToastMessage(ResponseMessageUtilities.DELETED_SUCESSFUL);

            return RedirectToAction(nameof(Index));
        }

        private bool GrainDishNutrientExists(long id)
        {
            return _context.GrainDishNutrient.Any(e => e.Id == id);
        }
    }
}
