using KeyKeeper.Model.DataAccess;
using KeyKeeper.Model.DataOperations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Model.DataModel
{
    /// <summary>
    /// Concrete representation of query and command type operations/"services" on database. Does not follow CQRS pattern*.
    /// Methods of this class are meant to be "dumb". They only manage db connection and encapsulate queries and command.
    /// They may and will be throwing exceptions if used without caution.
    /// *Designed to work with pure CRUD type viewmodels.
    /// </summary>
    /// <typeparam name="T">Any EF POCO class.</typeparam>
    public class RecordRepositoryManagerOperations<T> : IRepositoryManagerOperations<T> where T : class
    {
        /// <summary>
        /// Adds record to the database.
        /// </summary>
        /// <param name="record">Record of given type.</param>
        /// <returns></returns>
        public async Task AddAsync(T record)
        {
            using(var companyDb = new CompanyDbContext())
            {
                companyDb.Set<T>().Add(record);
                await companyDb.SaveChangesAsync();
            }
        }
        /// <summary>
        /// Adds a collection of records to the database.
        /// </summary>
        /// <param name="records">An enumerable collection of records of given type.</param>
        /// <returns></returns>
        public async Task AddAsync(IEnumerable<T> records)
        {
            using (var companyDb = new CompanyDbContext())
            {
                companyDb.Set<T>().AddRange(records);
                await companyDb.SaveChangesAsync();
            }
        }
        /// <summary>
        /// Returns all records of given type.
        /// </summary>
        /// <returns>An enumerable collection of records of given type.</returns>
        public async Task<IEnumerable<T>> GetAllRecordsAsync()
        {
            IEnumerable<T> collection;

            using (var companyDb = new CompanyDbContext())
            {
                collection = await companyDb.Set<T>().ToListAsync();
            }

            return collection;
        }
        /// <summary>
        /// Removes given record from the database.
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public async Task RemoveAsync(T record)
        {
            using (var companyDb = new CompanyDbContext())
            {
                companyDb.Set<T>().Attach(record);
                companyDb.Set<T>().Remove(record);
                await companyDb.SaveChangesAsync();
            }
        }
        /// <summary>
        /// Removes given collection of records from the database.
        /// </summary>
        /// <param name="records"></param>
        /// <returns></returns>
        public async Task RemoveAsync(IEnumerable<T> records)
        {
            using (var companyDb = new CompanyDbContext())
            {
                foreach (T record in records) companyDb.Set<T>().Attach(record);
                companyDb.Set<T>().RemoveRange(records);
                await companyDb.SaveChangesAsync();
            }
        }
        /// <summary>
        /// Overwrites an existing record in database with given one.
        /// </summary>
        /// <param name="updatedRecord">Previously modified object.</param>
        /// <returns></returns>
        public async Task UpdateRecordAsync(T updatedRecord)
        {
            using (var companyDb = new CompanyDbContext())
            {
                companyDb.Entry(updatedRecord).State = EntityState.Modified;
                await companyDb.SaveChangesAsync();
            }
        }
    }
}
