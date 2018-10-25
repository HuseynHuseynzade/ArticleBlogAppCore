using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ArticleBlogAppCore.Models.ViewModels
{
    public class ArticleModel
    {
        [Required]
        public string Name { get; set; }
       
        public string Tags { get; set; }

        [Required]
        public string MetaKeywords { get; set; }
        [Required]
        public string MetaDescription { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Detail { get; set; }
       
        public string PhotoPath { get; set; }
        [Required]
        public IFormFile FormFile { get; set; }

        public static implicit operator Article(ArticleModel articleModel)
        {
            return new Article()
            {
                AddedDate = DateTime.Now,
                 Description = articleModel.Description,
                  Detail = articleModel.Detail,
                   MetaDescription = articleModel.MetaDescription,
                    MetaKeywords = articleModel.MetaKeywords,
                     Name = articleModel.Name,
                      PhotoPath = articleModel.PhotoPath,
                       Tags = articleModel.Tags
                      
            };

        }
    }
}
