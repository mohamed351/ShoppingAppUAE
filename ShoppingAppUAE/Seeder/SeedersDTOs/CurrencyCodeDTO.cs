using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Seeder.SeedersDTOs
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class CurrencyCodeDTO
    {
        public string AlphabeticCode { get; set; }
        public string Currency { get; set; }
        public string Entity { get; set; }
        public string MinorUnit { get; set; }
        public double? NumericCode { get; set; }
        public string WithdrawalDate { get; set; }
    }


}
