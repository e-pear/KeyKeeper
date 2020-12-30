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
    public interface IDataModelCommandOperations
    {
        /// <summary>
        /// Assign a single room key to specified employee.
        /// </summary>
        /// <param name="key">Assigned key.</param>
        /// <param name="employee">Target employee.</param>
        /// <returns></returns>
        Task HandOverTheRoomKeyToEmployeeAsync(RoomKey key, Employee employee);
        /// <summary>
        /// Assign a collection of room keys to specified employee.
        /// </summary>
        /// <param name="keys">Enumerable collection of RoomKey objects.</param>
        /// <param name="employee">Target employee.</param>
        /// <returns></returns>
        Task HandOverMultipleRoomKeysToEmployeeAsync(IEnumerable<RoomKey> keys, Employee employee);
        /// <summary>
        /// Resets assigned employee of specified room key to null value.
        /// </summary>
        /// <param name="key">Target room key.</param>
        /// <returns></returns>
        Task TakeTheRoomKeyAsync(RoomKey key);
        /// <summary>
        /// Resets assigned employee of room keys collection to null value. 
        /// </summary>
        /// <param name="keys">Target collection of room keys.</param>
        /// <returns></returns>
        Task TakeMultipleRoomKeysAsync(IEnumerable<RoomKey> keys);
    }
}
