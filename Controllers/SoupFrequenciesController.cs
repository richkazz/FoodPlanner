using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodPlanner.Models;
using Identity.Models;
using FoodPlanner.Util;
using NToastNotify;

namespace FoodPlanner.Controllers
{
    public class SoupFrequenciesController : Controller
    {
        private readonly AppIdentityDbContext _context;
        private readonly IToastNotification _toastNotification;

        public SoupFrequenciesController(AppIdentityDbContext context, IToastNotification toastNotification)
        {
            _toastNotification = toastNotification;
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
            if (!ModelState.IsValid)
            {
                string msg = (ModelState.FirstOrDefault(x => x.Value.Errors.Any()).Value.Errors.FirstOrDefault().ErrorMessage).Replace("'", "");
                _toastNotification.AddErrorToastMessage(msg);

                return View(soupFrequency);
            }
            if (ModelState.IsValid)
            {
                var checkExit = _context.SoupFrequency.Where(x => x.SoupCount == soupFrequency.SoupCount).Count();

                if (checkExit == 0)
                {
                    _context.Add(soupFrequency);
                    await _context.SaveChangesAsync();
                    _toastNotification.AddErrorToastMessage(ResponseMessageUtilities.CREATED_SUCESSFUL);
                    return RedirectToAction(nameof(Index));
                }
                _toastNotification.AddErrorToastMessage(ResponseMessageUtilities.ITEM_EXIST);
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
            if (!ModelState.IsValid)
            {
                string msg = (ModelState.FirstOrDefault(x => x.Value.Errors.Any()).Value.Errors.FirstOrDefault().ErrorMessage).Replace("'", "");
                _toastNotification.AddErrorToastMessage(msg);

                return View(soupFrequency);
            }

            var CheckExist = _context.SoupFrequency.Where(x => x.Id != soupFrequency.Id && x.SoupCount == soupFrequency.SoupCount).Count();

            if (CheckExist == 0)
            {
                var model = _context.SoupFrequency.FirstOrDefault(x => x.Id == soupFrequency.Id);
                model.SoupCount = soupFrequency.SoupCount;
                await _context.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage(ResponseMessageUtilities.UPDATE_SUCESSFUL);

                return RedirectToAction(nameof(Index));
            }
            _toastNotification.AddErrorToastMessage(ResponseMessageUtilities.ITEM_EXIST);
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
