using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Areas.Admin.Models.BloggerViewModel
{
    public class BlogCreateViewModel
    {

        public string EnglishTitle { get; set; }

        public string ArabicTitle { get; set; }

        public string ImageURL { get; set; }

        public string EnglishContent { get; set; }

        public string ArabicContent { get; set; }
    }

    public class BlogEditViewModel : BlogCreateViewModel
    {
        public int ID { get; set; }

    }
}
