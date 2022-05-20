using ShoppingAppUAE.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Models.AccountViewModels
{
    public class AccountViewModel
    {
        

        public List<Order> Orders { get; set; }

        public ApplicationUser CureentUser { get; set; }
    }
}
