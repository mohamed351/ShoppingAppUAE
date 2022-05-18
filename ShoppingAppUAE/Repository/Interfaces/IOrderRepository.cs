using ShoppingAppUAE.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Repository.Interfaces
{
   public interface IOrderRepository:IRepository<Order,int>
    {
        public DataTableViewModel<Order> GetDataTableOrders(int start, int lenght, Expression<Func<Order, bool>> search, Expression<Func<Order, DateTime>> OrderBy);
    }
}
