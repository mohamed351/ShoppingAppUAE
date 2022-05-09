using Microsoft.AspNetCore.Mvc;
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

        public OrdersController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {

            return View();
        }
        [HttpGet]
        public IActionResult GetOrders(int start = 0, int length = 10)
        {

            string search = HttpContext.Request.Query["search[value]"];
            return Json(unitOfWork.Orders.GetDataTable(start, length, a => a.FirstName.Contains(search) || a.FirstName.Contains(search), a => a.OrderID));
        }


    }
}
