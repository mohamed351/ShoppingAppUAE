using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingAppUAE.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Controllers
{
    public class NewsController : Controller
    {
        private readonly ApplicationDbContext context;

        public NewsController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            
            return View(context.Blogs.Include(a => a.BlogComments).ToList());
        }
        [HttpGet]
        public IActionResult Blog(int? ID)
        {
            if(ID == null)
            {
                return BadRequest("The ID is not found");
            }
            var blog = context.Blogs.Find(ID);
            if(blog == null)
            {
                return NotFound();
            }
            return View();

        }
    }
}
