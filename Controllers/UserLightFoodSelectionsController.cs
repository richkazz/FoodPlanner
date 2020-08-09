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
using FoodPlanner.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace FoodPlanner.Controllers
{
    public class UserLightFoodSelectionsController : Controller
    {
        private readonly AppIdentityDbContext _context;
        private ISchedule _scheduleManager;
        private IOperation _scheduleoperation;
        private ISoupFrequency _soupfrequencymanager;

        private UserManager<AppUser> _userManager;
        private RoleManager<IdentityRole> roleManager;
        private SignInManager<AppUser> signInManager;

        public UserLightFoodSelectionsController(AppIdentityDbContext context, ISoupFrequency soupfrequencymanager, RoleManager<IdentityRole> roleMgr, ISchedule scheduleManager, IOperation scheduleoperation, UserManager<AppUser> userManager, SignInManager<AppUser> signinMgr)
        {
            _context = context;
            _scheduleManager = scheduleManager;
            _soupfrequencymanager = soupfrequencymanager;

            _scheduleoperation = scheduleoperation;
            _userManager = userManager;
            roleManager = roleMgr;
            signInManager = signinMgr;
        }

        // GET: UserLightFoodSelections
        public async Task<IActionResult> Index(string message)
        {

            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.Error = message;
            var checkExist = await _context.UserLightFoodSelection.Where(x => x.UserId == user.Id).Select(x => x.UserLightFoodId).ToListAsync();

            var userlightfood = await _context.LightFood.ToListAsync();
            ViewData["userlightfood"] = userlightfood;
            ViewBag.userIdPass = checkExist;
            ViewBag.userId = user.Id;

            return View(await _context.UserLightFoodSelection.ToListAsync());
        }

        // GET: UserLightFoodSelections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var result = await _context.UserLightFoodSelection.FirstOrDefaultAsync(x => x.Id == id);
            var userlightfood = await _context.LightFood.ToListAsync();
            ViewData["userlightfood"] = userlightfood;


            int indx2 = 0;



            foreach (var graFood in userlightfood)
            {

                if (result.UserLightFoodId == graFood.Id)
                {
                    ViewData["indUserLightFoodSelection"] = indx2;
                    break;
                }
                indx2++;
            }
            if (id == null)
            {
                return NotFound();
            }

            var userLightFoodSelection = await _context.UserLightFoodSelection
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userLightFoodSelection == null)
            {
                return NotFound();
            }

            return View(userLightFoodSelection);
        }

        // GET: UserLightFoodSelections/Create
        public async Task<IActionResult> create(string message)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.passUserId = user.Id;
            ViewBag.Error = message;
            var userlightfood = await _context.LightFood.ToListAsync();
            ViewData["userlightfood"] = userlightfood;
            return View();
        }

        // POST: UserLightFoodSelections/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,UserLightFoodId")] UserLightFoodSelection userLightFoodSelection, int grainselect)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
             
            userLightFoodSelection.UserId = user.Id;
            //userLightFoodSelection.UserLightFoodId = grainselect;
            if (userLightFoodSelection.Id>=0&userLightFoodSelection.UserId!=null&userLightFoodSelection.UserLightFoodId!=0)
            {
                var item = await _context.UserLightFoodSelection.Where(x=>x.UserId==user.Id).FirstOrDefaultAsync(x => x.UserLightFoodId == userLightFoodSelection.UserLightFoodId);
                if (item == null)
                {
                    _context.Add(userLightFoodSelection);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // ModelState.AddModelError("", "Light food alreay exist");
                    var errorMessage = "Light food alreay exist";
                    //return RedirectToAction(nameof(Create));
                    return RedirectToAction("Index", new { message = errorMessage });
                }


            }
            return View(userLightFoodSelection);
        }

        // GET: UserLightFoodSelections/Edit/5
        public async Task<IActionResult> Edit(int? id, string message)
        {
            ViewBag.Error = message;
            var userlightfood = await _context.LightFood.ToListAsync();
            ViewData["userlightfood"] = userlightfood;
            if (id == null)
            {
                return NotFound();
            }

            var userLightFoodSelection = await _context.UserLightFoodSelection.FindAsync(id);
            if (userLightFoodSelection == null)
            {
                return NotFound();
            }
            return View(userLightFoodSelection);
        }

        // POST: UserLightFoodSelections/Edit/5



        [HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,UserLightFoodId")] UserLightFoodSelection userLightFoodSelection, int grainselect)
        public async Task<JsonResult> Edit(int id, [Bind("Id,UserId,UserLightFoodId")] UserLightFoodSelection userLightFoodSelection, int grainselect)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            var checkExist = await _context.UserLightFoodSelection.Where(x => x.UserId == user.Id).Select(x => x.UserLightFoodId).ToListAsync();


            List<int> checkEqual = new List<int>();
            var checkIs = await _context.UserLightFoodSelection.Where(x => x.UserId == user.Id).Where(x => x.Id == id).Select(x => x.UserLightFoodId).ToListAsync();
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
                    var item = await _context.UserLightFoodSelection.Where(x=>x.UserId==user.Id).Select(x => x.UserLightFoodId == userLightFoodSelection.UserLightFoodId/* && x.Id != userLightFoodSelection.Id*/).ToListAsync();
                    if (!checkExist.Contains(userLightFoodSelection.UserLightFoodId))
                    {
                        _context.Update(userLightFoodSelection);
                        await _context.SaveChangesAsync();
                        return new JsonResult(new { status = 1, message = "Your request was processed successfully" });
                    }
                    else
                    {
                        return new JsonResult(new { status = 0, message = "The light food already exist" });
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserLightFoodSelectionExists(userLightFoodSelection.Id))
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
            //return View(userLightFoodSelection);
            return new JsonResult(new { status = 0, message = "Check your entries" });
        }

        // GET: UserLightFoodSelections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var result = await _context.UserLightFoodSelection.FirstOrDefaultAsync(x => x.Id == id);
            var userlightfood = await _context.LightFood.ToListAsync();
            ViewData["userlightfood"] = userlightfood;


            int indx2 = 0;



            foreach (var graFood in userlightfood)
            {

                if (result.UserLightFoodId == graFood.Id)
                {
                    ViewData["indUserLightFoodSelection"] = indx2;
                    break;
                }
                indx2++;
            }

            if (id == null)
            {
                return NotFound();
            }

            var userLightFoodSelection = await _context.UserLightFoodSelection
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userLightFoodSelection == null)
            {
                return NotFound();
            }

            return View(userLightFoodSelection);
        }

        // POST: UserLightFoodSelections/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userLightFoodSelection = await _context.UserLightFoodSelection.FindAsync(id);
            _context.UserLightFoodSelection.Remove(userLightFoodSelection);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserLightFoodSelectionExists(int id)
        {
            return _context.UserLightFoodSelection.Any(e => e.Id == id);
        }
    }
}