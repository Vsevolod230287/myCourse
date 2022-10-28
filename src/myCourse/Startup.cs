using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace myCourse
{
   public class Startup
   {
      public void ConfigureServices(IServiceCollection services)
      {
         services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
         app.UseStaticFiles();

         app.UseMvc(routeBuilder =>
                 {
                    routeBuilder.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
                 });

      }
   }
}
