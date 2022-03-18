using ShoppingAppUAE.Data;
using ShoppingAppUAE.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Repository.implementation
{
    public class BrandRepository : Repository<Brand, int>, IBrandRepository
    {
        public BrandRepository(ApplicationDbContext context) 
            : base(context)
        {
        }
    }
}
