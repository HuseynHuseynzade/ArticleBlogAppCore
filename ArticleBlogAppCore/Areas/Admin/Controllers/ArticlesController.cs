using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleBlogAppCore.Data;
using ArticleBlogAppCore.Extensions;
using ArticleBlogAppCore.Models;
using ArticleBlogAppCore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ArticleBlogAppCore.Areas.Admin.Models;

namespace ArticleBlogAppCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class ArticlesController : Controller
    {
        private ArticleBlogDbContext _articleBlogDbContext;
        private IHttpContextAccessor _session;
        private IHostingEnvironment _hostingEnvironment;
        public ArticlesController(ArticleBlogDbContext articleBlogDbContext
                                 , IHttpContextAccessor session
                                 ,IHostingEnvironment hostingEnvironment)
        {
            this._articleBlogDbContext = articleBlogDbContext;
            this._session = session;
            this._hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public async  Task<IActionResult> Edit(int id)
        {
            Article article = await  _articleBlogDbContext.Articles.FindAsync(id);
            if(article==null)
            {
                ModelState.AddModelError("", "Article is not found!!");
                return View();
            }

            return View(article);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ArticleEditModel articleEditModel,int id)
        {
            if (articleEditModel.Id != id)
                return RedirectToAction("List", "Articles", new { area = "Admin" });

            if(ModelState.IsValid)
            {
                Article currentArticle = await _articleBlogDbContext.Articles.FindAsync(articleEditModel.Id);

                if(currentArticle!=null)
                {
                    #region update current article
                    currentArticle.MetaDescription = articleEditModel.MetaDescription;
                    currentArticle.MetaKeywords = articleEditModel.MetaKeywords;
                    currentArticle.Name = articleEditModel.Name;
                    currentArticle.Tags = articleEditModel.Tags;
                    currentArticle.Description = articleEditModel.Description;
                    currentArticle.Detail = articleEditModel.Detail;
                    #endregion

                   await _articleBlogDbContext.SaveChangesAsync();
                    return RedirectToAction("Articles", "Home", new { area = "Admin" });
                }
            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
           Article currentArticle = await _articleBlogDbContext.Articles.FindAsync(id);
            if(currentArticle!=null)
                {
                   _articleBlogDbContext.Articles.Remove(currentArticle);
                    await  _articleBlogDbContext.SaveChangesAsync();
                 }
            return RedirectToAction("Articles", "Home", new { area = "Admin" });
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ArticleModel articleModel)
        {

            string p = articleModel?.FormFile?.FileName;


            if (p != null)
            {
                articleModel.PhotoPath = p.Substring(p.LastIndexOf(@"\") + 1);
            }
            else
            {
                ModelState.AddModelError("", "Photo not defined");
                return View();
            }
                articleModel.Tags = articleModel.MetaKeywords;
          
            if(ModelState.IsValid)
            {
                Article article = articleModel;
                article.AuthorId = _session.HttpContext.Session.TryGetSessionValue("userId");
                 if(article.AuthorId==null)
                {
                    ModelState.AddModelError("", "User not defined!!");
                    return View();
                }
                 else
                {
                   await _articleBlogDbContext.Articles.AddAsync(article);
                   await _articleBlogDbContext.SaveChangesAsync();

                               
                   await articleModel.FormFile.UploadAsync(_hostingEnvironment);

                    return RedirectToAction("Articles", "Home", new { area = "Admin" });
                }



            }
            return View();
        }
    }
}