using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using myCourse.Models.Options;
using myCourse.Models.Services.Application;
using myCourse.Models.Services.Infrastructure;

namespace myCourse
{
    public class Startup
    {

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;

        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            // services.AddTransient<ICourseService, AdoNetCourseService>();
            services.AddTransient<ICourseService, EfCoreCourseService>();
            services.AddTransient<IDatabaseAccessor, SQLiteDatabaseAccessor>();
            services.AddTransient<ICashedCourseService, MemoryCacheCourseService>();
            // services.AddScoped<MyCourceDbContext>();
            // services.AddDbContext<MyCourceDbContext>();
            services.AddDbContextPool<MyCourceDbContext>(optionsBuilder =>
            {
                string connectionString = Configuration.GetSection("ConnectionStrings").GetValue<string>("Default");
                optionsBuilder.UseSqlite(connectionString);
            });



            //Options
            services.Configure<ConnectionStringsOptions>(Configuration.GetSection("ConnectionStrings"));
            services.Configure<CoursesOptions>(Configuration.GetSection("Courses"));
            services.Configure<MemoryCacheOptions>(Configuration.GetSection("MemoryCache"));


        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();


                lifetime.ApplicationStarted.Register(() =>
                {
                    string filePath = Path.Combine(env.ContentRootPath, "bin/reload.txt");
                    File.WriteAllText(filePath, DateTime.Now.ToString());
                });

            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();

            app.UseMvc(routeBuilder =>
                    {
                        routeBuilder.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
                    });

        }
    }
}
