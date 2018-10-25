using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArticleBlogAppCore.Models
{
    public class AppUser:IdentityUser
    {
        public AppUser()
        {
            Articles = new HashSet<Article>();
        }
        public virtual ICollection<Article> Articles { get; set; }
    }
}
