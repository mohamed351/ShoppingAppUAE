using System.Collections.Generic;

namespace ShoppingAppUAE.Data
{
    public class Blog
    {
        public Blog()
        {
            this.BlogComments = new HashSet<BlogComment>();
        }

        public int ID { get; set; }
        public string Title { get; set; }

        public string ImageURL { get; set; }

        public string ContentEnglish { get; set; }

        public string ContentArabic { get; set; }

        public string KeyWordsEnglish { get; set; }

        public string KeyWordArabic { get; set; }

       public  ICollection<BlogComment> BlogComments { get; set; }
    }

}
