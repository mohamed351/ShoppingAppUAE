using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingAppUAE.Data
{
    public class BlogComment
    {
        public int ID { get; set; }
        [ForeignKey(nameof(User))]
        public string UserID { get; set; }
 
        public ApplicationUser User { get; set; }

        public string Comment { get; set; }
    }

}
