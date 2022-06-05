using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ShoppingAppUAE.Data;
using System.Collections.Generic;
namespace ShoppingAppUAE.Data
{
    public class Category
    {

        public Category()
        {
            this.Products = new HashSet<Product>();
        }

        public int CategoryID { get; set; }
        [Required]
        public string CategoryNameArabic { get; set; }
        [Required]
        public string CategoryNameEnglish { get; set; }
        [Required]

        public string CategoryDescriptionArabic { get; set; }
        [Required]
        public string CategoryDescriptionEnglish { get; set; }

      

        public string SEOEnglishTitle { get; set; }

        public string SEOArabicTitle { get; set; }


        public string SEOEnglishKeyWords { get; set; }

        public string SEOArabicKeyWords { get; set; }


        public bool IsActive { get; set; }


        public string ImageURL { get; set; }


        public string ImageAlternativeText { get; set; }

        public bool IsShownInHome { get; set; }


        public ICollection<Product>  Products { get; set; }


    }
}
