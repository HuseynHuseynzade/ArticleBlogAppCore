using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ArticleBlogAppCore.Areas.Admin.Models;
using ArticleBlogAppCore.Data;
using ArticleBlogAppCore.Extensions;
using ArticleBlogAppCore.Models;
using ArticleBlogAppCore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IPasswordValidator<AppUser> _passwordValidator;
        private readonly IPasswordHasher<AppUser> _passwordHasher;
        public UsersController(UserManager<AppUser> userManager
            ,ArticleBlogDbContext articleBlogDbContext
            , RoleManager<IdentityRole> roleManager
            ,IPasswordValidator<AppUser> passwordValidator
            ,IPasswordHasher<AppUser> passwordHasher)
        {
            this._articleBlogDbContext = articleBlogDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _passwordHasher = passwordHasher;
            _passwordValidator = passwordValidator;
        }
        public async Task<IActionResult> List()
        {



            var users = await _userManager.Users.Select((x) => new UserIndexModel()
            {
                Email = x.Email,
                Id = x.Id,
                PhoneNumber = x.PhoneNumber,
                UserName = x.UserName
            }).ToListAsync();

            var userViewModels = users.Select(async user =>
            {
                user.RoleName = await GetUserRoleAsync(user.Id);
              });

          await Task.WhenAll(userViewModels);
            
            return View(users);
        }

        private async Task<string> GetUserRoleAsync(string id)
        {
            AppUser appUser = await _userManager.FindByIdAsync(id);

            List<string> appRoles = (await _userManager.GetRolesAsync(appUser)).ToList();

           return await Task.FromResult<string>(appRoles[0]);
          
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            UserCreateModel userCreateModel = new UserCreateModel
            {
                Roles = _roleManager.Roles.Select(x => new UserRole
                {
                    Id = x.Id,
                    RoleName = x.Name
                })
            };
           
            return View(userCreateModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser([Bind("Email","Password","ConfirmPassword","Name","RoleId")]UserCreateModel createModel)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser
                {
                    Email = createModel.Email,
                    UserName = createModel.Name
                };

                IdentityResult identityResult = await _userManager.CreateAsync(appUser, createModel.Password);
                if (identityResult.Succeeded)
                {
                    IdentityRole identityRole = await _roleManager.Roles.Where(x => x.Id == createModel.RoleId).FirstOrDefaultAsync();

                    if (identityRole != null)
                    {
                        await _userManager.AddToRoleAsync(appUser, identityRole.Name);
                        return RedirectToAction("List", "Users");
                    }
                    else
                        return View();
                }
               
                else
                    return View();
            }
            else
                return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            AppUser appUser = await _userManager.FindByIdAsync(id);
          var roles =  await _userManager.GetRolesAsync(appUser);
            if(appUser != null)
            {
                UserCreateModel userCreateModel = new UserCreateModel
                {
                     Id = appUser.Id,
                     Email = appUser.Email,
                      Name  = appUser.UserName,
                    Roles = _roleManager.Roles.Select(x => new UserRole
                    {
                        Id = x.Id,
                        RoleName = x.Name
                    }),
                     RoleName = roles[0]
                };

                return View(userCreateModel);
            }
            return RedirectToAction(nameof(List));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserCreateModel userCreateModel)
        {
            if(ModelState.IsValid)
            {
               AppUser currentUser = await _userManager.FindByEmailAsync(userCreateModel.Email);

                var roles = await _userManager.GetRolesAsync(currentUser);
                //TODO:Password validdirmi
                //TODO:validdirse paswordu update et
                IdentityResult identityResult = 
                      await _passwordValidator.ValidateAsync(_userManager, currentUser, userCreateModel.Password);

                //1.Adam parolu deyismir,sadece muelumatlari update edir
                //2.Adam parolu da melumatlari da deyiisir.
                if (identityResult.Succeeded)
                {
                    currentUser.UserName = userCreateModel.Name;

                  IdentityRole receivedRole = await _roleManager.FindByIdAsync(userCreateModel.RoleId);

                    if(receivedRole.Name.ToLower()!=roles[0].ToLower())
                    {
                        await _userManager.RemoveFromRoleAsync(currentUser, roles[0]);
                        await _userManager.AddToRoleAsync(currentUser, receivedRole.Name);
                    }
                  
                   await _userManager.UpdateAsync(currentUser);
                 
                    if(!String.IsNullOrEmpty(userCreateModel.NewPassword))
                    {
                       await _userManager.ChangePasswordAsync(currentUser, userCreateModel.Password, userCreateModel.NewPassword);
                        //currentUser.PasswordHash = _passwordHasher.HashPassword(currentUser, userCreateModel.Password);
                    }
                }
                else
                    this.AddErrorsToModelState(identityResult.Errors);
            }
            return RedirectToAction(nameof(List));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([Required]string id)
        {
           AppUser currentUser = await _userManager.FindByIdAsync(id);
            if(currentUser!=null)
            {
              IdentityResult identityResult = await _userManager.DeleteAsync(currentUser);
                if(!identityResult.Succeeded)
                {
                    this.AddErrorsToModelState(identityResult.Errors);
                }
            }
            else
            {
                ModelState.AddModelError("", "This is is not exists");
            }
            return RedirectToAction(nameof(List));
        }
    }
}