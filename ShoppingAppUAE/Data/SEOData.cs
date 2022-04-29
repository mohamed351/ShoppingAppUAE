using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Data
{
    public class SEOData
    {
        [Required]
        [MaxLength(25)]
       
        public string ActionName { get; set; }
        [Required]
        [MaxLength(25)]
        public string ControllerName { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string AnotherTags { get; set; }


    }
}
