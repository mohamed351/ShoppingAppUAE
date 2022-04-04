using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoppingAppUAE.Data;
using ShoppingAppUAE.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet()]
        public IActionResult GetUsers(int start = 0, int length = 10)
        {
            string search = HttpContext.Request.Query["search[value]"];
            search = search.Trim();

            return Json(GetUser(start,length,u=> u.Email.Contains(search) || u.PhoneNumber.Contains(search) || u.FirstName.Contains(search) || u.LastName.Contains(search),a=> a.Id));
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ApplicationUser user)
        {
            return View();
        }
        public DataTableViewModel<ApplicationUser> GetUser(int start, int lenght, Expression<Func<ApplicationUser, bool>> search, Expression<Func<ApplicationUser, string>> OrderBy)
        {
            int count = userManager.Users.Where(search).Count();
            return new DataTableViewModel<ApplicationUser>(){
               data = userManager.Users.Where(search).OrderBy(OrderBy).Skip(start).Take(lenght).ToList(),
               recordsFiltered = count,
               recordsTotal = count
            };
        }


    }
}
