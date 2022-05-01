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
    public class CurrencyController : Controller
    {
        private readonly ApplicationDbContext context;

        public CurrencyController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {

            return View(context.Currencies.ToList());
        }
        [HttpPost]
        public IActionResult Update([FromBody] List<CurrencyDTO> dTO)
        {
           var currencyCodes = context.Currencies.ToList();
            foreach (var item in dTO)
            {
                var currentCurrency = currencyCodes.FirstOrDefault(a => a.CurrencyCode == item.Code);
                currentCurrency.ExchangeRate = item.ExchangeRate;
                currentCurrency.IsActive = item.IsActive;
            }
            context.SaveChanges();

            return Json("successfull");
        }
        public class CurrencyDTO
        {
            [JsonProperty(PropertyName ="code")]
            public string Code { get; set; }
            [JsonProperty(PropertyName = "exchangeRate")]
            public decimal ExchangeRate { get; set; }

            [JsonProperty(PropertyName = "isActive")]
            public bool IsActive { get; set; }
        }
    }
}
