using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingAppUAE.Data
{
    public class OrderDetails
    {
        public int OrderDetailsID { get; set; }

        public int OrderID { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductID { get; set; }


        public Product Product { get; set; }
        public decimal AmountSold { get; set; }

        public decimal AmountCurrency { get; set; }

        public decimal DiscountBeforeTax { get; set; }

        public decimal DiscountAfterTax { get; set; }


        public decimal Quantity { get; set; }

        public decimal SalesTotal { get; set; }

        public decimal NetTotal { get; set; }

        public decimal TaxRate { get; set; }

        public string TaxCode { get; set; }

        public decimal Total { get; set; }


    }

  
}
