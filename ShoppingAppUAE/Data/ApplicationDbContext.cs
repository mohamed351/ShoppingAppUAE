using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;

namespace ShoppingAppUAE.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductImage> ProductImages { get; set; }


        public DbSet<Brand> Brands { get; set; }


        public DbSet<Banner> Banners { get; set; }

        public DbSet<Counties> Counties { get; set; }

        public DbSet<ContactInfo> ContactInfos { get; set; }

        public DbSet<ContactMessage> ContactMessages { get; set; }

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<BlogComment> BlogComment { get; set; }

        public DbSet<ShippingAndDelivery> shippingAndDeliveries { get; set; }

        public DbSet<Currency> Currencies { get; set; }


        public DbSet<Order> orders { get; set; }

        public DbSet<OrderDetails> OrderDetails { get; set; }


        public DbSet<SEOData> SEODatas { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<SEOData>().HasKey(a => new { a.ActionName, a.ControllerName });
            base.OnModelCreating(builder);  
        }



    }

}
