using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingAppUAE.Data
{
    public class Review
    {
     
        public int ProductID { get; set; }
        [ForeignKey("ProductID")]
        public Product Product { get; set; }

        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public ApplicationUser ApplicationUser { get; set; }


        public string Comment { get; set; }

        public int FeedBackNumber { get; set; }
    }
}
