using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Data
{
    public class Brand
    {

        public int ID { get; set; }

        public string BrandNameEnglish { get; set; }


        public string BrandNameArabic { get; set; }


        public string ImageURL { get; set; }


        public string ImageAlternativeText { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
