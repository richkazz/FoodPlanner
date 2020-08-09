using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodPlanner.Models.MainIngredients;
using Identity.Models;
using FoodPlanner.Util;
using NToastNotify;

namespace FoodPlanner.Controllers
{
    public class MainIngredientsController : Controller
    {
        private readonly AppIdentityDbContext _context;
        private readonly IToastNotification _toastNotification;

        public MainIngredientsController(AppIdentityDbContext context, IToastNotification toastNotification)
        {
            _toastNotification = toastNotification;
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
            if (!ModelState.IsValid)
            {
                string msg = (ModelState.FirstOrDefault(x => x.Value.Errors.Any()).Value.Errors.FirstOrDefault().ErrorMessage).Replace("'", "");
                _toastNotification.AddErrorToastMessage(msg);

                return View(mainIngredient);
            }
            if (ModelState.IsValid)
            {
                var checkExit = _context.MainIngredient.Where(x => x.Name.ToLower() == mainIngredient.Name.ToLower()).Count();

                if (checkExit == 0)
                {
                    _context.Add(mainIngredient);
                    await _context.SaveChangesAsync();
                    _toastNotification.AddSuccessToastMessage(ResponseMessageUtilities.CREATED_SUCESSFUL);
                    return RedirectToAction(nameof(Index));
                }
                _toastNotification.AddErrorToastMessage(ResponseMessageUtilities.ITEM_EXIST);
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
            if (!ModelState.IsValid)
            {
                string msg = (ModelState.FirstOrDefault(x => x.Value.Errors.Any()).Value.Errors.FirstOrDefault().ErrorMessage).Replace("'", "");
                _toastNotification.AddErrorToastMessage(msg);

                return View(mainIngredient);
            }

            var CheckExist = _context.MainIngredient.Where(x => x.Id != mainIngredient.Id && x.Name.ToLower() == mainIngredient.Name.ToLower()).Count();

            if (CheckExist == 0)
            {
                var model = _context.MainIngredient.FirstOrDefault(x => x.Id == mainIngredient.Id);
                model.Name = mainIngredient.Name;
                model.ClassOfFood = mainIngredient.ClassOfFood;
                await _context.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage(ResponseMessageUtilities.UPDATE_SUCESSFUL);

                return RedirectToAction(nameof(Index));
            }
            _toastNotification.AddErrorToastMessage(ResponseMessageUtilities.ITEM_EXIST);
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
