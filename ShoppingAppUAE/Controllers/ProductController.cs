using Microsoft.AspNetCore.Mvc;
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
    }
}
