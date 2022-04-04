using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ShoppingAppUAE.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Seeder
{
    public static class Seeder
    {

        public static async Task PrePopulation(IApplicationBuilder builder)
        {
            using (var serviceScoped = builder.ApplicationServices.CreateScope())
            {

                var roleManager = serviceScoped.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                
               await BuildRoles(roleManager);


            }
        }

        private static async Task BuildRoles(RoleManager<IdentityRole> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync("Admin");
            if (adminRole == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));

            }
            var userRole = await roleManager.FindByIdAsync("User");
            if(userRole == null)
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
            }
            
        }

    }
}
