using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodPlanner.Models.UserFoodSelectionCategory;
using Identity.Models;
using FoodPlanner.Interface;
using Microsoft.AspNetCore.Identity;
using FoodPlanner.Interfaces;

namespace FoodPlanner.Controllers
{
    public class UserSwallowSelectionsController : Controller
    {
        private readonly AppIdentityDbContext _context;
        private ISchedule _scheduleManager;
        private IOperation _scheduleoperation;
        private ISoupFrequency _soupfrequencymanager;

        private UserManager<AppUser> _userManager;
        private RoleManager<IdentityRole> roleManager;
        private SignInManager<AppUser> signInManager;

        public UserSwallowSelectionsController(AppIdentityDbContext context, ISoupFrequency soupfrequencymanager, RoleManager<IdentityRole> roleMgr, ISchedule scheduleManager, IOperation scheduleoperation, UserManager<AppUser> userManager, SignInManager<AppUser> signinMgr)
        {
            _context = context;
            _scheduleManager = scheduleManager;
            _soupfrequencymanager = soupfrequencymanager;

            _scheduleoperation = scheduleoperation;
            _userManager = userManager;
            roleManager = roleMgr;
            signInManager = signinMgr;
        }

        // GET: UserSwallowSelections
        public async Task<IActionResult> Index(string message)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.Error = message;
            var userswallow = await _context.Swallow.ToListAsync();
            ViewData["userswallow"] = userswallow;
            ViewBag.userId = user.Id;

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
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            userSwallowsSelection.UserId = user.Id;
            //userSwallowsSelection.UserSwallowsId = grainselect;
            if (userSwallowsSelection.Id>=0&userSwallowsSelection.UserId!=null&userSwallowsSelection.UserSwallowId!=0)
            {
                var item = await _context.UserSwallowSelection.Where(x => x.UserId == user.Id).FirstOrDefaultAsync(x => x.UserSwallowId == userSwallowsSelection.UserSwallowId);
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
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            var checkExist = await _context.UserSwallowSelection.Where(x => x.UserId == user.Id).Select(x => x.UserSwallowId).ToListAsync();
            List<int> checkEqual = new List<int>();
            var checkIs = await _context.UserSwallowSelection.Where(x => x.UserId == user.Id).Where(x => x.Id == id).Select(x => x.UserSwallowId).ToListAsync();
            foreach (var item in checkExist)
            {
                if (item == checkIs[0])
                {
                    checkEqual.Add(id);
                }
            }
            // userGrainDishesSelection.UserId = 1;
            //userGrainDishesSelection.UserGrainDishesId = grainselect;
            if (checkExist.Contains(id) == true & checkEqual[0] == 0)
            {
                return new JsonResult(new { status = 0, message = "Item wasn't found" });
                //return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var item = await _context.UserSwallowSelection.FirstOrDefaultAsync(x => x.UserSwallowId == userSwallowsSelection.UserSwallowId && x.Id != userSwallowsSelection.Id);
                    if (!checkExist.Contains(userSwallowsSelection.UserSwallowId))
                    {
                        _context.Update(userSwallowsSelection);
                        await _context.SaveChangesAsync();
                        return new JsonResult(new { status = 1, message = "Your request was processed successfully" });
                    }
                    else
                    {
                        return new JsonResult(new { status = 0, message = "The swallow already exist" });
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
