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
using Stimulsoft.Report;
using Stimulsoft.Report.Web;
using Stimulsoft.Report.Dictionary;
using Microsoft.Extensions.Configuration;
using Stimulsoft.Report.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ShoppingAppUAE.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IConfiguration configuration;

        public CheckoutController(ApplicationDbContext context, IConfiguration configuration)
        {
            Stimulsoft.Base.StiLicense.LoadFromFile("license.key");
            this.context = context;
            this.configuration = configuration;
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
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    string orderGUID = Guid.NewGuid().ToString();
                    var userId = User.GetUserId();
                    var user = context.Users.Find(userId);
                    var currency = context.Currencies.Find(paymentCompletedView.CurrenyCode);

                    if (ModelState.IsValid)
                    {


                     
                        var order = new Data.Order()
                        {
                            TotalShippingFees = paymentCompletedView.ShipmentFees,
                            Address = paymentCompletedView.Address,
                            UserID = userId,
                            City = paymentCompletedView.City,
                            CurrencyCode = paymentCompletedView.CurrenyCode,
                            DateTimeIssued = DateTime.Now,
                            FirstName = paymentCompletedView.FirstName,
                            LastName = paymentCompletedView.LastName,
                            ExchangeRate = currency.ExchangeRate,
                            Note = paymentCompletedView.OrderNote,
                            OrderGUID = orderGUID,
                            ReferenceID = "",
                            OrderAddress = paymentCompletedView.Address,
                            OrderEmail = paymentCompletedView.Email,
                            OrderPhone = paymentCompletedView.PhonePrefix + paymentCompletedView.PhoneNumber,
                            OriginalEmail = user.Email,
                            OriginalPhone = user.PhonePrefix + "" + user.PhoneNumber,
                            Approved = false,
                            InvoiceNumber = MaxInvoiceNumber() + 1,
                            ShippingAndDeliveryShippingID = paymentCompletedView.ShipmentID,
                            Total = (paymentCompletedView.PaymentCarts.Sum(a => a.Quantity * a.Price) + paymentCompletedView.ShipmentFees) * currency.ExchangeRate,
                            OrderDetails = new List<OrderDetails>(),
                            OrderType = paymentCompletedView.OrderType,
                            StripID = paymentCompletedView.StripId
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
                        context.SaveChanges();
                        transaction.Commit();

                        return Json(new { status = "SUCCESS", id = orderGUID });
                    }
                    return Json("DATA_NOT_COMPLETED");

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return BadRequest(ex.Message);
                }
            }

        }

       

        public void DecreseQuantity(List<OrderDetails> orderDetails)
        {
            foreach (var item in orderDetails)
            {
             
                var product =  this.context.Products.FirstOrDefault(a => a.ProductID == item.ProductID);
                product.Quantity =  product.Quantity - Convert.ToInt32(item.Quantity);
                if(product.Quantity < 0)
                {
                    throw new Exception("Sorry the Quantity is not enough");
                }
            }

         

        }

        public int MaxInvoiceNumber()
        {
           if( this.context.orders.Count() == 0)
            {
                return 0;
            }
            else
            {
              return   this.context.orders.Max(a => a.InvoiceNumber);
            }
        }



        [HttpPost]
        public IActionResult PaymentCompletedSripe()
        {

            return View();
        }

      

        
     
        [HttpPost]
        public IActionResult Create([FromBody]PaymentIntentCreateRequest request)
        {
            var currency = context.Currencies.Find(request.CurrencyCode);
            var userID = User.GetUserId();
            var currentUser = context.Users.Find(userID);
            var paymentIntentService = new PaymentIntentService();
            var customerService = new CustomerService();
            

              Customer  userStripe = CreateUser(currentUser, customerService);



            var paymentIntent = paymentIntentService.Create(new PaymentIntentCreateOptions
            {

                Amount = CalculateOrderAmount(request.Items, currency.ExchangeRate, request.ShippingFees),
                Currency = request.CurrencyCode.ToLower(),
                //Shipping = new ChargeShippingOptions()
                //{
                //    TrackingNumber
                //},
                Customer = userStripe.Id,
                Shipping = new ChargeShippingOptions()
                {
                    Name = currentUser.FirstName,
                    Phone = currentUser.PhoneNumber,
                    Address = new AddressOptions()
                    {
                        City = currentUser.City,
                        Country = currentUser.CountryCode,
                        Line1 = currentUser.Address,
                        State = currentUser.City,
                        
                    },
                   

                },
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = true,
                },
            });

            return Json(new { clientSecret = paymentIntent.ClientSecret });

        }

        private static Customer CreateUser(ApplicationUser currentUser, CustomerService customerService)
        {
           
            
            var userStripe = customerService.Search(new CustomerSearchOptions
            {
                Query = "email:'"+ currentUser.Email+ "'",
            });

            if (userStripe.Count() != 0)
            {
                var customer = userStripe.FirstOrDefault(a => a.Email == currentUser.Email);

                if (customer == null)
                {
                  return   customerService.Create(new CustomerCreateOptions()
                    {
                        Address = new AddressOptions()
                        {
                            City = currentUser.City,
                            Country = currentUser.CountryCode,
                            Line1 = currentUser.Address,
                            State = currentUser.City
                        },
                        Email = currentUser.Email,
                        Phone = currentUser.PhonePrefix + " " + currentUser.PhoneNumber,
                        Name = currentUser.FirstName + "," + currentUser.LastName,


                    });
                }
                else
                {
                    return customer;
                }
            }
            else
            {
              return  customerService.Create(new CustomerCreateOptions()
                {
                    Address = new AddressOptions()
                    {
                        City = currentUser.City,
                        Country = currentUser.CountryCode,
                        Line1 = currentUser.Address,
                        State = currentUser.City
                    },
                    Email = currentUser.Email,
                    Phone = currentUser.PhonePrefix + " " + currentUser.PhoneNumber,
                    Name = currentUser.FirstName + "," + currentUser.LastName,


                });

            }

          
        }

        [HttpGet]
        public IActionResult PaymentCompleted([FromQuery]string payment_intent ,[FromQuery]string payment_intent_client_secret,[FromQuery] string redirect_status, [FromQuery]string type, [FromQuery] string id)
        {
            var user = User.GetUserId();

        
            var order = context.orders.Include(a=>a.OrderDetails).FirstOrDefault(a => a.UserID == user && a.OrderGUID == id);
            //DecreseQuantity(order.OrderDetails.ToList());
            context.SaveChanges();
            if (type == "card")
            {
               
               
                order.ReferenceID = payment_intent;
                context.SaveChanges();
                return View();

            }
            
        
            return View();
        }
        public IActionResult ViewerEvent()
        {
            return StiNetCoreViewer.ViewerEventResult(this);
        }

        public IActionResult GetInvoice([FromQuery] string ID)
        {
            var user = User.GetUserId();

            var order = context.orders.FirstOrDefault(a => a.UserID == user && a.OrderGUID == ID);
            if(order == null)
            {
                return NotFound();
            }
            StiReport report = new StiReport();
            report.Load("Reports/OrderInvoice.mrt");
            report["@OrderID"] = order.OrderID;
            report.Dictionary.Databases.Add(new StiSqlDatabase("Connection", configuration.GetConnectionString("DefaultConnection")));

            report.Render();

            return StiNetCoreViewer.GetReportResult(this, report);
        }

        public IActionResult InvoicePDF(int? ID)
        {
            if(ID == null)
            {
                return BadRequest("ID of Invoice is not specifed");
            }

           var order = context.orders.FirstOrDefault(a => a.OrderID == ID);
          if(order.UserID != User.GetUserId())
            {
                return NotFound("the Invoice doesn't exist please contact company");
            }
             
            StiReport report = new StiReport();
            report.Load("Reports/OrderInvoice.mrt");
            report["@OrderID"] = ID;
            report.Dictionary.Databases.Add(new StiSqlDatabase("Connection", configuration.GetConnectionString("DefaultConnection")));
            return StiNetCoreReportResponse.PrintAsPdf(report);
        }

        private long CalculateOrderAmount(Item[] items, decimal exchangeRate,decimal shippingFees)
        {
            decimal totalPrice = 0;
            foreach (var item in items)
            {
                totalPrice += (item.Price * item.Quantity)* exchangeRate + shippingFees;   
            }
            return Convert.ToInt64(totalPrice * 100);
        }
    }
}
