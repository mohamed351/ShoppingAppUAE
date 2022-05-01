using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Data
{
    public class Currency
    {
        [Key]
        public string CurrencyCode { get; set; }

        public string ArabicName { get; set; }

        public string EnglishName { get; set; }
        public decimal ExchangeRate { get; set; }

        public bool IsActive { get; set; }

        public bool IsDefault { get; set; }



    }
}
