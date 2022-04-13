
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using ShoppingAppUAE.Extension;

namespace ShoppingAppUAE.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        public CartController()
        {

        }
        public IActionResult Index()
        {
            var ip = HttpContext.GetRemoteIPAddress().ToString();



            return View();
        }
    }
}
