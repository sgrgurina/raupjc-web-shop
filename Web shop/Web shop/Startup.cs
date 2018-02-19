using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Google.Apis;
using Google;
using Microsoft.AspNetCore.Http;
using Webshop.Shop;
using Web_shop.Data;
using Web_shop.Models;
using Web_shop.Services;

namespace Web_shop
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IShopRepository, ShopRepository>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(sp => ShoppingCart.GetCart(sp));
            services.AddScoped((s) =>
            {
                return new ShopDbContext(Configuration.GetConnectionString("DefaultConnection"));
            });


            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication().AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = Configuration.GetSection("Google")["ClientId"];
                googleOptions.ClientSecret = Configuration.GetSection("Google")["ClientSecret"];
            });


            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            CreateRoles(serviceProvider).Wait();
            
        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var roleExist = await roleManager.RoleExistsAsync("Administrator");
            if (!roleExist)
            {
                var role = new IdentityRole("Administrator");
                var res = await roleManager.CreateAsync(role);
            }

            var adminUser = new ApplicationUser
            {
                UserName = Configuration.GetSection("Logging:UserSettings")["UserEmail"],
                Email = Configuration.GetSection("Logging:UserSettings")["UserEmail"]
            };

            string userPassword = Configuration.GetSection("Logging:UserSettings")["UserPassword"];
            var _user = await userManager.FindByEmailAsync(Configuration.GetSection("Logging:UserSettings")["UserEmail"]);

            if (_user == null)
            {
                var createPowerUser = await userManager.CreateAsync(adminUser, userPassword);

                if (createPowerUser.Succeeded)

                {
                    await userManager.AddToRoleAsync(adminUser, "Administrator");
                }
            }
        }
    }
}