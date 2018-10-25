using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArticleBlogAppCore.Areas.Admin.Models;
using ArticleBlogAppCore.Data;
using ArticleBlogAppCore.Models;
using ArticleBlogAppCore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArticleBlogAppCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class UsersController : Controller
    {
        private ArticleBlogDbContext _articleBlogDbContext;
        private readonly UserManager<AppUser> _userManager;
        public UsersController(UserManager<AppUser> userManager,ArticleBlogDbContext articleBlogDbContext)
        {
            this._articleBlogDbContext = articleBlogDbContext;
            _userManager = userManager;
        }
        public async Task<IActionResult> List()
        {
            var users = await _userManager.Users.Select(x => new UserIndexModel()
            {
                Email = x.Email,
                Id = x.Id,
                PhoneNumber = x.PhoneNumber,
                UserName = x.UserName
            }).ToListAsync();

            return View(users);
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = registerModel;
                IdentityResult identityResult = await _userManager.CreateAsync(appUser, registerModel.Password);
                await _userManager.AddToRoleAsync(appUser, "User");
                if (identityResult.Succeeded)
                {
                    return RedirectToAction("List", "Users");
                }
                else
                    return View();
            }
            else
                return View();
        }

    }
}