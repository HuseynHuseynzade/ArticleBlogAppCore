using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ArticleBlogAppCore.Data;
using ArticleBlogAppCore.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace ArticleBlogAppCore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ArticleBlogDbContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:ArticleDbConnection"]);
            });


           
            services.AddIdentity<AppUser, IdentityRole>()
                     .AddEntityFrameworkStores<ArticleBlogDbContext>()
                       .AddDefaultTokenProviders();


            //asililigin yeridilmesi prosesi

          
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddDistributedMemoryCache();
            services.AddSession();

            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
               
            }
            else
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
                app.UseHsts();
               
               // app.UseExceptionHandler("/Home/error");
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "Uploads")),
                RequestPath = new PathString("/Uploads"),
                
            });

         
            app.UseSession();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                   name: "areas",
                   template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                 );

                routes.MapRoute(null, "{controller}/{action}/{id?}", new { controller = "Home", action = "Index" });
            });
        }
    }
}
