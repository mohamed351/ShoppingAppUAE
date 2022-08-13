using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoppingAppUAE.Data;
using ShoppingAppUAE.Repository.UnitOfWork;

namespace ShoppingAppUAE.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment environment;

        public CategoriesController(IUnitOfWork unitOfWork, IWebHostEnvironment environment)
        {
         
            this.unitOfWork = unitOfWork;
            this.environment = environment;
        }

        [Authorize(Roles = "Admin,Admin2")]
        public   IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Admin2")]
        public IActionResult GetCategories(int start = 0, int length = 10)
        {
           
            string search = HttpContext.Request.Query["search[value]"];
            return Json(unitOfWork.Categories.GetDataTable(start, length, a => a.IsDeleted == false && (a.CategoryNameEnglish.Contains(search) || a.CategoryNameArabic.Contains(search)),a=> a.CategoryID));
        }
        [Authorize(Roles = "Admin,Admin2")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await unitOfWork.Categories.GetByIDAsync(id.Value);
                
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }
        [Authorize(Roles = "Admin,Admin2")]
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Admin2")]
        public  IActionResult Create([Bind("CategoryID,PaidShipping,CategoryNameArabic,CategoryNameEnglish,CategoryDescriptionArabic,CategoryDescriptionEnglish,ImageURL,ImageAlternativeText,SEOEnglishTitle,SEOArabicTitle,SEOEnglishKeyWords,SEOArabicKeyWords,IsActive, IsShownInHome")] Category category)
        {
            if (ModelState.IsValid)
            {
                category.ImageURL = Helpers.ImageHelpers.ConvertMainImage(category.ImageURL, this.environment);
                unitOfWork.Categories.Add(category);
                 unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Admin/Categories/Edit/5
        [Authorize(Roles = "Admin,Admin2")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await unitOfWork.Categories.GetByIDAsync(id.Value);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Admin2")]
        public  IActionResult Edit(int id, [Bind("CategoryID,PaidShipping,CategoryNameArabic,CategoryNameEnglish,CategoryDescriptionArabic,CategoryDescriptionEnglish,ImageURL,ImageAlternativeText,SEOEnglishTitle,SEOArabicTitle,SEOEnglishKeyWords,SEOArabicKeyWords,IsActive,IsShownInHome")] Category category)
        {
            if (id != category.CategoryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if(category.ImageURL == null)
                    {
                        category.ImageURL = unitOfWork.Categories.GetByIDAsNotTracking(a => a.CategoryID == category.CategoryID).ImageURL;
                    }
                    else
                    {
                        category.ImageURL = Helpers.ImageHelpers.ConvertMainImage(category.ImageURL, this.environment);
                    }
                    unitOfWork.Categories.Edit(category);
                    unitOfWork.Complete();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CategoryID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

       [HttpGet]
        [Authorize(Roles = "Admin,Admin2")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await unitOfWork.Categories
                .GetByIDAsync(id.Value);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Admin2")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {


            var category = await unitOfWork.Categories.GetByIDAsync(id);
           
            category.IsDeleted = true;
            unitOfWork.Complete();
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin,Admin2")]
        private bool CategoryExists(int id)
        {
            return unitOfWork.Categories.AnyEntity(a => a.CategoryID == id);
        }
    }
}
