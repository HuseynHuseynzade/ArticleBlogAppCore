using ArticleBlogAppCore.Data;
using ArticleBlogAppCore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArticleBlogAppCore.ViewComponents
{
    public class SliderViewComponent:ViewComponent
    {
        private readonly ArticleBlogDbContext _articleBlogDbContext;

        public SliderViewComponent(ArticleBlogDbContext articleBlogDbContext)
        {
            _articleBlogDbContext = articleBlogDbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var articles = await _articleBlogDbContext.Articles.Take(4).Select(x => new ArticleIndexModel()
            {
                CreateDate = x.AddedDate,
                Description = x.Description,
                Id = x.Id,
                Name = x.Name,
                PhotoPath = x.PhotoPath,
                Tags = x.Tags

            }).ToListAsync();

            return View(articles);
        }
    }
}
