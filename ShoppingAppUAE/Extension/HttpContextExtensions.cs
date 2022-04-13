using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Extension
{
    public static class HttpContextExtensions
    {

        public static string GetRemoteIPAddress(this HttpContext context, bool allowForwarded = true)
        {
            if (!string.IsNullOrEmpty(context.Request.Headers["X-Forwarded-For"]))
            {
                return context.Request.Headers["X-Forwarded-For"];
            }
            return context.Connection?.RemoteIpAddress?.ToString();
        }
    }
}
