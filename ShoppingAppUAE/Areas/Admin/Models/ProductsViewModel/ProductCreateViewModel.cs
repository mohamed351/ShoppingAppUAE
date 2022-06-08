using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Areas.Admin.Models.ProductsViewModel
{
    public class ProductCreateViewModel
    {
        public ProductCreateViewModel()
        {
           
        }


        [Required]
        public string ProductNameEnglish { get; set; }

        [Required]
        public string ProductNameArabic { get; set; }

        [Required]
        public decimal SellingPrice { get; set; }
        [Required]
        public decimal PurchasingPrice { get; set; }

        [Required]
        public decimal Discount { get; set; }


        public string ImageURL { get; set; }

        public string ImageAlternativeText { get; set; }



        public string SEOEnglishTitle { get; set; }

        public string SEOArabicTitle { get; set; }


        public string SEOEnglishKeyWords { get; set; }

        public string SEOArabicKeyWords { get; set; }

        public int Quantity { get; set; }


        public string DescriptionEnglish { get; set; }


        public string DescriptionArabic { get; set; }
        [Required]
        public decimal Weight { get; set; }



        public int CategoryID { get; set; }

        public int? BrandID { get; set; }

        public bool IsArravel { get; set; }

        public bool IsTop10 { get; set; }


        public string BreifDescriptionArabic { get; set; }

        public string BreifDescriptionEnglish { get; set; }

        public string VideoIFrameURL { get; set; }


      

       
    }

   
}
