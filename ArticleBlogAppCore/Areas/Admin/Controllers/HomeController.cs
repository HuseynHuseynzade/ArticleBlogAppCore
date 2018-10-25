using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ArticleBlogAppCore.Data;
using ArticleBlogAppCore.Models;
using ArticleBlogAppCore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ArticleBlogAppCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin,User")]
    public class HomeController : Controller
    {
       private ArticleBlogDbContext _articleBlogDbContext;
        private IHostingEnvironment _hostingEnvironment;
        private readonly SignInManager<AppUser> _signInManager;
        public HomeController(ArticleBlogDbContext articleBlogDbContext
            ,IHostingEnvironment hostingEnvironment
            , SignInManager<AppUser> signInManager)
        {
            _articleBlogDbContext = articleBlogDbContext;
            _hostingEnvironment = hostingEnvironment;
            _signInManager = signInManager;

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
           await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        [HttpGet]
        public IActionResult Articles()
        {

          IEnumerable<ArticleIndexModel> articleIndexModels =  _articleBlogDbContext.Articles.Select(x => new ArticleIndexModel()
            {
                CreateDate = x.AddedDate,
                Description = x.Description,
                Name = x.Name,
                PhotoPath = x.PhotoPath,
                Tags = x.Tags,
                 Id = x.Id
            }).AsEnumerable();

            return View(articleIndexModels);
        }
    }
}