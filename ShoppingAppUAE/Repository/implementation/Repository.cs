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
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {
        private readonly ApplicationDbContext context;

        public Repository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Add(TEntity entity)
        {
            context.Add<TEntity>(entity);
        }

        public bool AnyEntity(Expression<Func<TEntity,bool>> expression)
        {
            return context.Set<TEntity>().Any(expression);
        }

        public void Delete(TEntity entity)
        {
            context.Entry<TEntity>(entity).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
        }

        public void Edit(TEntity entity)
        {
            context.Entry<TEntity>(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public IEnumerable<TEntity> GetAll()
        {
           return context.Set<TEntity>().ToList();
        }

        public IEnumerable<TEntity> GetByCondition(Expression<Func<TEntity, bool>> search)
        {
            return context.Set<TEntity>().Where(search).ToList();
        }

        public TEntity GetByID(TKey key)
        {
            return context.Find<TEntity>(key);
        }

        public TEntity GetByIDAsNotTracking(Expression<Func<TEntity,bool>> expression)
        {
            return context.Set<TEntity>().AsNoTracking().FirstOrDefault(expression);
        }

        public async Task<TEntity> GetByIDAsync(TKey key)
        {
            return await context.FindAsync<TEntity>(key);
        }

        public DataTableViewModel<TEntity> GetDataTable(int start, int lenght, Expression<Func<TEntity, bool>> search, Expression<Func<TEntity, TKey>> OrderBy)
        {
            ///query the skip number of Rows and Take number of rows 
            ///Like Take and Fetch 
            var query = context.Set<TEntity>().Where(search)
                   .OrderBy(OrderBy).Skip(start)
                   .Take(lenght);
            ///Get the Total Count 
            var count = context.Set<TEntity>().Where(search).Count();
            ///Return The ViewModel That DataTable will take it in Json 
            return new DataTableViewModel<TEntity>()
            {
                data = query,
                recordsFiltered = count,
                recordsTotal = count
            };
        }
    }
}
