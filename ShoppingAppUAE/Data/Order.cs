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
        [ForeignKey(nameof(Product))]
        public int ProductID { get; set; }

        public Product Product { get; set; }


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

  
}
