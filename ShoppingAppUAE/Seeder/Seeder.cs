using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ShoppingAppUAE.Data;
using ShoppingAppUAE.Seeder.SeedersDTOs;
using System;
using System.Collections.Generic;
using System.IO;
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
                var dbContext = serviceScoped.ServiceProvider.GetService<ApplicationDbContext>();
               await BuildRoles(roleManager);
                await BuildCountries(dbContext);

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


        private static async Task BuildCountries(ApplicationDbContext context)
        {
            if (!context.Counties.Any())
            {
                string jsonCountires = File.ReadAllText("Seeder/countyCodes.json");
               var list = JsonConvert.DeserializeObject<List<ContryCodesDTOS>>(jsonCountires);
                List<Counties> counties = new List<Counties>();
                foreach (var item in list)
                {
                    counties.Add(new Counties()
                    {
                        Code = item.CountryCode,
                        ArabicName = item.Name,
                        EnglishName = item.Name,
                        Prefix = item.Iso
                    });
                }
                context.Counties.AddRange(counties);
              await  context.SaveChangesAsync();

            }

           
        }
    }
}
