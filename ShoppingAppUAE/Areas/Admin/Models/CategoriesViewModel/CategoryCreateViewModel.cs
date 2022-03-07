using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Areas.Admin.Models.CategoriesViewModel
{
    public class CategoryCreateViewModel
    {

     
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
    }
}
