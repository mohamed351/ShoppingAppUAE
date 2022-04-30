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
            CheckOutViewModel viewModel = new CheckOutViewModel()
            {
                ApplicationUser = context.Users.Find(userId),

            };
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult SubmitCheckout()
        {
            return View();
        }

        
     
        [HttpPost]
        public IActionResult Create([FromBody]PaymentIntentCreateRequest request)
        {
            

            var paymentIntentService = new PaymentIntentService();
            var paymentIntent = paymentIntentService.Create(new PaymentIntentCreateOptions
            {
                Amount = CalculateOrderAmount(request.Items),
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

        private long CalculateOrderAmount(Item[] items)
        {
            decimal totalPrice = 0;
            foreach (var item in items)
            {
                totalPrice += item.Price * item.Quantity;   
            }
            return Convert.ToInt64(totalPrice * 100);
        }
    }
}
