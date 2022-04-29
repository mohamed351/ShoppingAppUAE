using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using ShoppingAppUAE.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Helpers
{
    public static class SEOHelper
    {
        public static URLInfo GetURLData(this ActionContext context , ApplicationDbContext dbContext , IConfiguration configuration)
        {
          var routerData =  context.RouteData;
            string currentController = routerData.Values["controller"].ToString();
            string currentAction = routerData.Values["action"].ToString();
            var SEOData = dbContext.SEODatas.Find(currentAction, currentController);

            return new URLInfo()
            {
                ActionName = currentAction,
                ControllerName = currentController,
                Description = SEOData == null ? configuration["SEO:DefaultDescription"] :SEOData.Description,
                Tags = SEOData == null ? configuration["SEO:DefaultTags"]:SEOData.AnotherTags,
                Title = SEOData == null ? configuration["SEO:DefaultTitle"]:SEOData.Title

            };
        }

        
    }

    public class URLInfo
    {
        public string ActionName { get; set; }
        public string ControllerName { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Tags { get; set; }




    }
}
