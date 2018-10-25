using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArticleBlogAppCore.Areas.Admin.Models;
using ArticleBlogAppCore.Models;
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
        private readonly UserManager<AppUser> _userManager;
        public UsersController(UserManager<AppUser> userManager)
        {
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
    }
}