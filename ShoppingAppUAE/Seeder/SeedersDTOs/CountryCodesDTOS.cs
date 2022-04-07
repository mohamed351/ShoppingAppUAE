using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Seeder.SeedersDTOs
{
    public  class ContryCodesDTOS
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("Iso")]
        public string Iso { get; set; }

        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }
    }
}
