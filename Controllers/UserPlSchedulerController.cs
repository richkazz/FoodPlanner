using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodPlanner.Interface;
using FoodPlanner.Interfaces;
using FoodPlanner.Models.UserPlScheduler;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodPlanner.Controllers
{
    public class UserPlSchedulerController : Controller
    {
        private readonly AppIdentityDbContext _context;
        private ISchedule _scheduleManager;
        private IOperation _scheduleoperation;
        private ISoupFrequency _soupfrequencymanager;

        private UserManager<AppUser> _userManager;
        private RoleManager<IdentityRole> roleManager;
        private SignInManager<AppUser> signInManager;

        public UserPlSchedulerController(AppIdentityDbContext context, ISoupFrequency soupfrequencymanager, RoleManager<IdentityRole> roleMgr, ISchedule scheduleManager, IOperation scheduleoperation, UserManager<AppUser> userManager, SignInManager<AppUser> signinMgr)
        {
            _context = context;
            _scheduleManager = scheduleManager;
            _soupfrequencymanager = soupfrequencymanager;

            _scheduleoperation = scheduleoperation;
            _userManager = userManager;
            roleManager = roleMgr;
            signInManager = signinMgr;
        }





        public async Task<IActionResult> DailyScheduler(string id)
        {


            List<string> Checkfoodlist = new List<string>();
            List<string> combinedlist = new List<string>();
            List<string> timeresult = new List<string>();
            List<string> combinedlistsplit = new List<string>();
            List<DateTime> savetime = new List<DateTime>();

            IdentityRole role = await roleManager.FindByIdAsync(id);
            List<AppUser> members = new List<AppUser>();
            List<AppUser> nonMembers = new List<AppUser>();
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            var randomise = await _scheduleManager.OrderOne();
            combinedlist = randomise;
            var comperuser = await _scheduleoperation.FetchFoodByTime(user.Id);
                if (comperuser == null)
                {

                    string combinedFoodList =
                      (combinedlist[0] + "#" + combinedlist[1] + "#" + combinedlist[2] + "#" + combinedlist[3] + "#" + combinedlist[4] + "#" + combinedlist[5] + "#" + combinedlist[6]);

                    //AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                    var starttime = await _context.FoodSchedulerTimeStarts.ToListAsync();
                    foreach (var item in starttime)
                    {
                        savetime.Add(item.Name.Date);
                    }
                    DateTime t = savetime[0];
                    var FoodList = new UserPlSchedulers
                    {
                        FoodList = combinedFoodList,
                        UserId = user.Id,
                        StartTime = t
                    };
                    var createFoodList = await _scheduleoperation.CreateFood(FoodList);
                }
            
            

            AppUser users = await _userManager.FindByNameAsync(User.Identity.Name);
           

            
            var newlist = await _scheduleManager.SplitFoodList(users.Id);
            combinedlistsplit = newlist;



            if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
            {
                var Monday = combinedlistsplit[0].Split(new char[] { '|' });
                ViewBag.dayofweek = "Monday";
                ViewBag.Monday0 = Monday[0]; ViewBag.Monday1 = Monday[1]; ViewBag.Monday2 = Monday[2];
            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday)
            {
                var Monday = combinedlistsplit[1].Split(new char[] { '|' });
                ViewBag.dayofweek = "Tuesday";
                ViewBag.Monday0 = Monday[0]; ViewBag.Monday1 = Monday[1]; ViewBag.Monday2 = Monday[2];
            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday)
            {
                var Monday = combinedlistsplit[2].Split(new char[] { '|' });
                ViewBag.dayofweek = "Wednesday";
                ViewBag.Monday0 = Monday[0]; ViewBag.Monday1 = Monday[1]; ViewBag.Monday2 = Monday[2];
            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Thursday)
            {
                var Monday = combinedlistsplit[3].Split(new char[] { '|' });
                ViewBag.dayofweek = "Thursday";
                ViewBag.Monday0 = Monday[0]; ViewBag.Monday1 = Monday[1]; ViewBag.Monday2 = Monday[2];
            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                var Monday = combinedlistsplit[4].Split(new char[] { '|' });
                ViewBag.dayofweek = "Friday";
                ViewBag.Monday0 = Monday[0]; ViewBag.Monday1 = Monday[1]; ViewBag.Monday2 = Monday[2];
            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
            {
                var Monday = combinedlistsplit[5].Split(new char[] { '|' });
                ViewBag.dayofweek = "Saturday";
                ViewBag.Monday0 = Monday[0]; ViewBag.Monday1 = Monday[1]; ViewBag.Monday2 = Monday[2];
            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                var Monday = combinedlistsplit[6].Split(new char[] { '|' });
                ViewBag.dayofweek = "Sunday";
                ViewBag.Monday0 = Monday[0]; ViewBag.Monday1 = Monday[1]; ViewBag.Monday2 = Monday[2];
            }





            return View();
        }
        public async Task<IActionResult> WeeklyScheduler()
        {
            List<string> Checkfoodlist = new List<string>();
            List<string> combinedlist = new List<string>();
            List<string> timeresult = new List<string>();
            List<string> combinedlistsplit = new List<string>();
            List<DateTime> savetime = new List<DateTime>();

            var randomise = await _scheduleManager.OrderOne();
            combinedlist = randomise;
            AppUser users = await _userManager.FindByNameAsync(User.Identity.Name);
            var Check = await _context.UserPlScheduler.Where
                (
                x => x.UserId == users.Id
                ).Select(x => x.SoupFrequency
                ).ToListAsync();

            var comperuser = await _scheduleoperation.FetchFoodByTime(users.Id);
            if (comperuser == null)
            {
                string combinedFoodList =
                  (combinedlist[0] + "#" + combinedlist[1] + "#" + combinedlist[2] + "#" + combinedlist[3] + "#" + combinedlist[4] + "#" + combinedlist[5] + "#" + combinedlist[6]);

                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                var starttime = await _context.FoodSchedulerTimeStarts.ToListAsync();
                foreach (var item in starttime)
                {


                    savetime.Add(item.Name.Date);


                }
                DateTime t = savetime[0];
                var FoodList = new UserPlSchedulers
                {
                    FoodList = combinedFoodList,
                    UserId = user.Id,
                    StartTime = t
                };
                var createFoodList = await _scheduleoperation.CreateFood(FoodList);

                var newlist = await _scheduleManager.SplitFoodList(User.Identity.Name);
                combinedlistsplit = newlist;


                var Monday = combinedlistsplit[0].Split(new char[] { '|' });
                var Tuesday = combinedlistsplit[1].Split(new char[] { '|' });
                var Wednesday = combinedlistsplit[2].Split(new char[] { '|' });
                var Thursday = combinedlistsplit[3].Split(new char[] { '|' });
                var Friday = combinedlistsplit[4].Split(new char[] { '|' });
                var Saturday = combinedlistsplit[5].Split(new char[] { '|' });
                var Sunday = combinedlistsplit[6].Split(new char[] { '|' });


                ViewBag.Monday0 = Monday[0]; ViewBag.Monday1 = Monday[1]; ViewBag.Monday2 = Monday[2];

                ViewBag.Tuesday0 = Tuesday[0]; ViewBag.Tuesday1 = Tuesday[1]; ViewBag.Tuesday2 = Tuesday[2];

                ViewBag.Wednesday0 = Wednesday[0]; ViewBag.Wednesday1 = Wednesday[1]; ViewBag.Wednesday2 = Wednesday[2];

                ViewBag.Thursday0 = Thursday[0]; ViewBag.Thursday1 = Thursday[1]; ViewBag.Thursday2 = Thursday[2];

                ViewBag.Friday0 = Friday[0]; ViewBag.Friday1 = Friday[1]; ViewBag.Friday2 = Friday[2];

                ViewBag.Saturday0 = Saturday[0]; ViewBag.Saturday1 = Saturday[1]; ViewBag.Saturday2 = Saturday[2];

                ViewBag.Sunday0 = Sunday[0]; ViewBag.Sunday1 = Sunday[1]; ViewBag.Sunday2 = Sunday[2];

                return View();
            }

            var time = await _scheduleManager.ComperTime(User.Identity.Name);







            timeresult = time;
            var timecheck = timeresult[0].Split(new char[] { '|' });


            var databasetime = timecheck[0]; var presenttime = timecheck[1];




            DateTime intdatabasetime = DateTime.Parse(databasetime);
            DateTime intpresenttime = DateTime.Parse(presenttime);


            if (intdatabasetime < intpresenttime)
            {
                var timeupdate = DateTime.Now;

                var FoodList = new UserPlSchedulers
                {

                    StartTime = timeupdate,

                };
                var updateFoodList = await _scheduleoperation.ÙpdateFood(FoodList);

            }

            if (databasetime == presenttime)
            {
                var startSoupFrquency = await _soupfrequencymanager.StartSoupFrequencyProcess(users.Id,Check[0]);

                string combinedFoodList =
                   (combinedlist[0] + "#" + combinedlist[1] + "#" + combinedlist[2] + "#" + combinedlist[3] + "#" + combinedlist[4] + "#" + combinedlist[5] + "#" + combinedlist[6]);

                //var user = _userManager.FindByNameAsync(User.Identity.Name);


                var timeupdate = intdatabasetime.AddDays(6);

                var FoodList = new UserPlSchedulers
                {
                    FoodList = combinedFoodList,
                    StartTime = timeupdate
                };
                var updateFoodList = await _scheduleoperation.ÙpdateFood(FoodList);

                var newlist = await _scheduleManager.SplitFoodList(User.Identity.Name);
                combinedlistsplit = newlist;


                var Monday = combinedlistsplit[0].Split(new char[] { '|' });
                var Tuesday = combinedlistsplit[1].Split(new char[] { '|' });
                var Wednesday = combinedlistsplit[2].Split(new char[] { '|' });
                var Thursday = combinedlistsplit[3].Split(new char[] { '|' });
                var Friday = combinedlistsplit[4].Split(new char[] { '|' });
                var Saturday = combinedlistsplit[5].Split(new char[] { '|' });
                var Sunday = combinedlistsplit[6].Split(new char[] { '|' });


                ViewBag.Monday0 = Monday[0]; ViewBag.Monday1 = Monday[1]; ViewBag.Monday2 = Monday[2];

                ViewBag.Tuesday0 = Tuesday[0]; ViewBag.Tuesday1 = Tuesday[1]; ViewBag.Tuesday2 = Tuesday[2];

                ViewBag.Wednesday0 = Wednesday[0]; ViewBag.Wednesday1 = Wednesday[1]; ViewBag.Wednesday2 = Wednesday[2];

                ViewBag.Thursday0 = Thursday[0]; ViewBag.Thursday1 = Thursday[1]; ViewBag.Thursday2 = Thursday[2];

                ViewBag.Friday0 = Friday[0]; ViewBag.Friday1 = Friday[1]; ViewBag.Friday2 = Friday[2];

                ViewBag.Saturday0 = Saturday[0]; ViewBag.Saturday1 = Saturday[1]; ViewBag.Saturday2 = Saturday[2];

                ViewBag.Sunday0 = Sunday[0]; ViewBag.Sunday1 = Sunday[1]; ViewBag.Sunday2 = Sunday[2];
            }
            else
            {
                var newlist = await _scheduleManager.SplitFoodList(User.Identity.Name);
                combinedlistsplit = newlist;


                var Monday = combinedlistsplit[0].Split(new char[] { '|' });
                var Tuesday = combinedlistsplit[1].Split(new char[] { '|' });
                var Wednesday = combinedlistsplit[2].Split(new char[] { '|' });
                var Thursday = combinedlistsplit[3].Split(new char[] { '|' });
                var Friday = combinedlistsplit[4].Split(new char[] { '|' });
                var Saturday = combinedlistsplit[5].Split(new char[] { '|' });
                var Sunday = combinedlistsplit[6].Split(new char[] { '|' });


                ViewBag.Monday0 = Monday[0]; ViewBag.Monday1 = Monday[1]; ViewBag.Monday2 = Monday[2];

                ViewBag.Tuesday0 = Tuesday[0]; ViewBag.Tuesday1 = Tuesday[1]; ViewBag.Tuesday2 = Tuesday[2];

                ViewBag.Wednesday0 = Wednesday[0]; ViewBag.Wednesday1 = Wednesday[1]; ViewBag.Wednesday2 = Wednesday[2];

                ViewBag.Thursday0 = Thursday[0]; ViewBag.Thursday1 = Thursday[1]; ViewBag.Thursday2 = Thursday[2];

                ViewBag.Friday0 = Friday[0]; ViewBag.Friday1 = Friday[1]; ViewBag.Friday2 = Friday[2];

                ViewBag.Saturday0 = Saturday[0]; ViewBag.Saturday1 = Saturday[1]; ViewBag.Saturday2 = Saturday[2];

                ViewBag.Sunday0 = Sunday[0]; ViewBag.Sunday1 = Sunday[1]; ViewBag.S3unday2 = Sunday[2];

            }


            {


                return View();
            }

        }
        public IActionResult MonthlyScheduler()
        {
            return View();
        }
        //public Tuple<(string[], string[], string[], string[], string[], string[], string[]> Schedule(List<string> combinedlist)
        //{
        //    List<string> combin = new List<string>();
        //    var Monday = combinedlist[0].Split(new char[] { '|' });
        //    var Tuesday = combinedlist[1].Split(new char[] { '|' });
        //    var Wednesday = combinedlist[2].Split(new char[] { '|' });
        //    var Thursday = combinedlist[3].Split(new char[] { '|' });
        //    var Friday = combinedlist[4].Split(new char[] { '|' });
        //    var Saturday = combinedlist[5].Split(new char[] { '|' });
        //    var Sunday = combinedlist[6].Split(new char[] { '|' });


        //    ViewBag.Monday0 = Monday[0]; ViewBag.Monday1 = Monday[1]; ViewBag.Monday2 = Monday[2];

        //    ViewBag.Tuesday0 = Tuesday[0]; ViewBag.Tuesday1 = Tuesday[1]; ViewBag.Tuesday2 = Tuesday[2];

        //    ViewBag.Wednesday0 = Wednesday[0]; ViewBag.Wednesday1 = Wednesday[1]; ViewBag.Wednesday2 = Wednesday[2];

        //    ViewBag.Thursday0 = Thursday[0]; ViewBag.Thursday1 = Thursday[1]; ViewBag.Thursday2 = Thursday[2];

        //    ViewBag.Friday0 = Friday[0]; ViewBag.Friday1 = Friday[1]; ViewBag.Friday2 = Friday[2];

        //    ViewBag.Saturday0 = Saturday[0]; ViewBag.Saturday1 = Saturday[1]; ViewBag.Saturday2 = Saturday[2];

        //    ViewBag.Sunday0 = Sunday[0]; ViewBag.Sunday1 = Sunday[1]; ViewBag.Sunday2 = Sunday[2];



        //    return CO;
        //}
    }
}