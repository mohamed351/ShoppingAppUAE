
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using ShoppingAppUAE.Data;
using ShoppingAppUAE.Extension;
namespace ShoppingAppUAE.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            var ip = HttpContext.GetRemoteIPAddress().ToString();
            var currentUserID = User.GetUserId();
           var currentUserData =  _context.Users.Find(currentUserID);
           var countryCode  = currentUserData.CountryCode;
            var Shipment = _context.shippingAndDeliveries.Where(a => a.ShippingCountry == countryCode).ToList();

            return View(Shipment);
        }



    }
}
