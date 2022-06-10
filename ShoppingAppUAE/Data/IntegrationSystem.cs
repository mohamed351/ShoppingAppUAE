using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Data
{
    public class IntegrationSystem
    {

        public int ID { get; set; }


        [Required]
        public string ClientID { get; set; }

        [Required]

        public string ClientSecret { get; set; }
    }
}
