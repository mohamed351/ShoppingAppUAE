using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingAppUAE.Areas.API.DTOS.Products;
using ShoppingAppUAE.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Areas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ProductsController(IUnitOfWork unitOfWork , IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            this.httpContextAccessor = httpContextAccessor;
        }
        [HttpGet]
        public IActionResult GetProducts(int start=0, int length=10, string search ="")
        {
           

            ProductsGetDTO productsGetDTO = new ProductsGetDTO();
            string host = httpContextAccessor.HttpContext.Request.Host.Value;
            string schema = httpContextAccessor.HttpContext.Request.Scheme;
            var dataTableData =  unitOfWork
                .Products
                .GetDataTable(start, length, a => a.ProductNameArabic.Contains(search) || a.ProductNameEnglish.Contains(search), a => a.ProductID);
           
            productsGetDTO.Data = dataTableData.data.Select(a => new ProductDataDTO()
            {
                 DescriptionArabic = a.DescriptionArabic,
                 DescriptionEnglish = a.DescriptionEnglish,
                 ProductID = a.ProductID,
                 ProductNameArabic = a.ProductNameArabic,
                 ProductNameEnglish = a.ProductNameEnglish,
                 CateogryID = a.CategoryID,
                 ImageURL = schema+ "://" + host + "/api/Image/"+a.ImageURL
            }).ToList();
            productsGetDTO.MetaData = new MetaDataDTO()
            {
                recordsFiltered = dataTableData.recordsFiltered,
                recordsTotal = dataTableData.recordsTotal
            };
            return Ok(productsGetDTO);
        }

      

        [HttpGet("{CategoryID}/Category")]
        public IActionResult GetProductsByCategoryID(int CategoryID,int start = 0, int length = 10, string search = "" )
        {


            ProductsGetDTO productsGetDTO = new ProductsGetDTO();

            var dataTableData = unitOfWork
                 .Products
                 .GetDataTable(start, length, a => a.CategoryID == CategoryID &&  (a.ProductNameArabic.Contains(search) || a.ProductNameEnglish.Contains(search)), a => a.ProductID );

            productsGetDTO.Data = dataTableData.data.Select(a => new ProductDataDTO()
            {
                DescriptionArabic = a.DescriptionArabic,
                DescriptionEnglish = a.DescriptionEnglish,
                ProductID = a.ProductID,
                ProductNameArabic = a.ProductNameArabic,
                ProductNameEnglish = a.ProductNameEnglish,
                CateogryID = a.CategoryID,
                ImageURL = a.ImageURL
            }).ToList();
            productsGetDTO.MetaData = new MetaDataDTO()
            {
                recordsFiltered = dataTableData.recordsFiltered,
                recordsTotal = dataTableData.recordsTotal
            };
            return Ok(productsGetDTO);
        }
    }
}
