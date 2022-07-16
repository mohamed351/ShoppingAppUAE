using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Data
{
    public class WishList
    {

        

       

        public string UserID { get; set; }

        public int ProductID { get; set; }
     
        [ForeignKey("ProductID")]
        public Product Product { get; set; }
        [ForeignKey("UserID")]
        public ApplicationUser MyProperty { get; set; }
    }
}
