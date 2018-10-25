using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArticleBlogAppCore.Models
{
    public class Article
    {
      
        public int Id { get; set; }
        public string Name { get; set; }
        public string Tags { get; set; }

        public virtual AppUser Author { get; set; }
        public string AuthorId { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }

        public string Description { get; set; }
        public string Detail { get; set; }
        public string PhotoPath { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime EditDate { get; set; }
        public int ViewCount { get; set; }


    }
}
