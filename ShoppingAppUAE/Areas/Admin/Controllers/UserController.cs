using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoppingAppUAE.Areas.Admin.Models.UserViewModel;
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
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ApplicationDbContext context;

        public UserController(UserManager<ApplicationUser> userManager ,
            RoleManager<IdentityRole> roleManager , ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.context = context;
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
            ViewBag.Roles = new SelectList(roleManager.Roles, "Name", "Name");
            ViewBag.Countries = new SelectList(context.Counties, "Code", "EnglishName");
            return View();
        }
        [HttpPost]
        public async  Task< IActionResult> Create(UserCreateViewModel user)
        {
            if (ModelState.IsValid)
            {
                var selectCounty = context.Counties.Find(user.CountryCode);
                var currentUser = new ApplicationUser()
                {
                    Email = user.Email,
                    Address = user.Address,
                    City = user.City,
                    CountryCode = user.CountryCode,
                    LastName = user.LastName,
                    FirstName = user.FirstName,
                    PhoneNumber = user.Phone,
                    UserName = user.UserName,
                    PhonePrefix = selectCounty.Prefix
                };
              var result = await  userManager.CreateAsync(currentUser,user.Password);

                if (result.Succeeded)
                {
                   await userManager.AddToRoleAsync(currentUser, user.RoleName);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // append Error List
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(item.Code, item.Description);
                    }

                    ViewBag.Roles = new SelectList(roleManager.Roles, "Name", "Name");
                    ViewBag.Countries = new SelectList(context.Counties, "Code", "EnglishName");
                    return View(user);
                }

                
            }
            ViewBag.Roles = new SelectList(roleManager.Roles, "Name", "Name");
            ViewBag.Countries = new SelectList(context.Counties, "Code", "EnglishName");
            return View(user);
        }
        [HttpGet]
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return BadRequest("The ID is not exist");
            }

            var applicationUser = this.context.Users.Include(a => a.Counties).FirstOrDefault(a => a.Id == id);
            if (applicationUser == null)
            {
                return NotFound("The Product is not found");
            }

            return View(applicationUser);


        }
        [HttpDelete]
        public IActionResult DeleteUser(string id)
        {
            if(id == null)
            {
                return BadRequest("The ID is not Specified");
            }

            var userId =  userManager.FindByIdAsync(id);
            if(userId == null)
            {
                return NotFound("The ");
            }
            return NoContent();
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
