using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
        private readonly ApplicationDbContext context;

        public ProductsController(IUnitOfWork unitOfWork , IWebHostEnvironment webHostEnvironment, ApplicationDbContext context)
        {
            this.unitOfWork = unitOfWork;
            this.webHostEnvironment = webHostEnvironment;
            this.context = context;
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
            return Json(this.unitOfWork.Products.GetDataTable(start, length,  a =>  (a.ProductNameEnglish.Contains(search) || a.ProductNameArabic.Contains(search)) &&  a.IsDeleted == false, a => a.ProductID) );
        }

        public IActionResult EditPhoto(int ID)
        {
         var product =   context.Products.Include(a => a.ProductImages).FirstOrDefault(a => a.ProductID == ID);
            return View(product);
        }


        [HttpPost]
        public IActionResult EditPhoto([FromBody]ProductEditPhotoViewModel viewModel)
        {
          var imageName =  Helpers.ImageHelpers.ConvertMainImage(viewModel.Base64, webHostEnvironment);
            context.ProductImages.Add(new ProductImage()
            {
                ImagesURL = imageName,
                ProductID = viewModel.ProductId
            });

            context.SaveChanges();

            return Json(viewModel);
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
                    BrandID = productCreateViewModel.BrandID,
                    Weight = productCreateViewModel.Weight,
                    IsNewArrivals = productCreateViewModel.IsArravel,
                    IsTop10 = productCreateViewModel.IsTop10,
                    BreifDescriptionArabic = productCreateViewModel.BreifDescriptionArabic,
                    BreifDescriptionEnglish = productCreateViewModel.BreifDescriptionEnglish,
                    VideoUrl = productCreateViewModel.VideoIFrameURL
                   

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
                BrandID = product.BrandID,
                Weight = product.Weight,
                IsArravel = product.IsNewArrivals,
                IsTop10 =product.IsTop10,
                BreifDescriptionArabic = product.BreifDescriptionArabic,
                BreifDescriptionEnglish = product.BreifDescriptionEnglish,
                ImageAlternativeText = product.ImageAlternativeText,
                VideoIFrameURL = product.VideoUrl
               
               
                

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
                    Quantity = product.Quantity,
                    ProductNameArabic = productEditViewModel.ProductNameArabic,
                    ProductNameEnglish = productEditViewModel.ProductNameEnglish,
                    PurchasingPrice = productEditViewModel.PurchasingPrice,
                    SellingPrice = productEditViewModel.SellingPrice,
                    SEOArabicKeyWords = productEditViewModel.SEOArabicKeyWords,
                    SEOArabicTitle = productEditViewModel.SEOArabicTitle,
                    SEOEnglishKeyWords = productEditViewModel.SEOEnglishKeyWords,
                    SEOEnglishTitle = productEditViewModel.SEOEnglishTitle,
                    BrandID = productEditViewModel.BrandID,
                    Weight = productEditViewModel.Weight,
                    IsTop10 = productEditViewModel.IsTop10,
                    IsNewArrivals= productEditViewModel.IsArravel,
                    VideoUrl = productEditViewModel.VideoIFrameURL,
                    BreifDescriptionArabic = productEditViewModel.BreifDescriptionArabic,
                    BreifDescriptionEnglish = productEditViewModel.BreifDescriptionEnglish
                };
                unitOfWork.Products.Edit(editedProduct);
                unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Brand = new SelectList(this.unitOfWork.Brands.GetAll(), "ID", "BrandNameEnglish");
            ViewBag.Categories = new SelectList(this.unitOfWork.Categories.GetAll(), "CategoryID", "CategoryNameEnglish", productEditViewModel.CategoryID);
            return View(productEditViewModel);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await unitOfWork.Products
                .GetByIDAsync(id.Value);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {


            var product = await unitOfWork.Products.GetByIDAsync(id);

            product.IsDeleted = true;
            unitOfWork.Complete();
            return RedirectToAction(nameof(Index));
        }

        //[HttpPost]
        //public IActionResult Delete([FromBody]int? ID)
        //{
        //    if(ID == null)
        //    {
        //        return BadRequest("The ID is not Specified");
        //    }
        //    var product = unitOfWork.Products.GetByID(ID.Value);

        //    if(product == null)
        //    {
        //        return NotFound("The Product is not found");

        //    }

        //    product.IsDeleted = true;
        //    unitOfWork.Complete();
        //    return NoContent();
        //}



    }
}
