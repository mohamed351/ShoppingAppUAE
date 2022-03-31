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
            ViewBag.Brand = new SelectList(this.unitOfWork.Brands.GetAll(), "ID", "BrandNameEnglish");
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
                    BrandID = productCreateViewModel.BrandID

                };
                unitOfWork.Products.Add(product);
                unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
              

            }
            ViewBag.Brand = new SelectList(this.unitOfWork.Brands.GetAll(), "ID", "BrandNameEnglish");
            ViewBag.Categories = new SelectList(this.unitOfWork.Categories.GetAll(), "CategoryID", "CategoryNameEnglish", productCreateViewModel.CategoryID);
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
            var productEditViewModel = new ProductEditViewModel()
            {
                CategoryID = product.CategoryID,
                DescriptionArabic = product.DescriptionArabic,
                ProductNameEnglish = product.ProductNameEnglish,
                DescriptionEnglish = product.DescriptionEnglish,
                Discount = product.Discount,
                ProductID = product.ProductID,
                ProductNameArabic = product.ProductNameArabic,
                PurchasingPrice = product.PurchasingPrice,
                Quantity = product.Quantity,
                SellingPrice = product.SellingPrice,
                ImageURL = product.ImageURL,
                SEOArabicKeyWords = product.SEOArabicKeyWords,
                SEOArabicTitle = product.SEOArabicTitle,
                SEOEnglishKeyWords = product.SEOEnglishKeyWords,
                SEOEnglishTitle = product.SEOEnglishTitle,
                BrandID = product.BrandID
               
                

            };
            ViewBag.Brand = new SelectList(this.unitOfWork.Brands.GetAll(), "ID", "BrandNameEnglish",productEditViewModel.BrandID );
            ViewBag.Categories = new SelectList(this.unitOfWork.Categories.GetAll(), "CategoryID", "CategoryNameEnglish",productEditViewModel.CategoryID);
            return View(productEditViewModel);
        }
        [HttpPost]
        public IActionResult Edit(ProductEditViewModel productEditViewModel)
        {
            if (ModelState.IsValid)
            {
                var product = unitOfWork.Products.GetByIDAsNotTracking(a => a.ProductID == productEditViewModel.ProductID);
                //use auto mapper later


                var editedProduct = new Product()
                {
                    CategoryID = productEditViewModel.CategoryID,
                    DescriptionArabic = productEditViewModel.DescriptionArabic,
                    DescriptionEnglish = productEditViewModel.DescriptionEnglish,
                    Discount = productEditViewModel.Discount,
                    ImageURL = productEditViewModel.ImageURL == null ? product.ImageURL : ImageHelpers.ConvertMainImage(productEditViewModel.ImageURL, webHostEnvironment),
                    ImageAlternativeText = productEditViewModel.ImageAlternativeText,
                    IsActive = true,
                    ProductID = product.ProductID,
                    ProductNameArabic = productEditViewModel.ProductNameArabic,
                    ProductNameEnglish = productEditViewModel.ProductNameEnglish,
                    PurchasingPrice = productEditViewModel.PurchasingPrice,
                    SellingPrice = productEditViewModel.SellingPrice,
                    Quantity = productEditViewModel.Quantity,
                    SEOArabicKeyWords = productEditViewModel.SEOArabicKeyWords,
                    SEOArabicTitle = productEditViewModel.SEOArabicTitle,
                    SEOEnglishKeyWords = productEditViewModel.SEOEnglishKeyWords,
                    SEOEnglishTitle = productEditViewModel.SEOEnglishTitle,
                    BrandID = productEditViewModel.BrandID,
                };
                unitOfWork.Products.Edit(editedProduct);
                unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Brand = new SelectList(this.unitOfWork.Brands.GetAll(), "ID", "BrandNameEnglish");
            ViewBag.Categories = new SelectList(this.unitOfWork.Categories.GetAll(), "CategoryID", "CategoryNameEnglish", productEditViewModel.CategoryID);
            return View(productEditViewModel);
        }

        [HttpPost]
        public IActionResult Delete([FromBody]int? ID)
        {
            if(ID == null)
            {
                return BadRequest("The ID is not Specified");
            }
            var product = unitOfWork.Products.GetByID(ID.Value);

            if(product == null)
            {
                return NotFound("The Product is not found");

            }

            unitOfWork.Products.Delete(product);
            unitOfWork.Complete();
            return NoContent();
        }



    }
}
