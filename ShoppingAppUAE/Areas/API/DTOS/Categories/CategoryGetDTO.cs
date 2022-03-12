using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Areas.API.DTOS.Categories
{
    public class CategoryGetDTO
    {

        public int CategoryID { get; set; }

        public string  EnglishName { get; set; }

        public string ArabicName { get; set; }


        public string EnglishDescription { get; set; }


        public string ArabicDescription { get; set; }
    }
}
