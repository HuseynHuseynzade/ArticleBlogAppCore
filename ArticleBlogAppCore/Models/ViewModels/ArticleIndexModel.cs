using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArticleBlogAppCore.Models.ViewModels
{
    public class ArticleIndexModel
    {
       
        public int Id { get; set; }
        public string Name { get; set; }

        public string Tags { get; set; }
        
        public string Description { get; set; }

        public string PhotoPath { get; set; }
      
        public DateTime CreateDate { get; set; }
    }
}
