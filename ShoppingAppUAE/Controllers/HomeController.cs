using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShoppingAppUAE.Data;
using ShoppingAppUAE.Models;
using ShoppingAppUAE.Models.HomeViewModels;
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

        public HomeController(ILogger<HomeController> logger , ApplicationDbContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public IActionResult Index()
        {

            HomePageViewModel viewModel = new HomePageViewModel()
            {
                Banner = context.Banners.Select(a => new BannerViewModel { ID = a.ID, ImageURL = a.ImageURL, Paragraph = a.Pragraph, Title = a.Title }).ToList()
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
    }
}
