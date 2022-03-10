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
         
        IProductRepository ProductRepository { get; }

        int Complete();


    }
}
