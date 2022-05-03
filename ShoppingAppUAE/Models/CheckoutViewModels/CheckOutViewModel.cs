using ShoppingAppUAE.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Models.CheckoutViewModels
{
    public class CheckOutViewModel
    {

        public CheckOutViewModel()
        {
            cartDetails = new List<CartDetails>();
            ShippingDetails = new List<ShippingAndDelivery>();
        }

        public ApplicationUser ApplicationUser { get; set; }


        public string CountryCode { get; set; }

        public List<CartDetails> cartDetails { get; set; }

        public List<ShippingAndDelivery> ShippingDetails { get; set; }

        public string Note { get; set; }

        public CheckoutType CheckoutType { get; set; }


        public string CardFirstName { get; set; }

        public string CardLastName { get; set; }
    }
    public enum CheckoutType
    {
        CashOnDelivery = 0 ,
        Card =1
    }
    public class CartDetails
    {
        public int ProductId { get; set; }

        public string ProductNameEnglish { get; set; }

        public string ProductNameArabic { get; set; }

        public decimal Price { get; set; }

        public string CurrencyCode { get; set; }



    }

    public class ShippingDetails
    {
        public int ShippingID { get; set; }

        public decimal CompanyName { get; set; }

        public decimal TotalCost { get; set; }



    }
}
