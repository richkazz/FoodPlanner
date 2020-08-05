using FoodPlanner.Interfaces;
using Identity.Migrations;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.Services
{
    public class UserLoginStatusService : IUserLoginStatus
    {
        private readonly AppIdentityDbContext _context;
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;
        private IEmailSender _emailSender;
        private RoleManager<IdentityRole> roleManager;

        public UserLoginStatusService(AppIdentityDbContext context, IEmailSender emailSender, UserManager<AppUser> userMgr, SignInManager<AppUser> signinMgr, RoleManager<IdentityRole> roleMgr)
        {
            _context = context;
            userManager = userMgr;
            signInManager = signinMgr;
            roleManager = roleMgr;
            _emailSender = emailSender;
        }

        public async Task<(long, long)> GetStatus()

        {
            var IsSignInCount = userManager.Users.Where(x =>  x.IsUserLoggedIn == true).ToList().Count();

            var IsSignOutCount = userManager.Users.Where(x =>  x.IsUserLoggedIn == false).ToList().Count();
            

            return (IsSignInCount, IsSignOutCount);
            
        }

        public async Task<bool> UpdatStaus(string id, bool isLogin)
        {

            var checkExist = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (checkExist != null)
            {

                checkExist.IsUserLoggedIn = isLogin;
                
                await _context.SaveChangesAsync();
                return true;
            }
            return false;

            
        }
    }
}
