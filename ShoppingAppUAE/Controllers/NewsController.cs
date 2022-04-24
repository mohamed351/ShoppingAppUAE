using Microsoft.AspNetCore.Mvc;
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
            return View(context.Blogs.ToList());
        }
    }
}
