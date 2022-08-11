using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ShoppingAppUAE.Data;
using ShoppingAppUAE.Repository.UnitOfWork;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using Stimulsoft.Report.Mvc;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class OrdersController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ApplicationDbContext context;
        private readonly IConfiguration configuration;

        public OrdersController(IUnitOfWork unitOfWork, ApplicationDbContext context, IConfiguration configuration)
        {
            this.unitOfWork = unitOfWork;
            this.context = context;
            this.configuration = configuration;
            Stimulsoft.Base.StiLicense.LoadFromFile("license.key");
        }
        public IActionResult Index()
        {

            return View();
        }
        [HttpGet]
        public IActionResult GetOrders(int start = 0, int length = 10)
        {

            string search = HttpContext.Request.Query["search[value]"];
           var query =unitOfWork.Orders.GetDataTableOrders(start, length, a => a.FirstName.Contains(search)
           || a.FirstName.Contains(search) || a.OrderPhone.Contains(search) || a.OriginalPhone.Contains(search),
           a => a.DateTimeIssued);


            return Json(new
            {
                data = query.data.Select(a => new
                {
                    CountryCode = a.ApplicationUser.Counties.Code,
                    User = a.ApplicationUser.UserName,
                    City = a.City,
                    a.CurrencyCode,
                    a.DateTimeIssued,
                    a.Total,
                    a.OrderType,
                    a.OrderPhone,
                    UserPhone = a.ApplicationUser.PhoneNumber,
                    a.ExchangeRate,
                    shippingProvider = a.ShippingAndDelivery.ShippingCompany,
                    shippingFees = a.TotalShippingFees,
                    a.OrderID,
                    a.Approved,
                    a.InvoiceNumber,
                    a.Cancelled

                }),
                query.recordsFiltered,
                query.recordsTotal
            } );
        }
        [HttpGet]
        public IActionResult GetDetails(int? ID)
        {
            if(ID == null)
            {
                return BadRequest();
            }
            var order = unitOfWork.Orders.GetByID(ID.Value);
            if(order == null)
            {
                return NotFound();
            }
            return View(order);
        }
        [HttpPost]
        public IActionResult ApproveInvoice([FromBody]int? ID)
        {
            try
            {
                
                if (ID == null)
                {
                    return BadRequest();
                }
               

                var order = unitOfWork.Orders.GetByID(ID.Value);
                if(order.Approved || order.Cancelled)
                {
                    throw new Exception("Order has been Changed status please Refresh The Page");
                }
                DecreseQuantity(order.OrderDetails.ToList());
                order.Approved = true;
                context.SaveChanges();

                return Json(new { status = true , message="Order is Approved"  });
            }
            catch (Exception ex)
            {

                return Json(new { status = false, message = ex.Message });
            }
           
          
        }


        public void DecreseQuantity(List<OrderDetails> orderDetails)
        {
            foreach (var item in orderDetails)
            {

                var product = this.context.Products.FirstOrDefault(a => a.ProductID == item.ProductID);
                product.Quantity = product.Quantity - Convert.ToInt32(item.Quantity);
                if (product.Quantity < 0)
                {
                    throw new Exception("Sorry the Quantity is not enough");
                }
            }



        }
        [HttpGet()]
        public IActionResult InvoicePDF(int? ID)
        {
            if (ID == null)
            {
                return BadRequest("ID of Invoice is not specifed");
            }

            var order = context.orders.FirstOrDefault(a => a.OrderID == ID);
          

            StiReport report = new StiReport();
            report.Load("Reports/OrderInvoice.mrt");
            report["@OrderID"] = ID;
            report.Dictionary.Databases.Add(new StiSqlDatabase("Connection", configuration.GetConnectionString("DefaultConnection")));
            return StiNetCoreReportResponse.PrintAsPdf(report);
        }
        [HttpPost]
        public IActionResult CancelInvoice(int? ID)
        {
            if(ID == null)
            {
                return BadRequest("ID of Order is not Exist");
            }

            var order = context.orders.FirstOrDefault(a => a.OrderID == ID);
            if(order.OrderType == OrderType.OnDelivery)
            {
                order.Cancelled = true;
                context.SaveChanges();
            }
            else
            {
                var options = new RefundCreateOptions { PaymentIntent = "pi_Aabcxyz01aDfoo" };
                var service = new RefundService();
                service.Create(options);
            }

            return Json("DONE");
        }



    }
}
