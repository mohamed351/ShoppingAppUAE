using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShoppingAppUAE.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class InitalStoreController : Controller
    {
        private readonly ApplicationDbContext context;

        public InitalStoreController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            return View(context.Products.ToList());
        }

        [HttpPost]
        public IActionResult Update([FromBody] List<ProductStoreDTO> dTO)
        {
            var products = context.Products.ToList();
            foreach (var item in dTO)
            {
                var currentProduct = products.FirstOrDefault(a => a.ProductID == item.ID);
                currentProduct.Quantity = item.Quantity;
               
            }
            context.SaveChanges();

            return Json("successfull");
        }

        public class ProductStoreDTO
        {
            [JsonProperty(PropertyName = "id")]
            public int ID { get; set; }
            [JsonProperty(PropertyName = "quantity")]
            public int Quantity { get; set; }
        }


    }
}
