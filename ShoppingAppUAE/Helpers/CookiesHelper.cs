using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Helpers
{
    public static class CookiesHelper
    {

        public static string GetCookieValue(this HttpRequest context,string cookielkey)
        {
            string value = "";
            context.Cookies.TryGetValue(cookielkey, out value);
            if (string.IsNullOrWhiteSpace(value))
                return null;
            else
                return value;
        }
    }
}
