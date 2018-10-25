using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArticleBlogAppCore.Data;
using ArticleBlogAppCore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ArticleBlogAppCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ArticleBlogDbContext _articleBlogDbContext;


        public HomeController(ArticleBlogDbContext articleBlogDbContext)
        {
            this._articleBlogDbContext = articleBlogDbContext;
        }
        public IActionResult Index()
        {
            
          var articles =  _articleBlogDbContext.Articles.Take(6).Select(x=>new ArticleIndexModel()
            {
                CreateDate = x.AddedDate,
                 Description = x.Description,
                  Id = x.Id,
                   Name = x.Name,
                    PhotoPath = x.PhotoPath,
                     Tags =x.Tags

            });
            return View(articles);
        }
    }
}