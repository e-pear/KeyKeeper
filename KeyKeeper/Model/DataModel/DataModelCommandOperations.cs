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
    /// Concrete representation of command type operations/"services" on database. Follows CQRS pattern.
    /// Methods of this class are meant to be "dumb". They only manage db connection and encapsulate commands.
    /// They may and will be throwing exceptions if used without caution.
    /// </summary>
    public class DataModelCommandOperations : IDataModelCommandOperations
    {
        /// <summary>
        /// Assign a collection of room keys to specified employee.
        /// </summary>
        /// <param name="keys">Enumerable collection of RoomKey objects.</param>
        /// <param name="employee">Target employee.</param>
        /// <returns></returns>
        public async Task HandOverMultipleRoomKeysToEmployeeAsync(IEnumerable<RoomKey> keys, Employee employee)
        {
            using (CompanyDbContext companyDb = new CompanyDbContext())
            {
                foreach (RoomKey key in keys)
                {
                    key.ChangeAssignedEmployee(employee);
                    companyDb.Entry(key).State = EntityState.Modified;
                }
                await companyDb.SaveChangesAsync();
            }
        }
        /// <summary>
        /// Assign a single room key to specified employee.
        /// </summary>
        /// <param name="key">Assigned key.</param>
        /// <param name="employee">Target employee.</param>
        /// <returns></returns>
        public async Task HandOverTheRoomKeyToEmployeeAsync(RoomKey key, Employee employee)
        {
            using(CompanyDbContext companyDb = new CompanyDbContext())
            {
                key.ChangeAssignedEmployee(employee);
                companyDb.Entry(key).State = EntityState.Modified;

                await companyDb.SaveChangesAsync();
            }
        }
        /// <summary>
        /// Resets assigned employee of room keys collection to null value. 
        /// </summary>
        /// <param name="keys">Target collection of room keys.</param>
        /// <returns></returns>
        public async Task TakeMultipleRoomKeysAsync(IEnumerable<RoomKey> keys)
        {
            using (CompanyDbContext companyDb = new CompanyDbContext())
            {
                foreach (RoomKey key in keys)
                {
                    key.ChangeAssignedEmployee(null);
                    companyDb.Entry(key).State = EntityState.Modified;
                }
                await companyDb.SaveChangesAsync();
            }
        }
        /// <summary>
        /// Resets assigned employee of specified room key to null value.
        /// </summary>
        /// <param name="key">Target room key.</param>
        /// <returns></returns>
        public async Task TakeTheRoomKeyAsync(RoomKey key)
        {
            using (CompanyDbContext companyDb = new CompanyDbContext())
            {
                key.ChangeAssignedEmployee(null);
                companyDb.Entry(key).State = EntityState.Modified;

                await companyDb.SaveChangesAsync();
            }
        }
    }
}
