using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Repository.Interfaces
{
    public interface IRepository<TEntity,TKey> where TEntity : class
    {

        IEnumerable<TEntity> GetAll();


        TEntity GetByID(TKey key);

        TEntity GetByIDAsNotTracking(Expression<Func<TEntity, bool>> expression);

        Task<TEntity> GetByIDAsync(TKey key);

        bool AnyEntity(Expression<Func<TEntity, bool>> expression);

        void Add(TEntity entity);


        IEnumerable<TEntity> GetByCondition(Expression< Func<TEntity, bool>> search);

        public DataTableViewModel<TEntity> GetDataTable(int start, int lenght, Expression< Func<TEntity, bool>> search, Expression< Func<TEntity, TKey>> OrderBy);


        void Edit(TEntity entity);


        void Delete(TEntity entity);



    }
}
