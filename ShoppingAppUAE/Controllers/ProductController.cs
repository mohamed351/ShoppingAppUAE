using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingAppUAE.Data;
using ShoppingAppUAE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext context;

        public ProductController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {

            var ProductViewModel = new ProductViewModel()
            {
                CategoryViewModels = context.Categories.Select(a => new CategoryViewModel()
                {
                    ID = a.CategoryID,
                    NameArabic = a.CategoryNameArabic,
                    NameEnglish = a.CategoryNameEnglish
                }).ToList()
            };
           
            return View(ProductViewModel);
        }
        [HttpGet]
        public IActionResult GetProducts(ProductSelectionViewModel productViewModel)
        {
            IQueryable<Product> products = null;
            int totalProduct = 0;
            if (productViewModel.CategoryID == null)
            {
                products = context.Products;
                totalProduct = context.Products.Count();
            }
            else
            {
                products = context.Products.Where(a => a.CategoryID == productViewModel.CategoryID);
                totalProduct = context.Products.Where(a => a.CategoryID == productViewModel.CategoryID).Count();
            }


            var product = products.Select(a => new {
                a.ProductID,
                a.ProductNameEnglish,
                a.ProductNameArabic,
                a.PurchasingPrice,
                a.ImageURL
            }).OrderBy(a => a.ProductID).Skip((productViewModel.pageNumber - 1) * productViewModel.pageSize)
              .Take(productViewModel.pageSize)
              .ToList();




            return Json(new { product, totalProduct });

        }
        [HttpGet]
        public IActionResult CurrentProduct(int? ID)
        {
            if(ID == null)
            {
                return BadRequest("The ID is not specifed");
            }
            var product = context.Products.Include(a => a.Category).FirstOrDefault(a => a.ProductID == ID);
            if(product == null)
            {
                return NotFound("The Product is not found");
            }
            return View(product);
        }
    }
}
