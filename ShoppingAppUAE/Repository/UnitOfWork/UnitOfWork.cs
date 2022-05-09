using ShoppingAppUAE.Data;
using ShoppingAppUAE.Repository.implementation;
using ShoppingAppUAE.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            this.Categories = new CategoryRepository(context);
            this.Products = new ProductRepository(context);
            this.Brands = new BrandRepository(context);
            this.Orders = new OrderRepository(context);
        }
        public ICategoryRepository Categories { get; }

        public IProductRepository Products{ get; }

        public IBrandRepository Brands { get; }

        public IOrderRepository Orders { get; }

        public int Complete()
        {
            return this.context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
