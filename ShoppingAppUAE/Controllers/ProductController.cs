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
                }).ToList(),
                BrandViewModel = context.Brands.Select(a => new BrandViewModel() { ID = a.ID, ArabicName = a.BrandNameArabic, EnglishName = a.BrandNameEnglish }).ToList()

               
            };
           
            return View(ProductViewModel);
        }
        [HttpGet]
        public IActionResult GetProducts(ProductSelectionViewModel productViewModel)
        {
            IQueryable<Product> products = null;
            int totalProduct = 0;
            
                products = context.Products;

            if (productViewModel.CategoryID != null)
                products = products.Where(a => a.CategoryID == productViewModel.CategoryID);
            if (productViewModel.BrandID != null)
                products = products.Where(a => a.BrandID == productViewModel.BrandID);
            if (productViewModel.Search != null)
                products = products.Where(a => a.ProductNameArabic.Contains(productViewModel.Search) || a.ProductNameEnglish.Contains(productViewModel.Search));





            totalProduct = products.Count();



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
        [HttpPost()]
        public IActionResult GetProductPrices([FromBody] ProductQuery query)
        {
           var currentQuery =  context.Products.Where(a => query.producIds.Contains(a.ProductID)).Select(a=> new {  a.ProductID , a.PurchasingPrice});
            return Json(currentQuery);
        }

        public class ProductQuery
        {
            public ProductQuery()
            {
                this.producIds = new List<int>();
            }
            public List<int> producIds { get; set; }
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

        public IActionResult AddToCardModel(int? ID)
        {
            if(ID == null)
            {
                return BadRequest("The ID is not found");
            }
          var product =  context.Products.Find(ID);
            if(product == null)
            {
                return NotFound("The Product is not Found");
            }

            return PartialView(product);
        }
    }
}
