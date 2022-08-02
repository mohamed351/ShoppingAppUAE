using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingAppUAE.Data;
using System.Linq;
using ShoppingAppUAE.Extension;
using Microsoft.EntityFrameworkCore;
using ShoppingAppUAE.Helpers;

namespace ShoppingAppUAE.Controllers
{

    [Authorize]
    public class WishListController : Controller
    {
        private readonly ApplicationDbContext context;

        public WishListController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var userId = User.GetUserId();
            var query = context.WishLists.Include(a=> a.Product).Where(a => a.UserID == userId).ToList();
            SetCurrencyCookie();
            return View(query);
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
    }
}
