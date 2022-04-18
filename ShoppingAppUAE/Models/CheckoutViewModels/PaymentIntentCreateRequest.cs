using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Models.CheckoutViewModels
{
    public class Item
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class PaymentIntentCreateRequest
    {
        [JsonProperty("items")]
        public Item[] Items { get; set; }
    }
}
