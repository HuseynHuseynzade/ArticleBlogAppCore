using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArticleBlogAppCore.Areas.Admin.Models;
using ArticleBlogAppCore.Data;
using ArticleBlogAppCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArticleBlogAppCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SubCategoriesController : Controller
    {
        private ArticleBlogDbContext _articleBlogDbContext;
        public SubCategoriesController(ArticleBlogDbContext articleBlogDbContext)
        {
            _articleBlogDbContext = articleBlogDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
           List<SubCategory> SubCategories = await _articleBlogDbContext.SubCategories.Include(x=>x.Category).ToListAsync();

            return View(SubCategories);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
           var categories  = await _articleBlogDbContext.Categories.ToListAsync();

            SubcategoryCreateModel subcategoryCreateModel = new SubcategoryCreateModel
             {
                Categories = categories
             };
            return View(subcategoryCreateModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubCategory category)
        {
            if(ModelState.IsValid)
            {
               SubCategory existsSubCategory = await _articleBlogDbContext.SubCategories.Where(x => x.Name.ToLower() == category.Name.ToLower()).FirstOrDefaultAsync();
                if(existsSubCategory != null)
                {
                    ModelState.AddModelError("", "This subcategory already exists");
                    return View();
                }
                else
                {
                   await _articleBlogDbContext.SubCategories.AddAsync(existsSubCategory);
                    await _articleBlogDbContext.SaveChangesAsync();
                }
            }
            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            SubCategory currentCategory = await _articleBlogDbContext.SubCategories.FindAsync(id);
            if (currentCategory != null)
            {
                return View(currentCategory);
            }
            else
            {
                ModelState.AddModelError("", "This subcategory not exists");
                return RedirectToAction(nameof(List));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SubCategory category)
        {
            if(ModelState.IsValid)
            {
               SubCategory dbCategory = await _articleBlogDbContext.SubCategories.FindAsync(category.Id);
                if(dbCategory!=null)
                {
                    dbCategory.Name = category.Name;
                   await _articleBlogDbContext.SaveChangesAsync();
                    ModelState.AddModelError("", "Successfully saved");
                    return View();
                }
                else
                {
                    ModelState.AddModelError("", "This kind of subcategory is not exists");
                    return View();
                }
            }
            return RedirectToAction(nameof(List));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            SubCategory currentCategory = await _articleBlogDbContext.SubCategories.FindAsync(id);
            if (currentCategory != null)
            {
                _articleBlogDbContext.SubCategories.Remove(currentCategory);
                await _articleBlogDbContext.SaveChangesAsync();
            }
            return RedirectToAction(nameof(List));
        }
    }
}