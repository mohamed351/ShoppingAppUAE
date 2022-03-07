using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public CategoriesController(IUnitOfWork unitOfWork)
        {
         
            this.unitOfWork = unitOfWork;
        }

       
        public   IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetCategories(int start = 0, int length = 10)
        {
           
            string search = HttpContext.Request.Query["search[value]"];
            return Json(unitOfWork.Categories.GetDataTable(start, length, a => a.CategoryNameEnglish.Contains(search) || a.CategoryNameArabic.Contains(search),a=> a.CategoryID));
        }

  
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

      
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Create([Bind("CategoryID,CategoryNameArabic,CategoryNameEnglish,CategoryDescriptionArabic,CategoryDescriptionEnglish,ImageURL,ImageAlternativeText,SEOEnglishTitle,SEOArabicTitle,SEOEnglishKeyWords,SEOArabicKeyWords,IsActive")] Category category)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Categories.Add(category);
                 unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Admin/Categories/Edit/5
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

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Edit(int id, [Bind("CategoryID,CategoryNameArabic,CategoryNameEnglish,CategoryDescriptionArabic,CategoryDescriptionEnglish,ImageURL,ImageAlternativeText,SEOEnglishTitle,SEOArabicTitle,SEOEnglishKeyWords,SEOArabicKeyWords,IsActive")] Category category)
        {
            if (id != category.CategoryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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

        // GET: Admin/Categories/Delete/5
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

        // POST: Admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await unitOfWork.Categories.GetByIDAsync(id);
            unitOfWork.Categories.Delete(category);
            unitOfWork.Complete();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return unitOfWork.Categories.AnyEntity(a => a.CategoryID == id);
        }
    }
}
