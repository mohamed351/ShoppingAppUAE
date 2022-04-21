using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Controllers
{
    public class AboutUsController : Controller
    {
        private readonly ILogger<AboutUsController> logger;

        public AboutUsController(ILogger<AboutUsController> logger)
        {
            this.logger = logger;
        }
        public IActionResult Index()
        {
          
          
            return View();
        }
    }
}
