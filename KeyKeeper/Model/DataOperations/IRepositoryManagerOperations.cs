using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Model.DataOperations
{
    /// <summary>
    /// An additional abstract layer allowing to separate the EntityFramework dependent object from rest of the application.
    /// Defines basic CRUD operations. Does not follow CQRS pattern.
    /// </summary>
    public interface IRepositoryManagerOperations<T> where T : class
    {
        /// <summary>
        /// Adds record to the database.
        /// </summary>
        /// <param name="record">Record of given type.</param>
        /// <returns></returns>
        Task AddAsync(T record);
        /// <summary>
        /// Adds a collection of records to the database.
        /// </summary>
        /// <param name="records">An enumerable collection of records of given type.</param>
        /// <returns></returns>
        Task AddAsync(IEnumerable<T> records);
        /// <summary>
        /// Overwrites an existing record in database with given one.
        /// </summary>
        /// <param name="updatedRecord">Previously modified object.</param>
        /// <returns></returns>
        Task UpdateRecordAsync(T newRecord);
        /// <summary>
        /// Removes given record from the database.
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        Task RemoveAsync(T record);
        /// <summary>
        /// Removes given collection of records from the database.
        /// </summary>
        /// <param name="records"></param>
        /// <returns></returns>
        Task RemoveAsync(IEnumerable<T> records);
        
        /// <summary>
        /// Returns all records of given type.
        /// </summary>
        /// <returns>An enumerable collection of records of given type.</returns>
        Task<IEnumerable<T>> GetAllRecordsAsync();
    }
}
