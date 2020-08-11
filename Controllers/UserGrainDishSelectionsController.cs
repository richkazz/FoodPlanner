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
using Org.BouncyCastle.Math.EC.Rfc7748;
using Identity.Migrations;
using NToastNotify;
using FoodPlanner.Util;

namespace FoodPlanner.Controllers
{
    public class UserGrainDishSelectionsController : Controller
    {
        private readonly AppIdentityDbContext _context;
        private ISchedule _scheduleManager;
        private IOperation _scheduleoperation;
        private ISoupFrequency _soupfrequencymanager;
        private readonly IToastNotification _toastNotification;
        private UserManager<AppUser> _userManager;
        private RoleManager<IdentityRole> roleManager;
        private SignInManager<AppUser> signInManager;

        public UserGrainDishSelectionsController(AppIdentityDbContext context, IToastNotification toastNotification, ISoupFrequency soupfrequencymanager, RoleManager<IdentityRole> roleMgr, ISchedule scheduleManager, IOperation scheduleoperation, UserManager<AppUser> userManager, SignInManager<AppUser> signinMgr)
        {
            _context = context;
            _scheduleManager = scheduleManager;
            _soupfrequencymanager = soupfrequencymanager;
            _toastNotification = toastNotification;
            _scheduleoperation = scheduleoperation;
            _userManager = userManager;
            roleManager = roleMgr;
            signInManager = signinMgr;
        }

        // GET: UserGrainDishSelections
        public async Task<IActionResult> Index(string message)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.Error = message;
            var userswallow = await _context.GrainDish.ToListAsync();
            ViewData["userswallow"] = userswallow;
            ViewBag.userId = user.Id;

            return View(await _context.UserGrainDishSelection.ToListAsync());
        }

        // GET: UserGrainDishSelections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var result = await _context.UserGrainDishSelection.FirstOrDefaultAsync(x => x.Id == id);
            var userswallow = await _context.GrainDish.ToListAsync();
            ViewData["userswallow"] = userswallow;


            int indx2 = 0;



            foreach (var graFood in userswallow)
            {

                if (result.UserGrainDishId == graFood.Id)
                {
                    ViewData["indUserGrainDishSelection"] = indx2;
                    break;
                }
                indx2++;
            }
            if (id == null)
            {
                return NotFound();
            }

            var userGrainDishesSelection = await _context.UserGrainDishSelection
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userGrainDishesSelection == null)
            {
                return NotFound();
            }

            return View(userGrainDishesSelection);
        }

        // GET: UserGrainDishSelections/Create
        public async Task<IActionResult> Create(string message)
        {
            ViewBag.Error = message;
            var userswallow = await _context.GrainDish.ToListAsync();
            ViewData["userswallow"] = userswallow;
            return View();
        }

        // POST: UserGrainDishSelections/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,UserGrainDishId")] UserGrainDishSelection userGrainDishesSelection, int grainselect)
        {
            if (!ModelState.IsValid)
            {
                string msg = (ModelState.FirstOrDefault(x => x.Value.Errors.Any()).Value.Errors.FirstOrDefault().ErrorMessage).Replace("'", "");
                return new JsonResult(new { status = 0, message = msg });
            }
            
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

                var checkfood = _context.UserGrainDishSelection.Where(x => x.UserId == user.Id && x.UserGrainDishId == userGrainDishesSelection.UserGrainDishId).ToList().Count();
                if (checkfood == 0)
                {
                userGrainDishesSelection.UserId = user.Id;
                    _context.Add(userGrainDishesSelection);
                    await _context.SaveChangesAsync();
                _toastNotification.AddErrorToastMessage(ResponseMessageUtilities.CREATED_SUCESSFUL);
                return RedirectToAction(nameof(Index));
                }
                else
                {
                _toastNotification.AddErrorToastMessage(ResponseMessageUtilities.ITEM_EXIST);
                var errorMessage = "GrainDishes alreay exist";
                    //return RedirectToAction(nameof(Create));
                    return RedirectToAction("Index", new { message = errorMessage });
                }

        }

        // GET: UserGrainDishSelections/Edit/5
        public async Task<IActionResult> Edit(int? id, string message)
        {
            ViewBag.Error = message;
            var userswallow = await _context.GrainDish.ToListAsync();
            ViewData["userswallow"] = userswallow;
            if (id == null)
            {
                return NotFound();
            }

            var userGrainDishesSelection = await _context.UserGrainDishSelection.FindAsync(id);
            if (userGrainDishesSelection == null)
            {
                return NotFound();
            }
            return View(userGrainDishesSelection);
        }

        // POST: UserGrainDishSelections/Edit/5



        [HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,UserGrainDishesId")] UserGrainDishSelection userGrainDishesSelection, int grainselect)
        public async Task<JsonResult> Edit(int id, [Bind("Id,UserId,UserGrainDishId")]
        UserGrainDishSelection userGrainDishesSelection, int grainselect)
        {
            if (!ModelState.IsValid)
            {
                string msg = (ModelState.FirstOrDefault(x => x.Value.Errors.Any()).Value.Errors.FirstOrDefault().ErrorMessage).Replace("'", "");
                return new JsonResult(new { status = 0, message = msg });
            }
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                    var checkfoods = _context.UserGrainDishSelection.Where(x => x.Id != id && x.UserId == user.Id && x.UserGrainDishId == userGrainDishesSelection.UserGrainDishId).ToList().Count();
            
                try
                {
                    //if (checkid==0||checkfood==0)
                    if (checkfoods == 0)
                    {
                        var model = _context.UserGrainDishSelection.FirstOrDefault(x => x.Id == userGrainDishesSelection.Id);
                        model.UserGrainDishId = userGrainDishesSelection.UserGrainDishId;
                        await _context.SaveChangesAsync();
                        //_context.Update(userGrainDishesSelection);
                        //await _context.SaveChangesAsync();
                        return new JsonResult(new { status = 1, message = "Your request was processed successfully" });
                    }
                    else
                    {
                        return new JsonResult(new { status = 0, message = "The grain dish already exist" });
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserGrainDishSelectionExists(userGrainDishesSelection.Id))
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
            //return View(userGrainDishesSelection);
            return new JsonResult(new { status = 0, message = "Check your entries" });
        }

        // GET: UserGrainDishSelections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var result = await _context.UserGrainDishSelection.FirstOrDefaultAsync(x => x.Id == id);
            var userswallow = await _context.GrainDish.ToListAsync();
            ViewData["userswallow"] = userswallow;


            int indx2 = 0;



            foreach (var graFood in userswallow)
            {

                if (result.UserGrainDishId == graFood.Id)
                {
                    ViewData["indUserGrainDishSelection"] = indx2;
                    break;
                }
                indx2++;
            }

            if (id == null)
            {
                return NotFound();
            }

            var userGrainDishesSelection = await _context.UserGrainDishSelection
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userGrainDishesSelection == null)
            {
                return NotFound();
            }

            return View(userGrainDishesSelection);
        }

        // POST: UserGrainDishSelections/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userGrainDishesSelection = await _context.UserGrainDishSelection.FindAsync(id);
            _context.UserGrainDishSelection.Remove(userGrainDishesSelection);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserGrainDishSelectionExists(int id)
        {
            return _context.UserGrainDishSelection.Any(e => e.Id == id);
        }
    }
}
