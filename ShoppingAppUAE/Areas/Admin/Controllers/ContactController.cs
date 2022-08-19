using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingAppUAE.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "Admin,Admin2")]
    public class ContactController : Controller
    {
        private readonly ApplicationDbContext context;

        public ContactController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Create()
        {
            var contact = context.ContactInfos.FirstOrDefault();
            return View(contact);
        }
        [HttpPost]
        public IActionResult Create([FromForm] ContactInfo contact)
        {
            if(contact.ID == 0)
            {
                context.ContactInfos.Add(contact);
                context.SaveChanges();
            }
            else
            {
                context.Entry(contact).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
            return View(contact);
            
        }

       
    }
}
