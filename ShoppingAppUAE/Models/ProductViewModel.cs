using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Models
{
    public class ProductViewModel
    {

        public List<CategoryViewModel> CategoryViewModels { get; set; }
    }
    public class CategoryViewModel
    {
        public int ID { get; set; }

        public string NameArabic { get; set; }
        public string NameEnglish { get; set; }
    }


}
