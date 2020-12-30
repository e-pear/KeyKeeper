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
    /// Concrete representation of query type operations/"services" on database. Follows CQRS pattern.
    /// Methods of this class are meant to be "dumb". They only manage db connection and encapsulate queries.
    /// They may and will be throwing exceptions if used without caution.
    /// </summary>
    public class DataModelQueryOperations : IDataModelQueryOperations
    {
        /// <summary>
        /// Query returning all room keys that are present in data base.
        /// </summary>
        /// <returns>An enumerable collection of RoomKey objects.</returns>
        public async Task<IEnumerable<RoomKey>> GetAllRoomKeysAsync() // redundant with analogous method from RecordRepositoryManagerOperations object, left here for sake of convention ;)
        {
            IEnumerable<RoomKey> results;
            using (CompanyDbContext companyDb = new CompanyDbContext())
            {
                results = await companyDb.RoomKeys.ToListAsync();
            }
            return results;
        }

        /// <summary>
        /// Query returning all keys that are in possession of specified employee.
        /// </summary>
        /// <param name="employee">Target Employee object.</param>
        /// <returns>An enumerable collection of RoomKey objects. Will be empty when conditions aren't met.</returns>
        public async Task<IEnumerable<RoomKey>> GetAllRoomKeysOwnedByEmployeeAsync(Employee employee)
        {
            IEnumerable<RoomKey> results;
            using(CompanyDbContext companyDb = new CompanyDbContext())
            {
                results = await companyDb.RoomKeys.Where(rK => rK.AssignedEmployee_Id == employee.Employee_Id).ToListAsync();
            }
            return results;
        }
        /// <summary>
        /// Query returning all keys which weren't handover to employees.
        /// </summary>
        /// <returns>An enumerable collection of RoomKey objects. Will be empty when conditions aren't met.</returns>
        public async Task<IEnumerable<RoomKey>> GetAvailableRoomKeysAsync()
        {
            IEnumerable<RoomKey> results;
            using (CompanyDbContext companyDb = new CompanyDbContext())
            {
                results = await companyDb.RoomKeys.Where(rK => rK.AssignedEmployee_Id == null).ToListAsync();
            }
            return results;
        }
        /// <summary>
        /// Query returning employee who is in possession of specified key.
        /// </summary>
        /// <param name="key">Target RoomKey object.</param>
        /// <returns>An Employee type object. May return null reference in case none of the employees has the given key. </returns>
        public async Task<Employee> GetEmployeeByOwnedKeyAsync(RoomKey key)
        {
            IEnumerable<Employee> results;
            using (CompanyDbContext companyDb = new CompanyDbContext())
            {
                results = await companyDb.Employees.Where(e => e.RecievedRoomKeys.Select(k => k.RoomKey_Id).Contains(key.RoomKey_Id)).ToListAsync();
            }
            return results.FirstOrDefault();
        }
        /// <summary>
        /// Query returning all employees who have specified name and surname.
        /// </summary>
        /// <param name="employeeName"></param>
        /// <param name="employeeSurname"></param>
        /// <returns>An enumerable collection of Employee objects. Will be empty when conditions aren't met.</returns>
        public async Task<IEnumerable<Employee>> GetEmployeesByNamesAsync(string employeeName, string employeeSurname)
        {
            IEnumerable<Employee> results;
            using (CompanyDbContext companyDb = new CompanyDbContext())
            {
                results = await companyDb.Employees.Where(e => e.Name == employeeName && e.Surname == employeeSurname).ToListAsync();
            }
            return results;
        }

        /// <summary>
        /// Query returning all keys that are handovered to employees.
        /// </summary>
        /// <returns>An enumerable collection of RoomKey objects. Will be empty when conditions aren't met.</returns>
        public async Task<IEnumerable<RoomKey>> GetHandoveredRoomKeysAsync()
        {
            IEnumerable<RoomKey> results;
            using (CompanyDbContext companyDb = new CompanyDbContext())
            {
                results = await companyDb.RoomKeys.Where(rK => rK.AssignedEmployee_Id != null).ToListAsync();
            }
            return results;
        }
        /// <summary>
        /// Query returning specified room key by it's id code.
        /// </summary>
        /// <param name="roomKeyId"></param>
        /// <returns>A room key object. May return null in case the key does not exist in database. </returns>
        public async Task<RoomKey> GetRoomKeyByIdAsync(string roomKeyId)
        {
            IEnumerable<RoomKey> results;
            using (CompanyDbContext companyDb = new CompanyDbContext())
            {
                results = await companyDb.RoomKeys.Where(rK => rK.RoomKey_Id == roomKeyId).ToListAsync();
            }
            return results.FirstOrDefault();
        }
    }
}
