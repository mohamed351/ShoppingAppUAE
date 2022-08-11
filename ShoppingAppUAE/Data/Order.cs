using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Data
{
    public class Order
    {
        public Order()
        {
            this.OrderDetails = new HashSet<OrderDetails>();
        }
        public int OrderID { get; set; }
        [ForeignKey(nameof(ApplicationUser))]
        public string UserID { get; set; }
      
        public ApplicationUser ApplicationUser { get; set; }
        public string OrderGUID { get; set; }

        public int InvoiceNumber { get; set; }

        public DateTime DateTimeIssued { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Note { get; set; }

        public string Address { get; set; }

        public string City { get; set; }
        public string ReferenceID { get; set; }

        public string CurrencyCode { get; set; }

        public decimal ExchangeRate { get; set; }

        public decimal TotalShippingFees { get; set; }

        public string OriginalPhone { get; set; }
      


        public string OrderPhone { get; set; }

        public string OriginalEmail { get; set; }

        public string OrderEmail { get; set; }


        public string OrderAddress { get; set; }

        public bool Approved { get; set; }

        public bool Cancelled { get; set; }

        public string StripID { get; set; }





        public decimal Total { get; set; }

        [ForeignKey(nameof(ShippingAndDelivery))]
        public int ShippingAndDeliveryShippingID { get; set; }
       
        public ShippingAndDelivery ShippingAndDelivery { get; set; }


        public ICollection<OrderDetails> OrderDetails { get; set; }

        public OrderType OrderType { get; set; }

    }

    public enum OrderType
    {
        OnDelivery =0,
        Card = 1
    }

  
}
