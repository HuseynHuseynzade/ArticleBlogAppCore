using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArticleBlogAppCore.Models;
using ArticleBlogAppCore.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Identity = Microsoft.AspNetCore.Identity;
namespace ArticleBlogAppCore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private IHttpContextAccessor _session;
        public AccountController
             (UserManager<AppUser> userManager
              ,SignInManager<AppUser> signInManager
              , IHttpContextAccessor session)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._session = session;
        }
        [HttpGet]
        public IActionResult Login()
        {
          
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = await _userManager.FindByEmailAsync(loginModel.Email);

                if(appUser==null)
                {
                    ModelState.AddModelError("", "This user is not found!!");
                    return View();
                }
               
             Identity.SignInResult signInResult =  await _signInManager.PasswordSignInAsync(appUser, loginModel.Password, loginModel.IsRemembered, true);

                if(signInResult.Succeeded)
                {
                   
                    _session.HttpContext.Session.SetString("userId", appUser.Id);


                    if (await _userManager.IsInRoleAsync(appUser, "Admin"))
                    {
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    }
                    else
                        return RedirectToAction("Index", "Home", new { area = "" });

                }
                else if(signInResult.IsLockedOut)
                {
                    ModelState.AddModelError("", "This user already locked out");
                }
                else
                    if(signInResult.IsNotAllowed)
                {
                    ModelState.AddModelError("", "User is not allowed");
                }
                return View();

            }
            else
                return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = registerModel;
                IdentityResult identityResult = await _userManager.CreateAsync(appUser, registerModel.Password);
               await _userManager.AddToRoleAsync(appUser, "User");
                if (identityResult.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }
                else
                    return View();
            }
            else
                return View();
        }

        private void AddErrorToModelState(IEnumerable<IdentityError> errors)
        {
            foreach (IdentityError item in errors)
            {
                ModelState.AddModelError("", item.Description);
            }
        }

        
    }
}