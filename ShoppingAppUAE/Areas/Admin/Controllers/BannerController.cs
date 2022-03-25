using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ShoppingAppUAE.Areas.Admin.Models.BannerViewModel;
using ShoppingAppUAE.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BannerController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment environment;

        public BannerController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            this.context = context;
            this.environment = environment;
        }
        public IActionResult Index()
        {
            return View(context.Banners.ToList());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(BannerCreateViewModel bannerCreateView)
        {
            if (ModelState.IsValid)
            {
                context.Banners.Add(new Banner()
                {
                    Pragraph = bannerCreateView.Pragraph,
                    Title = bannerCreateView.Title,
                    ImageURL = Helpers.ImageHelpers.ConvertMainImage(bannerCreateView.ImageURL, environment)
                });
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }


            return View(bannerCreateView);
        }
    }
}
