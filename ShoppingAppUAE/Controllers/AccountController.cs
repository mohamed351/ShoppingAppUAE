using Microsoft.AspNetCore.Mvc;
using ShoppingAppUAE.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingAppUAE.Extension;
using Microsoft.EntityFrameworkCore;
using ShoppingAppUAE.Models.AccountViewModels;

namespace ShoppingAppUAE.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext context;

        public AccountController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
          var userId =  User.GetUserId();
            var userData = context.Users.Include(a=> a.Counties).Include(a => a.Orders).FirstOrDefault(a=> a.Id == userId);
            var accountDetails = new AccountViewModel()
            {
                Orders = userData.Orders.ToList(),
                CureentUser = userData
               
               
                
            };



            return View(accountDetails);
        }
    }
}
