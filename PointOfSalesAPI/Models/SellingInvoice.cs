using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSalesAPI.Models
{
    public class SellingInvoice
    {


        public int ID { get; set; }

        public int InvoiceNumber { get; set; }

        [ForeignKey(nameof(Customer))]
        public int CustomerID { get; set; }
  
        public Customer Customer { get; set; }

        public string UserId { get; set; }

        public string Note { get; set; }


        public decimal Total { get; set; }

        public string CurrencyCode { get; set; }

        public InvoiceType InvoiceType { get; set; }

    }

    public enum InvoiceType
    {
        Offline = 0,
        Online = 1

    }
    public class SellingInvoiceDetails
    {

        public int ID { get; set; }

        public int ProductID { get; set; }

        public decimal Discount { get; set; }

        public decimal Price { get; set; }

        public int  Serial { get; set; }
        public ProductBarCode ProductBarCode { get; set; }

    }
       
}
