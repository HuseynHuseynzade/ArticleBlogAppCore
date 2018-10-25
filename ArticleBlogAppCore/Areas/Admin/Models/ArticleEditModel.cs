using ArticleBlogAppCore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ArticleBlogAppCore.Areas.Admin.Models
{
    public partial class ArticleEditModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Tags { get; set; }
        [Required]
        public string MetaKeywords { get; set; }
        [Required]
        public string MetaDescription { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Detail { get; set; }
    }

    public partial class ArticleEditModel
    {
        public static implicit operator Article(ArticleEditModel articleEditModel)
        {
            return new Article()
            {
                Description = articleEditModel.Description,
                MetaDescription = articleEditModel.MetaDescription,
                MetaKeywords = articleEditModel.MetaKeywords,
                Name = articleEditModel.Name,
                Tags = articleEditModel.Tags,
                Detail = articleEditModel.Detail
            };
        }
    }
}
