using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodPlanner.Interfaces;
using Identity.Migrations;
using Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    //[Authorize(Roles = "Manager")]
    public class HomeController : Controller
    {

        private readonly Identity.Models.AppIdentityDbContext _context;
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;
        private IUserLoginStatus _userLoginStatus;

        public HomeController(UserManager<AppUser> userMgr, IUserLoginStatus userLoginStatus, AppIdentityDbContext context, SignInManager<AppUser> signinMgr)
        {
            signInManager = signinMgr;
            _context = context;
            userManager = userMgr;
            _userLoginStatus = userLoginStatus;
        }

        /*[Authorize]
        public IActionResult Index()
        {
            return View((object)"Hello");
        }*/
        
       // [Authorize]
      
       

        
        //[Authorize]
        public async Task<IActionResult> Index(Login login)
        {
            
           
            //{
                //
            ////}
            //AppUser appUser = await userManager.FindByNameAsync(User.Identity.Name);


            //var getrole = userManager.GetRolesAsync(appUser);
            //if (getrole.Result[0] == "User")
            //{
            //    return RedirectToAction("Index", "UserHome");
            //}
            List<bool> sig = new List<bool>();
            //int user = await userManager;
            var userCount = userManager.Users.Count();

            var signedinStatus = _userLoginStatus.GetStatus();
            //foreach(var item in signedin.Result)
            //{
            //    if(item == true)
            //    {
            //        sig.Add(item);
            //    }
            //}



            ViewBag.totalUser = userCount;
            ViewBag.totalSignIn = signedinStatus.Result.Item1;
            ViewBag.totalSignOut = signedinStatus.Result.Item2;

            //string message = "Hello " + user.UserName;
            return View();
        }
       
    }
}