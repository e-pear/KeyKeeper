using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Model.DataAccess.DataBase.Domain
{
    public class Employee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Position { get; private set; }
        public string Department { get; private set; }

        public ICollection<RoomKey> RecievedRoomKeys { get; private set; } = new List<RoomKey>();

        private Employee() { }
        public Employee (string name, string surname, string position, string department)
        {
            this.Name = name;
            this.Surname = surname;
            this.Position = position;
            this.Department = department;
        }
    }
}
