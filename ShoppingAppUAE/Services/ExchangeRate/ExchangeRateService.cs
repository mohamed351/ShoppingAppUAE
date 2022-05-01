using Microsoft.AspNetCore.Builder;
using ShoppingAppUAE.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Services.ExchangeRate
{
    public class ExchangeRateService : IExchangeRateService
    {

        public ExchangeRateService(ApplicationDbContext context)
        {
            this.ActiveCurrencies = context.Currencies.Where(a => a.IsActive == true).OrderByDescending(a => a.IsDefault).ToList();
        }
        public List<Currency> ActiveCurrencies { get; }
    }
}
