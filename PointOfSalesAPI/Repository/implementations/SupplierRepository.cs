using PointOfSalesAPI.Models;
using PointOfSalesAPI.Repository.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSalesAPI.Repository.implementations
{
    public class SupplierRepository : Repository<Supplier, int>, ISupplierRepository
    {
        public SupplierRepository(ApplicationPosDbContext context) : base(context)
        {
        }
    }
}
