using Microsoft.AspNetCore.Mvc;
using ShoppingAppUAE.Data;
using ShoppingAppUAE.Repository.UnitOfWork;
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

        public OrdersController(IUnitOfWork unitOfWork, ApplicationDbContext context)
        {
            this.unitOfWork = unitOfWork;
            this.context = context;
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
                DecreseQuantity(order.OrderDetails.ToList());

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


    }
}
