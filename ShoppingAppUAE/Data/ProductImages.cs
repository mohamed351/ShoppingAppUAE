using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingAppUAE.Data
{
    public class ProductImage
    {
        [Key]
        public int ProductImageID { get; set; }

        public string ImagesURL { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductID { get; set; }


        public Product Product { get; set; }
    }
}
