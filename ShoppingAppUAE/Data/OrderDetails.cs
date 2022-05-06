using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingAppUAE.Data
{
    public class OrderDetails
    {
        public int OrderDetailsID { get; set; }
        [ForeignKey(nameof(Order))]
        public int OrderID { get; set; }

        
        public Order Order { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductID { get; set; }


        public Product Product { get; set; }

        public decimal Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal ExchangeRate { get; set; }

        public string CurrencyCode { get; set; }

      

        public decimal Total { get; set; }


    }

  
}
