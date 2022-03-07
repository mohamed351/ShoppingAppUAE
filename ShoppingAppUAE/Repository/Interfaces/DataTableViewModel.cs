using System.Collections.Generic;

namespace ShoppingAppUAE.Repository.Interfaces
{
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
