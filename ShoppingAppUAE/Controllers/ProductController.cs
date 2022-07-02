using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingAppUAE.Data;
using ShoppingAppUAE.Helpers;
using ShoppingAppUAE.Models;
using ShoppingAppUAE.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IUnitOfWork unit;

        public ProductController(ApplicationDbContext context, IUnitOfWork unit)
        {
            this.context = context;
            this.unit = unit;
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
                BrandViewModel = context.Brands.Select(a => new BrandViewModel() { ID = a.ID, ArabicName = a.BrandNameArabic, EnglishName = a.BrandNameEnglish }).ToList(),

                MaxValue = unit.Products.GetMaxPurchasingPrice(),
                MinValue = unit.Products.GetMinPurchasingPrice()

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
            if (productViewModel.MinValue != null && productViewModel.MaxValue != null)
                products = products.Where(a => a.PurchasingPrice <= productViewModel.MaxValue && a.PurchasingPrice >= productViewModel.MinValue);


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

            var currentQuery = context.Products.Where(a => query.producIds.Contains(a.ProductID)).Select(a => new { a.ProductID, a.PurchasingPrice, a.Weight });

            return Json(currentQuery);
        }
        [HttpGet]
        public IActionResult Search([FromQuery] string search, [FromQuery] int page = 1, ProductSort sort = 0)
        {
            int currentPage = (page - 1)*12;
            ViewBag.Search = search;
            ViewBag.PageNumber = page;
            ViewBag.sort = (int) sort;
           var  selectedCurrency  = HttpContext.Request.GetCookieValue("currency");
            if (selectedCurrency == null)
                ViewBag.Currency = (Currency)context.Currencies.FirstOrDefault(a => a.IsDefault);
            else
                ViewBag.Currency = (Currency)context.Currencies.Find(selectedCurrency);

           
            switch (sort)
            {
                case ProductSort.ProductID:
                  return View( unit.Products.SeacrchProduct(currentPage, 12, a => a.ProductNameEnglish.Contains(search) ||
                     a.ProductNameArabic.Contains(search) ||
                     a.Brand.BrandNameArabic.Contains(search) || a.Brand.BrandNameEnglish.Contains(search) || a.Category.CategoryNameArabic.Contains(search) || a.Category.CategoryNameEnglish.Contains(search), a => a.ProductID));
                    
                case ProductSort.HigherPrice:
                    return View(unit.Products.SeacrchProductSortPriceHighertoLower(currentPage, 12, a => a.ProductNameEnglish.Contains(search) ||
                      a.ProductNameArabic.Contains(search) ||
                     a.Brand.BrandNameArabic.Contains(search) || a.Brand.BrandNameEnglish.Contains(search) || a.Category.CategoryNameArabic.Contains(search) || a.Category.CategoryNameEnglish.Contains(search)));
                  
                    
                case ProductSort.LowPrice:
                    return View(unit.Products.SeacrchProductSortPriceHighertoLower(currentPage, 12, a => a.ProductNameEnglish.Contains(search) ||
                     a.ProductNameArabic.Contains(search) ||
                     a.Brand.BrandNameArabic.Contains(search) || a.Brand.BrandNameEnglish.Contains(search) || a.Category.CategoryNameArabic.Contains(search) || a.Category.CategoryNameEnglish.Contains(search)));

                default:
                    return View(unit.Products.SeacrchProduct(currentPage, 12, a => a.ProductNameEnglish.Contains(search) ||
                       a.ProductNameArabic.Contains(search) ||
                       a.Brand.BrandNameArabic.Contains(search) || a.Brand.BrandNameEnglish.Contains(search) || a.Category.CategoryNameArabic.Contains(search) || a.Category.CategoryNameEnglish.Contains(search), a => a.ProductID));

            }


        }


        [HttpGet]
        public IActionResult SearchbyCategory([FromQuery] int? search, [FromQuery] int page = 1, ProductSort sort = 0)
        {
            int currentPage = (page - 1) * 12;
            ViewBag.search = search;
            ViewBag.PageNumber = page;
            ViewBag.sort = (int)sort;
           var category = context.Categories.Find(search);
            ViewBag.categoryArabic = category.CategoryNameArabic;
            ViewBag.categoryEnglish = category.CategoryNameEnglish;
            var selectedCurrency = HttpContext.Request.GetCookieValue("currency");
            if (selectedCurrency == null)
                ViewBag.Currency = (Currency)context.Currencies.FirstOrDefault(a => a.IsDefault);
            else
                ViewBag.Currency = (Currency)context.Currencies.Find(selectedCurrency);


            switch (sort)
            {
                case ProductSort.ProductID:
                    return View(unit.Products.SeacrchProduct(currentPage, 12, a => a.CategoryID == search, a => a.ProductID));

                case ProductSort.HigherPrice:
                    return View(unit.Products.SeacrchProductSortPriceHighertoLower(currentPage, 12, a => a.CategoryID == search));


                case ProductSort.LowPrice:
                    return View(unit.Products.SeacrchProductSortPriceHighertoLower(currentPage, 12, a=> a.CategoryID == search));

                default:
                    return View(unit.Products.SeacrchProduct(currentPage, 12, a=>a.CategoryID == search, a => a.ProductID));

            }


        }
        public enum ProductSort
        {
            ProductID =0,
            HigherPrice =1,
            LowPrice =2
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
            if (ID == null)
            {
                return BadRequest("The ID is not specifed");
            }
            var product = context.Products.Include(a => a.Category).Include(a => a.ProductImages).FirstOrDefault(a => a.ProductID == ID);
            if (product == null)
            {
                return NotFound("The Product is not found");
            }
            //get currency from Cookies
            SetCurrencyCookie();

            return View(product);
        }

        private void SetCurrencyCookie()
        {
            var currency = CookiesHelper.GetCookieValue(this.HttpContext.Request, "currency");
            if (string.IsNullOrEmpty(currency))
            {
                var defaultCurrency = context.Currencies.FirstOrDefault(a => a.IsDefault);
                ViewBag.Currency = defaultCurrency.CurrencyCode;
                ViewBag.ExchangeRate = defaultCurrency.ExchangeRate;
            }
            else
            {
                var currentCurreny = context.Currencies.FirstOrDefault(a => a.CurrencyCode == currency);
                ViewBag.Currency = currentCurreny.CurrencyCode;
                ViewBag.ExchangeRate = currentCurreny.ExchangeRate;
            }
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
