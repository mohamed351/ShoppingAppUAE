using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingAppUAE.Data
{
    public class Product
    {
        public Product()
        {
            this.ProductImages = new HashSet<ProductImage>();
        }

        public int ProductID { get; set; }

        [Required]
        public string ProductNameEnglish { get; set; }

        [Required]
        public string ProductNameArabic { get; set; }

        [Required]
        public decimal SellingPrice { get; set; }
        [Required]
        public decimal PurchasingPrice { get; set; }

        [Required]
        public decimal Discount { get; set; }


        [Required]
        public string  ImageURL { get; set; }

        public string ImageAlternativeText { get; set; }



        public string SEOEnglishTitle { get; set; }

        public string SEOArabicTitle { get; set; }


        public string SEOEnglishKeyWords { get; set; }

        public string SEOArabicKeyWords { get; set; }

        public int Quantity { get; set; }

        public ICollection<ProductImage> ProductImages { get; set; }




        public string DescriptionEnglish { get; set; }


        public string DescriptionArabic { get; set; }


        public bool IsActive { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryID { get; set; }

       [ForeignKey(nameof(Brand))]
        public int? BrandID { get; set; }


        public Brand Brand { get; set; }


        public Category Category { get; set; }


    }
}
