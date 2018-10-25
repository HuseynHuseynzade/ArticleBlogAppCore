using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArticleBlogAppCore.Data;
using ArticleBlogAppCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArticleBlogAppCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        private ArticleBlogDbContext _articleBlogDbContext;
        public CategoriesController(ArticleBlogDbContext articleBlogDbContext)
        {
            _articleBlogDbContext = articleBlogDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
           List<Category> categories = await _articleBlogDbContext.Categories.Include(x=>x.SubCategories).ToListAsync();

            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if(ModelState.IsValid)
            {
               Category existsCategory = await _articleBlogDbContext.Categories.Where(x => x.Name.ToLower() == category.Name.ToLower()).FirstOrDefaultAsync();
                if(existsCategory!=null)
                {
                    ModelState.AddModelError("", "This category already exists");
                    return View();
                }
                else
                {
                   await _articleBlogDbContext.Categories.AddAsync(category);
                    await _articleBlogDbContext.SaveChangesAsync();
                }
            }
            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Category currentCategory = await _articleBlogDbContext.Categories.FindAsync(id);
            if (currentCategory != null)
            {
                return View(currentCategory);
            }
            else
            {
                ModelState.AddModelError("", "This category not exists");
                return RedirectToAction(nameof(List));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category category)
        {
            if(ModelState.IsValid)
            {
               Category dbCategory = await _articleBlogDbContext.Categories.FindAsync(category.Id);
                if(dbCategory!=null)
                {
                    dbCategory.Name = category.Name;
                   await _articleBlogDbContext.SaveChangesAsync();
                    ModelState.AddModelError("", "Successfully saved");
                    return View();
                }
                else
                {
                    ModelState.AddModelError("", "This kind of category is not exists");
                    return View();
                }
            }
            return RedirectToAction(nameof(List));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Category currentCategory = await _articleBlogDbContext.Categories.FindAsync(id);
            if (currentCategory != null)
            {
                _articleBlogDbContext.Categories.Remove(currentCategory);
                await _articleBlogDbContext.SaveChangesAsync();
            }
            return RedirectToAction(nameof(List));
        }
    }
}