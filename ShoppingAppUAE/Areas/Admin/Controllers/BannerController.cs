using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                    ImageURL = Helpers.ImageHelpers.ConvertMainImage(bannerCreateView.ImageURL, environment),
                    ParagrapArabic =bannerCreateView.ArabicParagraph,
                    TitleArabic = bannerCreateView.ArabicTitle
                });
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }


            return View(bannerCreateView);
        }
   
        public IActionResult Edit(int? ID)
        {
            if(ID == null)
            {
                return BadRequest("The ID is not Exist");
            }
            var currentBanner = context.Banners.Find(ID);
            if(currentBanner == null)
            {
                return NotFound("The Banner is not found");
            }

            //auto mapper 
            var bannerMapping = new BannerEditViewModel()
            {
                ArabicParagraph = currentBanner.ParagrapArabic,
                ID = currentBanner.ID,
                ArabicTitle = currentBanner.TitleArabic,
                ImageURL = currentBanner.ImageURL,
                Pragraph = currentBanner.Pragraph,
                Title = currentBanner.Title,
              
            };

            return View(bannerMapping);
        }
        [HttpPost]
        public IActionResult Edit([FromForm]BannerEditViewModel bannerEditViewModel)
        {
            if (ModelState.IsValid)
            {
                var currentBanner = context.Banners.Find(bannerEditViewModel.ID);
               if(currentBanner == null)
                {
                    return BadRequest("The Banner is not found");

                }
                currentBanner.ImageURL = bannerEditViewModel.ImageURL == null ? currentBanner.ImageURL : Helpers.ImageHelpers.ConvertMainImage(bannerEditViewModel.ImageURL, environment);
                currentBanner.ParagrapArabic = bannerEditViewModel.ArabicParagraph;
                currentBanner.Pragraph = bannerEditViewModel.Pragraph;
                currentBanner.Title = bannerEditViewModel.Title;
                currentBanner.TitleArabic = bannerEditViewModel.ArabicTitle;
                
                context.SaveChanges();
                return RedirectToAction(nameof(Index));


            }

            return View(bannerEditViewModel);

        }

        public IActionResult Details(int? ID)
        {
            if(ID == null)
            {
                return BadRequest("The ID is not found");
            }
            var currentBanner = context.Banners.Find(ID);
            if(currentBanner == null)
            {
                return NotFound("The Banner is not found");

            }

            return View(currentBanner);


        }

        [HttpDelete]
        public IActionResult Delete(int? ID)
        {
            if(ID == null)
            {
                return BadRequest("The ID is not found");
            }
           var currentBanner = context.Banners.Find(ID);
            if(currentBanner == null)
            {
                return NotFound("The Banner is not found");
            }
            context.Banners.Remove(currentBanner);
            return Json(currentBanner);
            

        }
    }
}
