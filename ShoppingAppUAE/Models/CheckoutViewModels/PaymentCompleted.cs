using ShoppingAppUAE.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Models.CheckoutViewModels
{
    public class PaymentCompletedViewModel
    {


        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string OrderNote { get; set; }

        public string CurrenyCode { get; set; }

        public int ShipmentID { get; set; }

        public decimal ShipmentFees { get; set; }


        public string PhoneNumber { get; set; }



        public string Email { get; set; }

        public string PhonePrefix { get; set; }

      

        public OrderType OrderType { get; set; }
        public List<PaymentCart> PaymentCarts { get; set; }
    }

    public class PaymentCart
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
    public class ShipmentData
    {
        public int ShipmentID { get; set; }
        public decimal ShipmentTotal { get; set; }
    }


}
