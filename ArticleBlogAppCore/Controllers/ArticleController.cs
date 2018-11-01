using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArticleBlogAppCore.Data;
using ArticleBlogAppCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace ArticleBlogAppCore.Controllers
{
    public class ArticleController : Controller
    {
        private readonly ArticleBlogDbContext _articleBlogDbContext;
        public ArticleController(ArticleBlogDbContext articleBlogDbContext)
        {
            _articleBlogDbContext = articleBlogDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            Article currentArticle = await _articleBlogDbContext.Articles.FindAsync(id);

            if (currentArticle == null)
            {
                return NotFound();
            }
            return View(currentArticle);
        }
    }
}