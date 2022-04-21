using Microsoft.AspNetCore.Mvc;
using ShoppingAppUAE.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Controllers
{
    public class ContactController : Controller
    {
        private readonly ApplicationDbContext context;

        public ContactController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var contact = context.ContactInfos.FirstOrDefault();
            if(contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitContact([FromForm]ContactMessage contactMessage)
        {
            context.ContactMessages.Add(contactMessage);
            context.SaveChanges();

            return View();
        }
    }
}
