using AspNetCore.SEOHelper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShoppingAppUAE.Data;
using ShoppingAppUAE.Repository.UnitOfWork;
using ShoppingAppUAE.Services.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Stripe;
using ShoppingAppUAE.Models.CheckoutViewModels;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace ShoppingAppUAE
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
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddLocalization(opt => opt.ResourcesPath = "Resources");

            services.AddDefaultIdentity<ApplicationUser>(options => {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;

                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                
            }).AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();


            services.Configure<RequestLocalizationOptions>(options =>
            {
                List<CultureInfo> supportedCultures = new List<CultureInfo>
                {

                 new CultureInfo("en-GB"),
                new CultureInfo("ar-EG")
                 };
                options.DefaultRequestCulture = new RequestCulture("en-GB");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.RequestCultureProviders = new List<IRequestCultureProvider>
                 {
                  new QueryStringRequestCultureProvider(),
                         new CookieRequestCultureProvider()
                 };
            });


            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.AddTransient<IMailService, MailService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();


            services.AddAuthentication().AddGoogle(options =>
            {
                options.ClientId = Configuration["Google:ClientId"];
                options.ClientSecret = Configuration["Google:ClientSecret"];
            });



            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddControllersWithViews().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix); ;
            services.AddSwaggerGen();
            services.Configure<StripSettings>(Configuration.GetSection("Stripe"));

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            StripeConfiguration.ApiKey = Configuration["Stripe:SecretKey"];

            if (env.IsDevelopment())
            {
              
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseRequestLocalization(new RequestLocalizationOptions()
            {
                DefaultRequestCulture = new RequestCulture("en-GB"),
                SupportedCultures = new List<CultureInfo>
                {

                    new CultureInfo("en-GB"),
                     new CultureInfo("ar-EG")
                 },
                SupportedUICultures = new List<CultureInfo>
                {

                    new CultureInfo("en-GB"),
                     new CultureInfo("ar-EG")
                 },
                RequestCultureProviders = new List<IRequestCultureProvider>
                 {
                  new QueryStringRequestCultureProvider(),
                         new CookieRequestCultureProvider()
                 }



            });

            app.UseDeveloperExceptionPage();
            Seeder.Seeder.PrePopulation(app).Wait();
            app.UseForwardedHeaders(new ForwardedHeadersOptions() { ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto | ForwardedHeaders.All });


            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors(a => a.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseRobotsTxt(env.ContentRootPath);
            app.UseXMLSitemap(env.ContentRootPath);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
           name: "areas",
           pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
           );
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

           
                endpoints.MapRazorPages();
            });
        }
    }
}
