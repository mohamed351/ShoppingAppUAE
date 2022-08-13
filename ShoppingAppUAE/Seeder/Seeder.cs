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
                await BuildCurrnies(dbContext);
                await BuildCountries(dbContext);

            }
        }

        private static async Task BuildRoles(RoleManager<IdentityRole> roleManager)
        {
            await CreateRoleName(roleManager, "Admin");
            await CreateRoleName(roleManager, "Admin2");
            await CreateRoleName(roleManager, "Sales");
            await CreateRoleName(roleManager, "User");





        }

        private static async Task CreateRoleName(RoleManager<IdentityRole> roleManager , string name)
        {
            var adminRole = await roleManager.FindByNameAsync(name);
            if (adminRole == null)
            {
                await roleManager.CreateAsync(new IdentityRole(name));

            }
        }

        private static async Task BuildCountries(ApplicationDbContext context)
        {
            try
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
                    await context.SaveChangesAsync();

                }
            }
            catch
            {

            }


        }


        private static async Task BuildCurrnies(ApplicationDbContext context)
        {

           
                if (!context.Currencies.Any())
                {
                    string jsonCountires = File.ReadAllText("Seeder/currncyCode.json");
                var list = JsonConvert.DeserializeObject<List<CurrencyCodeDTO>>(jsonCountires).Where(a=> a.AlphabeticCode != null).Select(a=> new { a.Currency , a.AlphabeticCode}).Distinct().ToList();
                   
                    foreach (var item in list)
                    {
                    context.Currencies.Add(new Currency()
                        {
                            ArabicName = item.Currency,
                            EnglishName = item.Currency,
                            CurrencyCode = item.AlphabeticCode,
                            ExchangeRate = 0,
                            IsActive = false
                        });
              
                    await context.SaveChangesAsync();
                }
            
               

                }

           
        }
    }
}
