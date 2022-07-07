using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PointOfSalesAPI.Repository.interfaces
{
    public interface IRepository<TEntity, TKey> where TEntity : class
    {

        IEnumerable<TEntity> GetAll();


        TEntity GetByID(TKey key);

        TEntity GetByIDAsNotTracking(Expression<Func<TEntity, bool>> expression);

        Task<TEntity> GetByIDAsync(TKey key);

        bool AnyEntity(Expression<Func<TEntity, bool>> expression);

        void Add(TEntity entity);


        IEnumerable<TEntity> GetByCondition(Expression<Func<TEntity, bool>> search);

        public DataTableViewModel<TEntity> GetDataTable(int start, int lenght, Expression<Func<TEntity, bool>> search, Expression<Func<TEntity, TKey>> OrderBy);


        void Edit(TEntity entity);


        void Delete(TEntity entity);



    }

    /// <summary>
    /// DataTable ViewModel i didn't use Naming Convention in this class because they what their own name
    /// </summary>
    /// <typeparam name="T">The class that will apply on Data Table</typeparam>
    public class DataTableViewModel<T> where T : class
    {
        /// <summary>
        /// The Data whether the Model or Brand , etc
        /// That class is  generic
        /// </summary>
        public IEnumerable<T> data { get; set; }
        /// <summary>
        /// The Total Number of Filtered Records 
        /// </summary>
        public int recordsFiltered { get; set; }
        /// <summary>
        /// The Total Number of Records
        /// </summary>
        public int recordsTotal { get; set; }

    }
}
