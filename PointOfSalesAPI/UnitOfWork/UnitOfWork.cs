using PointOfSalesAPI.Models;
using PointOfSalesAPI.Repository.implementations;
using PointOfSalesAPI.Repository.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSalesAPI.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationPosDbContext context;

        public UnitOfWork(ApplicationPosDbContext context)
        {
            this.Customers = new CustomerRepository(context);
            this.context = context;
        }
        public ICustomerRepository Customers { get; }

        public int Complete()
        {
           return   this.context.SaveChanges();
        }

        public void Dispose()
        {
            this.context.Dispose();
        }
    }
}
