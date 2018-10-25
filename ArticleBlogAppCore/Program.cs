using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ArticleBlogAppCore.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ArticleBlogAppCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
           IWebHost webHost = CreateWebHostBuilder(args).Build();

            using (IServiceScope serviceScope = webHost.Services.CreateScope())
            {
               UserManager<AppUser> userManager =  serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
               RoleManager<IdentityRole> roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                IdentityRole admin = new IdentityRole
                {
                    Name = "Admin"
                };

                if (!roleManager.Roles.Any())
                {
                   

                    IdentityRole user = new IdentityRole
                    {
                        Name = "User"
                    };

                    roleManager.CreateAsync(admin).GetAwaiter().GetResult();
                    roleManager.CreateAsync(user).GetAwaiter().GetResult();
                }

                if (!userManager.Users.Any())
                {
                    AppUser user = new AppUser
                    {
                        Email = "huseyn@gmail.com",
                        UserName = "Huseyn"
                    };

                    userManager.CreateAsync(user, "Huseyn123@").GetAwaiter().GetResult();

                    userManager.AddToRoleAsync(user, admin.Name).GetAwaiter().GetResult();
                }
            }
                webHost.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
