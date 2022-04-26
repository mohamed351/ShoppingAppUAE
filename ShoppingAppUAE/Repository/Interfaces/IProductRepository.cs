using ShoppingAppUAE.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Repository.Interfaces
{
   public interface IProductRepository :IRepository<Product,int>
    {

        public DataTableViewModel<Product> SeacrchProduct(int start, int lenght, Expression<Func<Product, bool>> search, Expression<Func<Product, int>> OrderBy);
    }
}
