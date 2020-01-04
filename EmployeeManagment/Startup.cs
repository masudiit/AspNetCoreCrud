using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EmployeeManagment
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDbContext>(
                options => options.UseSqlServer(_config.GetConnectionString("EmployeeDBConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options => 
            {
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 3;
            })
                .AddEntityFrameworkStores<AppDbContext>();
            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddAuthorization(options =>
            {
                //options.AddPolicy("DeleteRolePloicy", 
                //    policy => policy.RequireClaim("Delete Role")
                //   .RequireClaim("Create Role"));

                options.AddPolicy("DeleteRolePloicy",
                    policy => policy.RequireClaim("Delete Role"));

                options.AddPolicy("AdminRolePloicy",
                   policy => policy.RequireRole("Admin"));

            });


            //services.AddSingleton<IEmployeeRepository, MockEmployeeRepository>();
            services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
              ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // app.UseStatusCodePages();
                //app.UseStatusCodePagesWithRedirects("/Error/{0}");

                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");

            }
            //app.Use(async (context, next) =>
            //{
            //    logger.LogInformation("MW1: Incoming Request");
            //    await next();
            //    logger.LogInformation("MW1: Outgoing Response");
            //});

            //app.Use(async (context, next) =>
            //{
            //    logger.LogInformation("MW2: Incoming Request");
            //    await next();
            //    logger.LogInformation("MW2: Outgoing Response");
            //});

            // app.UseDefaultFiles();

            app.UseAuthentication();
            app.UseStaticFiles();
            // app.UseMvcWithDefaultRoute();
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
           

            //app.Run(async (context) =>
            //{
            //    //  await context.Response.WriteAsync(_config["MyKey"]);
            //    await context.Response.WriteAsync("hello world");
            //    logger.LogInformation("MW1: Outgoing Response");
            //});
        }
    }
}
