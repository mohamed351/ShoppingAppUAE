using ShoppingAppUAE.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Services.ExchangeRate
{
   public interface IExchangeRateService
    {

        public List<Currency> ActiveCurrencies { get;  }



    }
}
