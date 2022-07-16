using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PointOfSalesAPI.DTO;
using PointOfSalesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSalesAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductSerialsController:ControllerBase
    {
        private readonly ApplicationPosDbContext posDbContext;

        public ProductSerialsController(ApplicationPosDbContext posDbContext)
        {
            this.posDbContext = posDbContext;
        }


        [HttpGet("{id?}",Name = "GetSerails")]
        public IActionResult GetSerails(int? id)
        {
          var productSerials =  this.posDbContext.ProductBarCodes.Include(a => a.SupportItemBarCodes)
                .Where(a => a.ProductID == id).Select(a=> new ProductSerialDTO() { ProductId = a.ProductID ,
                    Serial =a.MainBarCode, 
                    SerialStatus = a.SerialStatus,
                    subSerial =
                    a.SupportItemBarCodes.ToList().Select(a=> new SubSerial() {  MainSerial =a.SupportItem.MainBarCode , Serial = a.BarCode}).ToList()}).ToList();

            return Ok(productSerials);
        }
        [HttpPost]
        public IActionResult PostSerials([FromBody] List<ProductSerialDTO> dTO)
        {
            foreach (var item in dTO)
            {
                ProductBarCode code = new ProductBarCode()
                {
                    IsDeleted = false,
                    MainBarCode = item.Serial,
                    ProductID = item.ProductId,
                    SerialStatus = SerialStatus.New

                };
                code.SupportItemBarCodes = new List<SupportItemBarCode>();
                foreach (var item2 in item.subSerial)
                {
                    code.SupportItemBarCodes.Add(new SupportItemBarCode()
                    {
                        BarCode = item2.Serial,
                        IsDeleted = false,


                    });
                }
                posDbContext.ProductBarCodes.Add(code);
            }
       
         
            posDbContext.SaveChanges();
            return Ok();


        }
       
    }
}
