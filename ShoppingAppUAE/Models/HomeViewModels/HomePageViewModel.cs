using ShoppingAppUAE.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Models.HomeViewModels
{
    public class HomePageViewModel
    {

        public List<BannerViewModel> Banner { get; set; }

        public List<Category> Categories { get; set; }


        public List<Product> Products { get; set; }
    }
    public class ProductHomeViewModel
    {
        public int ID { get; set; }
        public string NameArabic { get; set; }


        public string NameEnglish { get; set; }

        public string ImageURL { get; set; }

       
    }

  
    public class BannerViewModel
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string ImageURL { get; set; }

        public string Paragraph { get; set; }

        public string ArabicTitle { get; set; }


        public string ArabicParagrap { get; set; }

    }
}
