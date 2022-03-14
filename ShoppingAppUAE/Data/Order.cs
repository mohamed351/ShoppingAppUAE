using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Data
{
    public class Order
    {

        public int OrderID { get; set; }

        public string UserID { get; set; }

        public int ProductID { get; set; }


        public DateTime DateTimeIssued { get; set; }


        public string ReferenceID { get; set; }

        public string CurrencyCode { get; set; }

        public decimal ExchangeRate { get; set; }
        public string TaxUUID { get; set; }

        public string TaxSubmissionID { get; set; }
        public decimal SalesTotal { get; set; }
        public decimal TotalDiscount { get; set; }

        public decimal TaxTotal { get; set; }
        public decimal TotalNet { get; set; }

        public decimal Total { get; set; }

        [ForeignKey(nameof(ShippingAndDelivery))]
        public int ShippingAndDeliveryShippingID { get; set; }
       
        public ShippingAndDelivery ShippingAndDelivery { get; set; }

    }

    public class ShippingAndDelivery
    {
        public int ShippingID { get; set; }

        public string ShippingCountry { get; set; }

        public string ShippingCompany { get; set; }

        public string ShippingCompanyPhone { get; set; }

        public string ShippingCompanyAddress { get; set; }


        public decimal ShippingPrice { get; set; }
        public bool IsDefault { get; set; }

      


    }

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
