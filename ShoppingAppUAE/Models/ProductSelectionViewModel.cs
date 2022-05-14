using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Models
{
    public class ProductSelectionViewModel
    {

        public int pageSize { get; set; }
        public int pageNumber { get; set; }
        public int? CategoryID { get; set; }
        //public decimal MaxPrice { get; set; }
        //public decimal MinPrice { get; set; }
        public int? BrandID { get; set; }
        public string Search { get; set; }

        public decimal? MaxValue { get; set; }
        public decimal? MinValue { get; set; }
    }
}
