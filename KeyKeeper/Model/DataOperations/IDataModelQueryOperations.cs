using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Model.DataOperations
{
    /// <summary>
    /// An additional abstract layer allowing to separate the EntityFramework dependent object from rest of the application.
    /// Defines basic commands. Follows CQRS pattern.
    /// </summary>
    public interface IDataModelQueryOperations
    {
        /// <summary>
        /// Query returning all employees who have specified name and surname.
        /// </summary>
        /// <param name="employeeName"></param>
        /// <param name="employeeSurname"></param>
        /// <returns>An enumerable collection of Employee objects. Will be empty when conditions aren't met.</returns>
        Task<IEnumerable<Employee>> GetEmployeesByNamesAsync(string employeeName, string employeeSurname);
        /// <summary>
        /// Query returning employee who is in possession of specified key.
        /// </summary>
        /// <param name="key">Target RoomKey object.</param>
        /// <returns>An Employee type object. May return null reference in case none of the employees has the given key. </returns>
        Task<Employee> GetEmployeeByOwnedKeyAsync(RoomKey key);
        
        /// <summary>
        /// Query returning specified room key by it's id code.
        /// </summary>
        /// <param name="roomKeyId"></param>
        /// <returns>A room key object. May return null in case the key does not exist in database.</returns>
        Task<RoomKey> GetRoomKeyByIdAsync(string roomKeyId);
        /// <summary>
        /// Query returning all keys that are in possession of specified employee.
        /// </summary>
        /// <param name="employee">Target Employee object.</param>
        /// <returns>An enumerable collection of RoomKey objects. Will be empty when conditions aren't met.</returns>
        Task<IEnumerable<RoomKey>> GetAllRoomKeysOwnedByEmployeeAsync(Employee employee);
        /// <summary>
        /// Query returning all keys which weren't handover to employees.
        /// </summary>
        /// <returns>An enumerable collection of RoomKey objects. Will be empty when conditions aren't met.</returns>
        Task<IEnumerable<RoomKey>> GetAvailableRoomKeysAsync();
        /// <summary>
        /// Query returning all keys that are handovered to employees.
        /// </summary>
        /// <returns>An enumerable collection of RoomKey objects. Will be empty when conditions aren't met.</returns>
        Task<IEnumerable<RoomKey>> GetHandoveredRoomKeysAsync();
        /// <summary>
        /// Query returning all room keys that are present in data base.
        /// </summary>
        /// <returns>An enumerable collection of RoomKey objects.</returns>
        Task<IEnumerable<RoomKey>> GetAllRoomKeysAsync();

    }
}