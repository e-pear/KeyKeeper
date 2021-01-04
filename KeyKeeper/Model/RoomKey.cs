using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Model
{
    /// <summary>
    /// The RoomKey POCO class.
    /// </summary>
    public class RoomKey : IRecord
    {
        // Base properties:
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [MaxLength(4), MinLength(4)]
        [Index(IsUnique = true)]
        public string RoomKey_Id { get; protected set; }
        [Required]
        public string RoomName { get; protected set; }
        // Foregin key:
        public string AssignedEmployee_Id { get; protected set; }
        // Navigation properties:
        [ForeignKey(name: "AssignedEmployee_Id")]
        public Employee AssignedEmployee { get; protected set; }

        // Constructors:
        private RoomKey() { }
        public RoomKey(string keyCode, string roomName)
        {
            this.RoomKey_Id = keyCode;
            this.RoomName = roomName;
        }
        // Base Mathods:
        public void Update(RoomKey updatedRoomKey)
        {
            this.RoomName = updatedRoomKey.RoomName;
        }
        /// <summary>
        /// Changes or resets an employee assigned to room key. 
        /// </summary>
        /// <param name="newEmployee">Employee type object, or null reference in case of reset the assignation.</param>
        public void ChangeAssignedEmployee(Employee newEmployee)
        {
            this.AssignedEmployee_Id = newEmployee != null ? newEmployee.Employee_Id : null;
        }
        public override string ToString()
        {
            return $"\"{RoomKey_Id}\";\"{RoomName}\"";
        }
        // IRecord Implementation:
        public int GetIdNumber()
        {
            return Convert.ToInt32(RoomKey_Id);
        }
        public string GetIdCode()
        {
            return RoomKey_Id;
        }
    }
}
