using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodPlanner.Models.UserFoodSelectionCategory;
using Identity.Models;

namespace FoodPlanner.Controllers
{
    public class UserSwallowSelectionsController : Controller
    {
        private readonly AppIdentityDbContext _context;

        public UserSwallowSelectionsController(AppIdentityDbContext context)
        {
            _context = context;
        }

        // GET: UserSwallowSelections
        public async Task<IActionResult> Index(string message)
        {


            ViewBag.Error = message;
            var userswallow = await _context.Swallow.ToListAsync();
            ViewData["userswallow"] = userswallow;


            return View(await _context.UserSwallowSelection.ToListAsync());
        }

        // GET: UserSwallowSelections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var result = await _context.UserSwallowSelection.FirstOrDefaultAsync(x => x.Id == id);
            var userswallow = await _context.Swallow.ToListAsync();
            ViewData["userswallow"] = userswallow;


            int indx2 = 0;



            foreach (var graFood in userswallow)
            {

                if (result.UserSwallowId == graFood.Id)
                {
                    ViewData["indUserSwallowSelection"] = indx2;
                    break;
                }
                indx2++;
            }
            if (id == null)
            {
                return NotFound();
            }

            var userSwallowsSelection = await _context.UserSwallowSelection
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userSwallowsSelection == null)
            {
                return NotFound();
            }

            return View(userSwallowsSelection);
        }

        // GET: UserSwallowSelections/Create
        public async Task<IActionResult> Create(string message)
        {
            ViewBag.Error = message;
            var userswallow = await _context.Swallow.ToListAsync();
            ViewData["userswallow"] = userswallow;
            return View();
        }

        // POST: UserSwallowSelections/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,UserSwallowId")] UserSwallowSelection userSwallowsSelection, int grainselect)
        {

            userSwallowsSelection.UserId = 1;
            //userSwallowsSelection.UserSwallowsId = grainselect;
            if (ModelState.IsValid)
            {
                var item = await _context.UserSwallowSelection.FirstOrDefaultAsync(x => x.UserSwallowId == userSwallowsSelection.UserSwallowId);
                if (item == null)
                {
                    _context.Add(userSwallowsSelection);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // ModelState.AddModelError("", "Swallows alreay exist");
                    var errorMessage = "Swallows alreay exist";
                    //return RedirectToAction(nameof(Create));
                    return RedirectToAction("Index", new { message = errorMessage });
                }


            }
            return View(userSwallowsSelection);
        }

        // GET: UserSwallowSelections/Edit/5
        public async Task<IActionResult> Edit(int? id, string message)
        {
            ViewBag.Error = message;
            var userswallow = await _context.Swallow.ToListAsync();
            ViewData["userswallow"] = userswallow;
            if (id == null)
            {
                return NotFound();
            }

            var userSwallowsSelection = await _context.UserSwallowSelection.FindAsync(id);
            if (userSwallowsSelection == null)
            {
                return NotFound();
            }
            return View(userSwallowsSelection);
        }

        // POST: UserSwallowSelections/Edit/5



        [HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,UserSwallowsId")] UserSwallowSelection userSwallowsSelection, int grainselect)
        public async Task<JsonResult> Edit(int id, [Bind("Id,UserId,UserSwallowId")] UserSwallowSelection userSwallowsSelection, int grainselect)
        {
            // userSwallowsSelection.UserId = 1;
            //userSwallowsSelection.UserSwallowsId = grainselect;
            if (id != userSwallowsSelection.Id)
            {
                return new JsonResult(new { status = 0, message = "Item wasn't found" });
                //return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var item = await _context.UserSwallowSelection.FirstOrDefaultAsync(x => x.UserSwallowId == userSwallowsSelection.UserSwallowId && x.Id != userSwallowsSelection.Id);
                    if (item == null)
                    {
                        _context.Update(userSwallowsSelection);
                        await _context.SaveChangesAsync();
                        return new JsonResult(new { status = 1, message = "Your request was processed successfully" });
                    }
                    else
                    {
                        return new JsonResult(new { status = 0, message = "Your request already exist" });
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserSwallowSelectionExists(userSwallowsSelection.Id))
                    {
                        //return NotFound();
                        return new JsonResult(new { status = 0, message = "Item wasn't found" });
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
            }
            //return View(userSwallowsSelection);
            return new JsonResult(new { status = 0, message = "Check your entries" });
        }

        // GET: UserSwallowSelections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var result = await _context.UserSwallowSelection.FirstOrDefaultAsync(x => x.Id == id);
            var userswallow = await _context.Swallow.ToListAsync();
            ViewData["userswallow"] = userswallow;


            int indx2 = 0;



            foreach (var graFood in userswallow)
            {

                if (result.UserSwallowId == graFood.Id)
                {
                    ViewData["indUserSwallowSelection"] = indx2;
                    break;
                }
                indx2++;
            }

            if (id == null)
            {
                return NotFound();
            }

            var userSwallowsSelection = await _context.UserSwallowSelection
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userSwallowsSelection == null)
            {
                return NotFound();
            }

            return View(userSwallowsSelection);
        }

        // POST: UserSwallowSelections/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userSwallowsSelection = await _context.UserSwallowSelection.FindAsync(id);
            _context.UserSwallowSelection.Remove(userSwallowsSelection);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserSwallowSelectionExists(int id)
        {
            return _context.UserSwallowSelection.Any(e => e.Id == id);
        }
    }
}
