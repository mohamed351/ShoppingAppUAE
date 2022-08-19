using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingAppUAE.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Areas.Admin.Controllers
{

  

    [Area("Admin")]
    [Authorize(Roles = "Admin,Admin2")]
    public class ContactMessagesController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public ContactMessagesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetContacts(int start = 0, int length = 10)
        {

            string search = HttpContext.Request.Query["search[value]"];
            return Json(unitOfWork.ContactMessages.GetDataTable(start, length, a => a.PhoneNumber.Contains(search) || a.Name.Contains(search), a => a.ID));
        }
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if(id == null)
            {
                return BadRequest("The ID is not Exist");
            }

           var contact = unitOfWork.ContactMessages.GetByID(id.Value);
            if(contact == null)
            {
                return NotFound("The Contact Message is not Exist");
            }

            return View(contact);
        }



    }
}
