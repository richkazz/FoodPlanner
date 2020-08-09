using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodPlanner.Models.LightFood;
using Identity.Models;
using NToastNotify;
using FoodPlanner.Util;

namespace FoodPlanner.Controllers
{
    public class LightFoodsController : Controller
    {
        private readonly AppIdentityDbContext _context;
        private readonly IToastNotification _toastNotification;

        public LightFoodsController(AppIdentityDbContext context, IToastNotification toastNotification)
        {
            _toastNotification = toastNotification;
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
            if (!ModelState.IsValid)
            {
                string msg = (ModelState.FirstOrDefault(x => x.Value.Errors.Any()).Value.Errors.FirstOrDefault().ErrorMessage).Replace("'", "");
                _toastNotification.AddErrorToastMessage(msg);

                return View(lightFood);
            }
            if (ModelState.IsValid)
            {
                var checkExit = _context.LightFood.Where(x => x.Name.ToLower() == lightFood.Name.ToLower()).Count();

                if (checkExit == 0)
                {
                    _context.Add(lightFood);
                    await _context.SaveChangesAsync();
                    _toastNotification.AddErrorToastMessage(ResponseMessageUtilities.CREATED_SUCESSFUL);
                    return RedirectToAction(nameof(Index));
                }
                _toastNotification.AddErrorToastMessage(ResponseMessageUtilities.ITEM_EXIST);
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
            if (!ModelState.IsValid)
            {
                string msg = (ModelState.FirstOrDefault(x => x.Value.Errors.Any()).Value.Errors.FirstOrDefault().ErrorMessage).Replace("'", "");
                _toastNotification.AddErrorToastMessage(msg);

                return View(lightFood);
            }

            var CheckExist = _context.LightFood.Where(x => x.Id != lightFood.Id && x.Name.ToLower() == lightFood.Name.ToLower()).Count();

            if (CheckExist == 0)
            {
                var model = _context.LightFood.FirstOrDefault(x => x.Id == lightFood.Id);
                model.Name = lightFood.Name;
                await _context.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage(ResponseMessageUtilities.UPDATE_SUCESSFUL);

                return RedirectToAction(nameof(Index));
            }
            _toastNotification.AddErrorToastMessage(ResponseMessageUtilities.ITEM_EXIST);
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
