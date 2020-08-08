using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodPlanner.Models.FoodSchedulerTimeStarts;
using Identity.Models;
using FoodPlanner.Interface;
using FoodPlanner.Interfaces;
using Microsoft.AspNetCore.Identity;
using FoodPlanner.Models.UserPlScheduler;

namespace FoodPlanner.Controllers
{
    public class FoodSchedulerTimeStartsController : Controller
    {
        private readonly AppIdentityDbContext _context;
        private ISchedule _scheduleManager;
        private ISoupFrequency _soupfrequencymanager;
        private IOperation _scheduleoperation;
        private UserManager<AppUser> _userManager;
        private RoleManager<IdentityRole> roleManager;
        private SignInManager<AppUser> signInManager;

        public FoodSchedulerTimeStartsController(AppIdentityDbContext context, RoleManager<IdentityRole> roleMgr, ISoupFrequency soupfrequencymanager, ISchedule scheduleManager, IOperation scheduleoperation, UserManager<AppUser> userManager, SignInManager<AppUser> signinMgr)
        {
            _context = context;
            _scheduleManager = scheduleManager;
            _soupfrequencymanager = soupfrequencymanager;
            _scheduleoperation = scheduleoperation;
            _userManager = userManager;
            roleManager = roleMgr;
            signInManager = signinMgr;
        }

        // GET: FoodSchedulerTimeStarts
        public async Task<IActionResult> Index()
        {
            AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.userId = appUser.Id;
            var checkExist = await _context.UserPlScheduler.Where(x => x.UserId == appUser.Id).Select(x => x.UserId).ToListAsync();
            if (checkExist.Count == 0)
            {
                return RedirectToAction("Create", "FoodSchedulerTimeStarts");
            }
            else 
            {
                return View(await _context.UserPlScheduler.ToListAsync());
            }
            
           
        }

        // GET: FoodSchedulerTimeStarts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodSchedulerTimeStarts = await _context.FoodSchedulerTimeStarts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (foodSchedulerTimeStarts == null)
            {
                return NotFound();
            }

            return View(foodSchedulerTimeStarts);
        }

        // GET: FoodSchedulerTimeStarts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FoodSchedulerTimeStarts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( DateTime dateTime)
        {
            AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);
            
            var FoodList = new UserPlSchedulers
            {
                
                UserId = appUser.Id,
                StartTime = dateTime
            };
            var createStartTime = await _scheduleoperation.ÙpdateStartDateTime(FoodList);


            return RedirectToAction("Index", "FoodSchedulerTimeStarts");
        }

        // GET: FoodSchedulerTimeStarts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodSchedulerTimeStarts = await _context.FoodSchedulerTimeStarts.FindAsync(id);
            if (foodSchedulerTimeStarts == null)
            {
                return NotFound();
            }
            return View(foodSchedulerTimeStarts);
        }

        // POST: FoodSchedulerTimeStarts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] FoodSchedulerTimeStarts foodSchedulerTimeStarts)
        {
            if (id != foodSchedulerTimeStarts.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(foodSchedulerTimeStarts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodSchedulerTimeStartsExists(foodSchedulerTimeStarts.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(foodSchedulerTimeStarts);
        }

        // GET: FoodSchedulerTimeStarts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodSchedulerTimeStarts = await _context.FoodSchedulerTimeStarts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (foodSchedulerTimeStarts == null)
            {
                return NotFound();
            }

            return View(foodSchedulerTimeStarts);
        }

        // POST: FoodSchedulerTimeStarts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var foodSchedulerTimeStarts = await _context.FoodSchedulerTimeStarts.FindAsync(id);
            _context.FoodSchedulerTimeStarts.Remove(foodSchedulerTimeStarts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodSchedulerTimeStartsExists(int id)
        {
            return _context.FoodSchedulerTimeStarts.Any(e => e.Id == id);
        }
    }
}
