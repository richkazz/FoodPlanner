
using FoodPlanner.Email;
using FoodPlanner.Interfaces;
using Identity.Migrations;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace FoodPlanner.Controllers
{
    public class RegisterController : Controller
    {

        private readonly Identity.Models.AppIdentityDbContext _context;
        private RoleManager<IdentityRole> roleManager;

        private UserManager<AppUser> userManager;
        private IPasswordHasher<AppUser> passwordHasher;
        private IPasswordValidator<AppUser> passwordValidator;
        private IUserValidator<AppUser> userValidator;
        private IEmailSender _emailSender;
        /*public AdminController(UserManager<AppUser> usrMgr, IPasswordHasher<AppUser> passwordHash)
        {
            userManager = usrMgr;
            passwordHasher = passwordHash;
        }*/

        public RegisterController(UserManager<AppUser> usrMgr, IEmailSender emailSender, IPasswordHasher<AppUser> passwordHash, AppIdentityDbContext context, RoleManager<IdentityRole> roleMgr, IPasswordValidator<AppUser> passwordVal, IUserValidator<AppUser> userValid)
        {
            _context = context;
            roleManager = roleMgr;
            userManager = usrMgr;
            passwordHasher = passwordHash;
            passwordValidator = passwordVal;
            userValidator = userValid;
            _emailSender = emailSender;
        }

        
        public ViewResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(User user, string id)
        {
            if (ModelState.IsValid)
            {
                
                AppUser appUser = new AppUser
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    UserName = user.UserName,
                    
                    Email = user.Email,
                    
                };
                
                IdentityResult result = await userManager.CreateAsync(appUser, user.Password);
                if (result.Succeeded)
                {
                    var rolestore = new RoleStore<IdentityRole>(_context);
                   // var roles = roleManager.Roles.ToList();
                    List<string> roles = roleManager.Roles.Select(x => x.Name).ToList();
                   
                    string rolename = roles[0];
                    
                    await userManager.AddToRoleAsync(appUser, rolename);


                    var token = await userManager.GenerateEmailConfirmationTokenAsync(appUser);
                    var confirmationLink = Url.Action("ConfirmEmail", "Email", new { token, email = user.Email }, Request.Scheme);
                    EmailConfiguration emailHelper = new EmailConfiguration();
                    //bool emailResponse = emailHelper.SendEmail(user.Email, confirmationLink);
                    var msg = new Message(new string[] { user.Email }, "Confirm Email", confirmationLink);
                    _emailSender.SendEmail(msg);
                    if (ModelState.IsValid)
                        return RedirectToAction("Index");
                    else
                    {
                        // log email failed 
                    }
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
                

            }
            return View(user);
        }
        public async Task<List<string>> AddRole(RoleModification model)
        {
            List<string> rollees = new List<string>();
            IdentityResult result;
            if (ModelState.IsValid)
            {
                IdentityRole rolename = await roleManager.FindByIdAsync(model.RoleId);
                foreach (string userId in model.AddIds ?? new string[] { })
                {
                    AppUser user = await userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await userManager.AddToRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                            Errors(result);
                    }
                }
               
            }
            return rollees;
        }

        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
    }
}