using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodPlanner.Models.GrainDishes;
using Identity.Models;
using Org.BouncyCastle.Math.EC.Rfc7748;
using NToastNotify;
using FoodPlanner.Util;

namespace FoodPlanner.Controllers
{
    public class GrainDishesController : Controller
    {
        private readonly AppIdentityDbContext _context;
        private readonly IToastNotification _toastNotification;

        public GrainDishesController(AppIdentityDbContext context, IToastNotification toastNotification)
        {
            _toastNotification = toastNotification;
            _context = context;
        }

        // GET: GrainDishes
        public async Task<IActionResult> Index()
        {
            return View(await _context.GrainDish.ToListAsync());
        }

        // GET: GrainDishes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grainDish = await _context.GrainDish
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grainDish == null)
            {
                return NotFound();
            }

            return View(grainDish);
        }

        // GET: GrainDishes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GrainDishes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] GrainDish grainDish)
        {
            if (!ModelState.IsValid)
            {
                string msg = (ModelState.FirstOrDefault(x => x.Value.Errors.Any()).Value.Errors.FirstOrDefault().ErrorMessage).Replace("'", "");
                _toastNotification.AddErrorToastMessage(msg);

                return View(grainDish);
            }

            var CheckExist = _context.GrainDish.Where(x => x.Id != grainDish.Id && x.Name.ToLower() == grainDish.Name.ToLower()).Count();

            if (CheckExist == 0)
            {
                var model = _context.Soup.FirstOrDefault(x => x.Id == grainDish.Id);
                model.Name = grainDish.Name;
                await _context.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage(ResponseMessageUtilities.UPDATE_SUCESSFUL);

                return RedirectToAction(nameof(Index));
            }
            _toastNotification.AddErrorToastMessage(ResponseMessageUtilities.ITEM_EXIST);
            return View(grainDish);
        }

        // GET: GrainDishes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grainDish = await _context.GrainDish.FindAsync(id);
            if (grainDish == null)
            {
                return NotFound();
            }
            return View(grainDish);
        }

        // POST: GrainDishes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] GrainDish grainDish)
        {
            if (!ModelState.IsValid)
            {
                string msg = (ModelState.FirstOrDefault(x => x.Value.Errors.Any()).Value.Errors.FirstOrDefault().ErrorMessage).Replace("'", "");
                _toastNotification.AddErrorToastMessage(msg);

                return View(grainDish);
            }

            var CheckExist = _context.GrainDish.Where(x => x.Id != grainDish.Id && x.Name.ToLower() == grainDish.Name.ToLower()).Count();

            if (CheckExist == 0)
            {
                var model = _context.GrainDish.FirstOrDefault(x => x.Id == grainDish.Id);
                model.Name = grainDish.Name;
                await _context.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage(ResponseMessageUtilities.UPDATE_SUCESSFUL);

                return RedirectToAction(nameof(Index));
            }
            _toastNotification.AddErrorToastMessage(ResponseMessageUtilities.ITEM_EXIST);
            return View(grainDish);
        }

        // GET: GrainDishes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grainDish = await _context.GrainDish
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grainDish == null)
            {
                return NotFound();
            }

            return View(grainDish);
        }

        // POST: GrainDishes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grainDish = await _context.GrainDish.FindAsync(id);
            _context.GrainDish.Remove(grainDish);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GrainDishExists(int id)
        {
            return _context.GrainDish.Any(e => e.Id == id);
        }
    }
}
