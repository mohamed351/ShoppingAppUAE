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
            var user = context.Users.Find(userId);
            CheckOutViewModel viewModel = new CheckOutViewModel()
            {
                ApplicationUser = user,
                ShippingDetails = context.shippingAndDeliveries.Where(a=> a.ShippingCountry == user.CountryCode).ToList()

            };
           
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult SubmitCheckout([FromBody]PaymentCompletedViewModel paymentCompletedView)
        {

           var currency = context.Currencies.Find(paymentCompletedView.CurrenyCode);
            if (ModelState.IsValid)
            {


                var order = new Data.Order()
                {
                    TotalShippingFees = paymentCompletedView.ShipmentFees,
                    Address = paymentCompletedView.Address,
                    UserID = User.GetUserId(),
                    City = paymentCompletedView.City,
                    CurrencyCode = paymentCompletedView.CurrenyCode,
                    DateTimeIssued = DateTime.Now,
                    FirstName = paymentCompletedView.FirstName,
                    LastName = paymentCompletedView.LastName,
                    ExchangeRate = currency.ExchangeRate,
                    Note = paymentCompletedView.OrderNote,
                    OrderGUID = Guid.NewGuid().ToString(),
                    ReferenceID = "",
                    ShippingAndDeliveryShippingID = paymentCompletedView.ShipmentID,
                    Total = (paymentCompletedView.PaymentCarts.Sum(a => a.Quantity * a.Price) + paymentCompletedView.ShipmentFees) * currency.ExchangeRate,
                    OrderDetails = new List<OrderDetails>()
                };
                foreach (var item in paymentCompletedView.PaymentCarts)
                {
                    var orderDetail = new OrderDetails()
                    {
                        CurrencyCode = paymentCompletedView.CurrenyCode,
                        ExchangeRate = currency.ExchangeRate,
                        Price = item.Price * currency.ExchangeRate,
                        ProductID = item.ProductId,
                        Quantity = item.Quantity,
                        Total = (item.Quantity * item.Price) * currency.ExchangeRate,

                    };
                    order.OrderDetails.Add(orderDetail);


                }



                context.orders.Add(order);
               
                return Json("SUCCESS");
            }
            return Json("DATA_NOT_COMPLETED");

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
