using PointOfSalesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSalesAPI.DTO
{
    public class ProductSerialDTO
    {

        public ProductSerialDTO()
        {
            this.subSerial = new List<SubSerial>();
        }
        public int ProductId { get; set; }
        public string Serial { get; set; }

        public SerialStatus SerialStatus { get; set; }



        public List<SubSerial> subSerial { get; set; }
    }

    public class SubSerial
    {
        public string MainSerial { get; set; }

        public string Serial { get; set; }
    }

  
}
