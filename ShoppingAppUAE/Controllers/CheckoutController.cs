using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingAppUAE.Extension;
using Stripe;
using ShoppingAppUAE.Data;
using Stripe.Checkout;
using ShoppingAppUAE.Models.CheckoutViewModels;

namespace ShoppingAppUAE.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly ApplicationDbContext context;

        public CheckoutController(ApplicationDbContext context )
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            string userId = User.GetUserId();
            return View();
        }
        [HttpPost]
        public IActionResult Create(PaymentIntentCreateRequest request)
        {


            var paymentIntentService = new PaymentIntentService();
            var paymentIntent = paymentIntentService.Create(new PaymentIntentCreateOptions
            {
                Amount = 1400,
                Currency = "usd",
                //Shipping = new ChargeShippingOptions()
                //{
                //    TrackingNumber
                //},
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = true,
                },
            });

            return Json(new { clientSecret = paymentIntent.ClientSecret });
        
        }
        [HttpGet]
        public IActionResult PaymentCompleted()
        {
            return View();
        }

        private int CalculateOrderAmount(Item[] items)
        {
            // Replace this constant with a calculation of the order's amount
            // Calculate the order total on the server to prevent
            // people from directly manipulating the amount on the client
            return 1400;
        }
    }
}
