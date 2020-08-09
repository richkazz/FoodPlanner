using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Identity.Models;
using System.Threading.Tasks;
using System.Security.Claims;

using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

using FoodPlanner.Email;
using FoodPlanner.Interfaces;
using System.Linq;
using NToastNotify;
using FoodPlanner.Util;

namespace Identity.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        private readonly AppIdentityDbContext _context;
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;
        private IEmailSender _emailSender;
        private IUserLoginStatus __userLoginStatus;
        private RoleManager<IdentityRole> roleManager;
        private readonly IToastNotification _toastNotification;

        public AccountController(AppIdentityDbContext context, IToastNotification toastNotification, IEmailSender emailSender, IUserLoginStatus userLoginStatus, UserManager<AppUser> userMgr, SignInManager<AppUser> signinMgr, RoleManager<IdentityRole> roleMgr)
        {
            _toastNotification = toastNotification;
            _context = context;
            userManager = userMgr;
            signInManager = signinMgr;
            roleManager = roleMgr;
            _emailSender = emailSender;
            __userLoginStatus = userLoginStatus;
        }

        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            Login login = new Login();
            login.ReturnUrl = returnUrl;
            return View(login);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(RoleModification model,Login login,User statuslog )
        {

            if (login.Email != null)
            {
                AppUser appUser = await userManager.FindByEmailAsync(login.Email);
                if (appUser != null)
                {
                    await signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result1 = await signInManager.PasswordSignInAsync(appUser, login.Password, false, true);
                    if (result1.Succeeded)
                    {
                        List<string> role = new List<string>();
                        role.Add(model.RoleName);
                        foreach (string userId in model.DeleteIds ?? new string[] { })
                        {
                            AppUser user = await userManager.FindByIdAsync(userId);
                            if (user != null)
                            {
                              var result2 = await userManager.FindByNameAsync(model.RoleName);
                                
                            }
                        }


                        var updateLoginStatus = await __userLoginStatus.UpdatStaus(appUser.Id, true);

                        TempData["UserId"] = appUser.Id;


                        var getrole = userManager.GetRolesAsync(appUser);
                        if(getrole.Result[0] == "Admin")
                        {
                            return RedirectToAction("Index1", "Home");
                        }
                        if(getrole.Result[0] == "User")
                        {
                            return RedirectToAction("Index", "UserHome");
                        }
                        if(getrole.Result[0] == "Manager")
                        {
                            return Redirect(login.ReturnUrl ?? "/UserIndex");
                        }
                        //if (User.IsInRole(appUser.Id,"Admin")) { return Redirect(login.ReturnUrl ?? "/"); }
                        //if (User.IsInRole("Admin")) { return Redirect(login.ReturnUrl ?? "/"); }
                        return Redirect(login.ReturnUrl ?? "/");
                    }
                    /*bool emailStatus = await userManager.IsEmailConfirmedAsync(appUser);
                    if (emailStatus == false)
                    {
                        ModelState.AddModelError(nameof(login.Email), "Email is unconfirmed, please confirm it first");
                    }*/

                    if (result1.IsLockedOut)
                        _toastNotification.AddErrorToastMessage(ResponseMessageUtilities.ACCOUNT_LOCKED_OUT);
                    if (!result1.Succeeded){
                        _toastNotification.AddErrorToastMessage(ResponseMessageUtilities.INVALID_LOGIN);
                        return View();
                    }
                    if (result1.RequiresTwoFactor)
                    {
                        return RedirectToAction("LoginTwoStep", new { appUser.Email, login.ReturnUrl });
                    }
                }
                _toastNotification.AddErrorToastMessage(ResponseMessageUtilities.INVALID_LOGIN);
                return View();
                //ModelState.AddModelError(nameof(login.Email), "Login Failed: Invalid Email or password");
            }
            return View(login);
        }

        [AllowAnonymous]
        public async Task<IActionResult> LoginTwoStep(string email, string returnUrl)
        {
            var user = await userManager.FindByEmailAsync(email);

            var token = await userManager.GenerateTwoFactorTokenAsync(user, "Email");

            EmailConfiguration emailHelper = new EmailConfiguration();
            //bool emailResponse = emailHelper.SendEmailTwoFactorCode(user.Email, token);

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoginTwoStep(TwoFactor twoFactor, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(twoFactor.TwoFactorCode);
            }

            var result = await signInManager.TwoFactorSignInAsync("Email", twoFactor.TwoFactorCode, false, false);
            if (result.Succeeded)
            {
                return Redirect(returnUrl ?? "/");
            }
            else
            {
                _toastNotification.AddErrorToastMessage(ResponseMessageUtilities.Invalid_Login_Attempt);
             
                return View();
            }
        }


        public async Task<IActionResult> Logout()
        {
            var userId = TempData["UserId"].ToString();
            var updateLoginStatus = await __userLoginStatus.UpdatStaus(userId, false);

            await signInManager.SignOutAsync();
                        
            return RedirectToAction("Login", "Account");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult GoogleLogin()
        {
            string redirectUrl = Url.Action("GoogleResponse", "Account");
            var properties = signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return new ChallengeResult("Google", properties);
        }

        [AllowAnonymous]
        public async Task<IActionResult> GoogleResponse()
        {
            ExternalLoginInfo info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
                return RedirectToAction(nameof(Login));

            var result = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
            string[] userInfo = { info.Principal.FindFirst(ClaimTypes.Name).Value, info.Principal.FindFirst(ClaimTypes.Email).Value };
            if (result.Succeeded)
                return View(userInfo);
            else
            {
                AppUser user = new AppUser
                {
                    Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
                    UserName = info.Principal.FindFirst(ClaimTypes.Email).Value
                };

                IdentityResult identResult = await userManager.CreateAsync(user);
                if (identResult.Succeeded)
                {
                    identResult = await userManager.AddLoginAsync(user, info);
                    if (identResult.Succeeded)
                    {
                        await signInManager.SignInAsync(user, false);
                        return View(userInfo);
                    }
                }
                return AccessDenied();
            }
        }

        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([Required]string email)
        {
            if (!ModelState.IsValid)
                return View(email);

            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                _toastNotification.AddErrorToastMessage(ResponseMessageUtilities.INVALID_EMAIL);
                return View();
            }
               

            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var link = Url.Action("ResetPassword", "Account", new { token, email = user.Email }, Request.Scheme);

            EmailConfiguration emailHelper = new EmailConfiguration();
         
            //bool emailResponse = emailHelper.SendEmailPasswordReset(user.Email, link);
            var msg = new Message(new string[] { user.Email }, "Password Reset", link);
            _emailSender.SendEmail(msg);
            //bool emailResponse = emailHelper.SendEmail(msg);

            if (ModelState.IsValid) { 
                _toastNotification.AddSuccessToastMessage(ResponseMessageUtilities.FORGOT_PASSWORD_CONFIRMTION);
                return View();
                //return RedirectToAction("ForgotPasswordConfirmation");
            }

            else
            {
                // log email failed 
            }
            return View(email);
        }

        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult ResetPassword(string token, string email)
        {
            var model = new ResetPassword { Token = token, Email = email };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPassword resetPassword)
        {
            if (!ModelState.IsValid)
                return View(resetPassword);

            var user = await userManager.FindByEmailAsync(resetPassword.Email);
            if (user == null)
                RedirectToAction(nameof(ResetPasswordConfirmation));

            var resetPassResult = await userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);
            if (!resetPassResult.Succeeded)
            {
                foreach (var error in resetPassResult.Errors)
                    ModelState.AddModelError(error.Code, error.Description);
                return View();
            }

            return RedirectToAction(nameof(ResetPasswordConfirmation));
        }

        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
    }
}