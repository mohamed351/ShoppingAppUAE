using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingAppUAE.Areas.API.DTOS.Categories;
using ShoppingAppUAE.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Areas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CategoriesController(IUnitOfWork unitOfWork , IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetCategories()
        {
         var mapping =   mapper.Map<List<CategoryGetDTO>>(unitOfWork.Categories.GetAll());
            return Ok(mapping);
        }

      
    }
}
