using AspNetCore.SEOHelper.Sitemap;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
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

namespace ShoppingAppUAE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment webHost;

        public HomeController(ILogger<HomeController> logger , ApplicationDbContext context , IWebHostEnvironment webHost )
        {
            _logger = logger;
            this.context = context;
            this.webHost = webHost;
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
        public IActionResult Cookie(string culture)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddMonths(1) }
            );

            return RedirectToAction("Index");
        }



    }
}
