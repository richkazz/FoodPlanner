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
    public class UserSoupSelectionsController : Controller
    {
        private readonly AppIdentityDbContext _context;

        public UserSoupSelectionsController(AppIdentityDbContext context)
        {
            _context = context;
        }

        // GET: UserSoupSelections
        public async Task<IActionResult> Index(string message)
        {


            ViewBag.Error = message;
            var userswallow = await _context.Soup.ToListAsync();
            ViewData["userswallow"] = userswallow;


            return View(await _context.UserSoupSelection.ToListAsync());
        }

        // GET: UserSoupSelections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var result = await _context.UserSoupSelection.FirstOrDefaultAsync(x => x.Id == id);
            var userswallow = await _context.Soup.ToListAsync();
            ViewData["userswallow"] = userswallow;


            int indx2 = 0;



            foreach (var graFood in userswallow)
            {

                if (result.UserSoupId == graFood.Id)
                {
                    ViewData["indUserSoupSelection"] = indx2;
                    break;
                }
                indx2++;
            }
            if (id == null)
            {
                return NotFound();
            }

            var userSoupsSelection = await _context.UserSoupSelection
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userSoupsSelection == null)
            {
                return NotFound();
            }

            return View(userSoupsSelection);
        }

        // GET: UserSoupSelections/Create
        public async Task<IActionResult> Create(string message)
        {
            ViewBag.Error = message;
            var userswallow = await _context.Soup.ToListAsync();
            ViewData["userswallow"] = userswallow;
            return View();
        }

        // POST: UserSoupSelections/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,UserSoupId")] UserSoupSelection userSoupsSelection, int grainselect)
        {

            userSoupsSelection.UserId = 1;
            //userSoupsSelection.UserSoupsId = grainselect;
            if (ModelState.IsValid)
            {
                var item = await _context.UserSoupSelection.FirstOrDefaultAsync(x => x.UserSoupId == userSoupsSelection.UserSoupId);
                if (item == null)
                {
                    _context.Add(userSoupsSelection);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // ModelState.AddModelError("", "Soups alreay exist");
                    var errorMessage = "Soups alreay exist";
                    //return RedirectToAction(nameof(Create));
                    return RedirectToAction("Index", new { message = errorMessage });
                }


            }
            return View(userSoupsSelection);
        }

        // GET: UserSoupSelections/Edit/5
        public async Task<IActionResult> Edit(int? id, string message)
        {
            ViewBag.Error = message;
            var userswallow = await _context.Soup.ToListAsync();
            ViewData["userswallow"] = userswallow;
            if (id == null)
            {
                return NotFound();
            }

            var userSoupsSelection = await _context.UserSoupSelection.FindAsync(id);
            if (userSoupsSelection == null)
            {
                return NotFound();
            }
            return View(userSoupsSelection);
        }

        // POST: UserSoupSelections/Edit/5



        [HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,UserSoupsId")] UserSoupSelection userSoupsSelection, int grainselect)
        public async Task<JsonResult> Edit(int id, [Bind("Id,UserId,UserSoupId")] UserSoupSelection userSoupsSelection, int grainselect)
        {
            // userSoupsSelection.UserId = 1;
            //userSoupsSelection.UserSoupsId = grainselect;
            if (id != userSoupsSelection.Id)
            {
                return new JsonResult(new { status = 0, message = "Item wasn't found" });
                //return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var item = await _context.UserSoupSelection.FirstOrDefaultAsync(x => x.UserSoupId == userSoupsSelection.UserSoupId && x.Id != userSoupsSelection.Id);
                    if (item == null)
                    {
                        _context.Update(userSoupsSelection);
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
                    if (!UserSoupSelectionExists(userSoupsSelection.Id))
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
            //return View(userSoupsSelection);
            return new JsonResult(new { status = 0, message = "Check your entries" });
        }

        // GET: UserSoupSelections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var result = await _context.UserSoupSelection.FirstOrDefaultAsync(x => x.Id == id);
            var userswallow = await _context.Soup.ToListAsync();
            ViewData["userswallow"] = userswallow;


            int indx2 = 0;



            foreach (var graFood in userswallow)
            {

                if (result.UserSoupId == graFood.Id)
                {
                    ViewData["indUserSoupSelection"] = indx2;
                    break;
                }
                indx2++;
            }

            if (id == null)
            {
                return NotFound();
            }

            var userSoupsSelection = await _context.UserSoupSelection
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userSoupsSelection == null)
            {
                return NotFound();
            }

            return View(userSoupsSelection);
        }

        // POST: UserSoupSelections/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userSoupsSelection = await _context.UserSoupSelection.FindAsync(id);
            _context.UserSoupSelection.Remove(userSoupsSelection);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserSoupSelectionExists(int id)
        {
            return _context.UserSoupSelection.Any(e => e.Id == id);
        }
    }
}
