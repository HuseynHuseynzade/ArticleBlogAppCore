using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArticleBlogAppCore.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public AppUser AppUser { get; set; }
        public int AppUserId { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime EditDate { get; set; }

    }
}
