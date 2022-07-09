using PointOfSalesAPI.Models;
using PointOfSalesAPI.Repository.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSalesAPI.Repository.implementations
{
    public class CustomerRepository : Repository<Customer, int>, ICustomerRepository
    {
        public CustomerRepository(ApplicationPosDbContext context)
            :base(context)
        {

        }


    }
}
