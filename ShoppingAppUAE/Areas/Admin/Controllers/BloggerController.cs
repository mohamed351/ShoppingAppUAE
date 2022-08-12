using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ShoppingAppUAE.Areas.Admin.Models.BloggerViewModel;
using ShoppingAppUAE.Data;
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

        public BloggerController(ApplicationDbContext context , IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
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
