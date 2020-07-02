using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using MBshop.Data.Data;
using MBshop.Service.interfaces;
using MBshop.Service.StaticProperyes;
using MBshop.Service.WebConstants;
using MBshop.Service.Services;
using AutoMapper;
using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid;
using SendGrid.Helpers.Mail;
using MBshop.MailKit.MessageSenders;

namespace MBshop
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<MovieShopDBSEContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            // Get connection string 
            //ConnectionString.ConString = Configuration.GetConnectionString("DefaultConnection");

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 0;
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAutoMapper(typeof(Startup));

            services.AddControllersWithViews();

            services.AddRazorPages();

            services.AddRouting();

            services.AddScoped<IProfileEditService, ProfileEditService>();
            services.AddScoped<MovieShopDBSEContext>();
            services.AddScoped<IViewMoviesService, ViewMoviesService>();
            services.AddScoped<IViewBooksService, ViewBooksService>();
            services.AddScoped<IShopItemsService, ShopItemsService>();
            services.AddScoped<IAdminPanel, AdminPanel>();
            services.AddSingleton<Status>();
            services.AddScoped<IUserShopedProductsService,
                UserShopedProductsService>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<RatingSistemService>();

            services.AddTransient<ICartService, CartService>();

            services.AddScoped<IRatingSistemService, RatingSistemService>();
            services.AddScoped<ISearchEngineService, SearchEngineService>();
            services.AddScoped<GlobalAlertMessages>();
            services.AddScoped<ILogService, LogService>();
            services.AddScoped<IRoleService, RoleService>();

            services.AddTransient<IEmailSender>(x => new EmailSender(""));

            services.AddCors(option =>
            {
                option.AddPolicy("MessagesCORSPolicy",
                    builder =>
                    {
                        builder.WithOrigins("https://localhost:44342")
                        .AllowAnyHeader();

                        builder.WithOrigins("https://localhost:5001").AllowAnyHeader();
                    });
            });
            services.AddApplicationInsightsTelemetry();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                WebConstantsVariables.EnvironmentVariables = Configuration.GetConnectionString("DevelopmentConnection");

                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {

                WebConstantsVariables.EnvironmentVariables = Configuration.GetConnectionString("ProductionConnection");

                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }



            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
           name: "MyArea",
           pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
           name: "Admin",
           areaName: "Admin",
           pattern: "Admin/{controller=RoleAssignment}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
           name: "RoleEdit",
           areaName: "Edit",
           pattern: "Edit/{controller=RoleAssignment}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "Order",
                    pattern: "{controller=Home}/{action=Index}/{string}");

                

                endpoints.MapRazorPages();


            });

        }
    }
}