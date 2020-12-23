using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Model.DataAccess.DataBase.Domain
{
    public class RoomKey
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoomKeyId { get; private set; }
        public string RoomName { get; private set; }

        [ForeignKey(name: "EmployeeId")]
        public Employee AssignedEmployee { get; set; }

        private RoomKey() { }
        public RoomKey(string roomName)
        {
            this.RoomName = roomName;
        }
    }
}
