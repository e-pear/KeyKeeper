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
    /// The Employee POCO class.
    /// </summary>
    public class Employee : IRecord
    {
        // Base properties:
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [MaxLength(4), MinLength(4)]
        [Index(IsUnique = true)]
        public string Employee_Id { get; protected set; }
        [Required]
        public string Name { get; protected set; }
        [Required]
        public string Surname { get; protected set; }
        [Required]
        public string Position { get; protected set; }
        [Required]
        public string Department { get; protected set; }
        
        // Navigation properties:
        public ICollection<RoomKey> RecievedRoomKeys { get; protected set; } = new List<RoomKey>();

        // Constructors:
        private Employee() { }
        public Employee (string employeeCode, string name, string surname, string position, string department)
        {
            this.Employee_Id = employeeCode;
            this.Name = name;
            this.Surname = surname;
            this.Position = position;
            this.Department = department;
        }
        // Base Methods:
        public void Update(Employee updatedEmployee)
        {
            this.Name = updatedEmployee.Name;
            this.Surname = updatedEmployee.Surname;
            this.Position = updatedEmployee.Position;
            this.Department = updatedEmployee.Department;
        }
        public override string ToString()
        {
            return $"\"{Employee_Id}\";\"{Name}\";\"{Surname}\";\"{Position}\";\"{Department}\"";
        }
        // IRecord Implementation:
        public int GetIdNumber()
        {
            return Convert.ToInt32(Employee_Id);
        }
        public string GetIdCode()
        {
            return Employee_Id;
        }
    }
}
