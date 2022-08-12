using ShoppingAppUAE.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Repository.UnitOfWork
{
   public interface IUnitOfWork :IDisposable
    {

         ICategoryRepository Categories { get; }
         
        IProductRepository Products { get; }

        IBrandRepository Brands { get; }

        public IOrderRepository Orders { get; }

        public IContactMessageRepository ContactMessages { get; }

        public IBloggerRepository Blogger { get; }


        int Complete();


    }
}
