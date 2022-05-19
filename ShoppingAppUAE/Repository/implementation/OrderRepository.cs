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
    public class OrderRepository : Repository<Order, int>, IOrderRepository
    {
        private readonly ApplicationDbContext context;

        public OrderRepository(ApplicationDbContext context) :
            base(context)
        {
            this.context = context;
        }

        public DataTableViewModel<Order> GetDataTableOrders(int start, int lenght, Expression<Func<Order, bool>> search, Expression<Func<Order, DateTime>> OrderBy)
        {
            var query = context.orders.Include(a => a.ApplicationUser)
                 .ThenInclude(a => a.Counties)
                 .Include(a=> a.ShippingAndDelivery)
                 .Where(search)
                 .Skip(start)
                 .Take(lenght)
                 .OrderBy(OrderBy).ToList();
            return new DataTableViewModel<Order>()
            {
                data = query,
                recordsFiltered = context.orders.Where(search).Count(),
                recordsTotal  = context.orders.Count()
            };


        }
    }
}
