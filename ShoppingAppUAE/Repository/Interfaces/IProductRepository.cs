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

        public DataTableViewModel<Product> SeacrchProduct(int start, int lenght, Expression<Func<Product, bool>> search, Expression<Func<Product, decimal>> OrderBy);

        public DataTableViewModel<Product> SeacrchProductSortPriceHighertoLower(int start, int lenght, Expression<Func<Product, bool>> search);

        public DataTableViewModel<Product> SeacrchProductSortPricelowerToHigher(int start, int lenght, Expression<Func<Product, bool>> search);

    }
}
