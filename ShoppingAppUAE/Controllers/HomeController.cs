using AspNetCore.SEOHelper.Sitemap;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using ShoppingAppUAE.Data;
using ShoppingAppUAE.Models;
using ShoppingAppUAE.Models.HomeViewModels;
using ShoppingAppUAE.Models.SitmapViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ShoppingAppUAE.Helpers;
using Microsoft.Extensions.Configuration;

namespace ShoppingAppUAE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment webHost;
        private readonly IActionContextAccessor actionContext;
        private readonly IConfiguration configuration;

        public HomeController(ILogger<HomeController> logger , 
            ApplicationDbContext context ,
            IWebHostEnvironment webHost, 
            IActionContextAccessor actionContext, IConfiguration configuration )
        {
            _logger = logger;
            this.context = context;
            this.webHost = webHost;
            this.actionContext = actionContext;
            this.configuration = configuration;
        }

        public IActionResult Index()
        {

       

            HomePageViewModel viewModel = new HomePageViewModel()
            {
                Banner = context.Banners.Select(a => new BannerViewModel { ID = a.ID,
                    ImageURL = a.ImageURL,
                    Paragraph = a.Pragraph,
                    Title = a.Title ,
                    ArabicParagrap =
                    a.ParagrapArabic, ArabicTitle = a.TitleArabic }).ToList()
            };
          ViewBag.SEO =actionContext.ActionContext.GetURLData(context, configuration);
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult Cookie(string culture , string code)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddMonths(1) }
            );


            Response.Cookies.Append(
            "currency", code
          );

            return RedirectToAction("Index");
        }

     



    }
}
