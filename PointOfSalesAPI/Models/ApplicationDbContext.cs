using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSalesAPI.Models
{
    public class ApplicationPosDbContext:DbContext
    {

        public ApplicationPosDbContext(DbContextOptions<ApplicationPosDbContext> contextOptions)
            :base(contextOptions)
        {

        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<ProductBarCode> ProductBarCodes { get; set; }


        public DbSet<SupportItemBarCode> SupportItemBarCodes { get; set; }



    }
}
