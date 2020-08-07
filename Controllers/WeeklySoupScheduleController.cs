using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodPlanner.Interface;
using FoodPlanner.Interfaces;
using FoodPlanner.Models;
using FoodPlanner.Models.UserPlScheduler;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Math.EC.Rfc7748;

namespace FoodPlanner.Controllers
{
    public class WeeklySoupScheduleController : Controller
    {
    private readonly AppIdentityDbContext _context;
    private ISchedule _scheduleManager;
    private ISoupFrequency _soupfrequencymanager;
    private IOperation _scheduleoperation;
    private UserManager<AppUser> _userManager;
    private RoleManager<IdentityRole> roleManager;
    private SignInManager<AppUser> signInManager;

    public WeeklySoupScheduleController(AppIdentityDbContext context, RoleManager<IdentityRole> roleMgr, ISoupFrequency soupfrequencymanager, ISchedule scheduleManager, IOperation scheduleoperation, UserManager<AppUser> userManager, SignInManager<AppUser> signinMgr)
    {
        _context = context;
        _scheduleManager = scheduleManager;
            _soupfrequencymanager = soupfrequencymanager;
        _scheduleoperation = scheduleoperation;
        _userManager = userManager;
        roleManager = roleMgr;
        signInManager = signinMgr;
    }
   
        
        //get create
        public async Task<IActionResult> CreateAsync()
        {
            
            

            var getfrequency = await _soupfrequencymanager.GetSelectFrequency();
            var getSoupFrequency = getfrequency.Select(x => new SoupFrequency {
               SoupCount= x.SoupCount,
               Id = x.Id
            }).ToList();
            ViewData["graindish"] = getSoupFrequency;
            return View();
        }
        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(int item)
        {
            AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var Check =  _context.UserPlScheduler.Where(x => x.UserId == appUser.Id).Select(x => x.SoupFrequency).ToList();
            if(Check[0] == 0)
            {
                var startSoupFrquency = await _soupfrequencymanager.StartSoupFrequencyProcess(appUser.Id, item);

                var getSoupListWith = await _context.UserPlScheduler.Where(x => x.UserId == appUser.Id).Select(x => x.SoupList).ToListAsync();
                List<string> passValue = new List<string>();
                var Monday = getSoupListWith[0].Split(new char[] { '|' });
                foreach (var value in Monday)
                {
                    passValue.Add(value);
                }
                ViewBag.soupList1 = passValue;
            }
            var getSoupList = await _context.UserPlScheduler.Where(x => x.UserId == appUser.Id).Select(x => x.showSF).ToListAsync();

            if (getSoupList[0] == true)
            {

            }
            if (getSoupList[0] == false)
            {

            }

            if ( Check[0]!=0)
            {
                var getFromDb =await _context.UserPlScheduler.Where(x => x.UserId == appUser.Id).Select(x => x.SoupList).ToListAsync();
                var Monday = getFromDb[0].Split(new char[] { '|' });
                ViewBag.soupList1 = Monday;

            }







            return View();
        }
            public IActionResult Edit()
        {

            return View();
        }
    }
}