using PointOfSalesAPI.Repository.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSalesAPI.UnitOfWork
{
   public interface IUnitOfWork
    {

        public ICustomerRepository Customers { get;  }

        int Complete();
    }
}
