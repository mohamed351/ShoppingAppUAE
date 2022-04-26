using Microsoft.EntityFrameworkCore;
using ShoppingAppUAE.Data;
using ShoppingAppUAE.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Repository.implementation
{
    public class ProductRepository : Repository<Product, int>, IProductRepository
    {
        private readonly ApplicationDbContext context;

        public ProductRepository(ApplicationDbContext context) 
            : base(context)
        {
            this.context = context;
        }

        public DataTableViewModel<Product> SeacrchProduct(int start, int lenght, Expression<Func<Product, bool>> search, Expression<Func<Product, int>> OrderBy)
        {
            var query = context.Products.Include(a => a.Category).Include(a => a.Brand).Where(search)
                .OrderBy(OrderBy).Skip(start)
                .Take(lenght);
            ///Get the Total Count 
            var count = context.Products.Include(a => a.Category).Include(a => a.Brand).Where(search).Count();
            ///Return The ViewModel That DataTable will take it in Json 
            return new DataTableViewModel<Product>()
            {
                data = query.ToList(),
                recordsFiltered = count,
                recordsTotal = count
            };
        }
    }
}
