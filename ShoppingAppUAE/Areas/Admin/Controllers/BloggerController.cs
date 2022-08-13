using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ShoppingAppUAE.Areas.Admin.Models.BloggerViewModel;
using ShoppingAppUAE.Data;
using ShoppingAppUAE.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BloggerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IUnitOfWork work;

        public BloggerController(ApplicationDbContext context , IWebHostEnvironment webHostEnvironment , IUnitOfWork work)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
            this.work = work;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetData(int start = 0, int length = 10)
        {
            string search = HttpContext.Request.Query["search[value]"];
           var dataTableQuery = work.Blogger.GetDataTable(start, length, a => a.ArabicTitle.Contains(search) || a.ContentEnglish.Contains(search), a => a.ID);
            return Json(dataTableQuery);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int  ID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var blogger = work.Blogger.GetByID(ID);
            if(blogger == null)
            {
                return NotFound();
            }
            return View(new BlogEditViewModel()
            {
                ArabicContent = blogger.ContentArabic,
                ArabicTitle = blogger.ArabicTitle,
                EnglishContent = blogger.ContentEnglish,
                EnglishTitle = blogger.Title,
                ImageURL = blogger.ImageURL,
                ID = blogger.ID
            });

        }

        public IActionResult Create(BlogCreateViewModel blog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(new Blog()
                {
                    ArabicTitle = blog.ArabicTitle,
                    ContentArabic = blog.ArabicContent,
                    ContentEnglish = blog.EnglishContent,
                    Title = blog.EnglishTitle,
                    ImageURL = Helpers.ImageHelpers.ConvertMainImage(blog.ImageURL, webHostEnvironment),

                });
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(blog);
        }
    }
}
