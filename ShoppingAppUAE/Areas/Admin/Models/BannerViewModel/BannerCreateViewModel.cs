using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Areas.Admin.Models.BannerViewModel
{
    public class BannerCreateViewModel
    {
       [Required]

        public string Title { get; set; }
        [Required]
        public string Pragraph { get; set; }
       
        public string ImageURL { get; set; }
        [Required]
        public string ArabicTitle { get; set; }
        [Required]
        public string ArabicParagraph { get; set; }
    }
}
