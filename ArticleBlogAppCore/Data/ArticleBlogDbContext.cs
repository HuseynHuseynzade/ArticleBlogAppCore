using ArticleBlogAppCore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArticleBlogAppCore.Data
{
    public class ArticleBlogDbContext:IdentityDbContext<AppUser>
    {
        public ArticleBlogDbContext(DbContextOptions<ArticleBlogDbContext> dbContextOptions) : base(dbContextOptions) { }
       
        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<SocialAccount> SocialAccounts { get; set; }

       
    }
}
