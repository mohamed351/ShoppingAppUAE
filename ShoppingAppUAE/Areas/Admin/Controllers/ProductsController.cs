using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingAppUAE.Areas.Admin.Models.ProductsViewModel;
using ShoppingAppUAE.Data;
using ShoppingAppUAE.Helpers;
using ShoppingAppUAE.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductsController(IUnitOfWork unitOfWork , IWebHostEnvironment webHostEnvironment)
        {
            this.unitOfWork = unitOfWork;
            this.webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {

            ViewBag.Categories = new SelectList(this.unitOfWork.Categories.GetAll(), "CategoryID", "CategoryNameEnglish");
            return View();
        }

        public IActionResult GetProducts(int start = 0, int length = 10)
        {
            string search = HttpContext.Request.Query["search[value]"];
            return Json(this.unitOfWork.Products.GetDataTable(start, length, a => a.ProductNameEnglish.Contains(search) || a.ProductNameArabic.Contains(search), a => a.ProductID));
        }
        [HttpPost()]
        public IActionResult Create(ProductCreateViewModel productCreateViewModel)
        {
            if (ModelState.IsValid)
            {

                //autoMapper or onther solution
                var product = new Product()
                {
                    CategoryID = productCreateViewModel.CategoryID,
                    DescriptionArabic = productCreateViewModel.DescriptionArabic,
                    DescriptionEnglish = productCreateViewModel.DescriptionEnglish,
                    Discount = productCreateViewModel.Discount,
                    ImageURL = ImageHelpers.ConvertMainImage(productCreateViewModel.ImageURL, webHostEnvironment),
                    IsActive = true,
                    ProductNameArabic = productCreateViewModel.ProductNameArabic,
                    ProductNameEnglish = productCreateViewModel.ProductNameEnglish,
                    PurchasingPrice = productCreateViewModel.PurchasingPrice,
                    SellingPrice = productCreateViewModel.SellingPrice,
                    Quantity = productCreateViewModel.Quantity,
                    SEOArabicKeyWords = productCreateViewModel.SEOArabicKeyWords,
                    SEOEnglishKeyWords = productCreateViewModel.SEOEnglishKeyWords,
                    SEOArabicTitle = productCreateViewModel.SEOArabicTitle,
                    SEOEnglishTitle = productCreateViewModel.SEOEnglishTitle,
                    ImageAlternativeText ="",

                };
                unitOfWork.Products.Add(product);
                unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
              

            }

            return View(productCreateViewModel);

        }

        public IActionResult Edit(int? ID)
        {
            if(ID == null)
            {
                return BadRequest("The ID is not specified");
            }
          var product =  unitOfWork.Products.GetByID(ID.Value);
            if(product == null)
            {
                return NotFound("The Product is not found");
            }

            ViewBag.Categories = new SelectList(this.unitOfWork.Categories.GetAll(), "CategoryID", "CategoryNameEnglish", product.CategoryID);
            return View(product);
        }
        [HttpPost]
        public IActionResult Edit(ProductEditViewModel productEditViewModel)
        {
            return View(productEditViewModel);
        }

        
    }
}
