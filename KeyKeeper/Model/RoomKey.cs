using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Model
{
    public class RoomKey
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [MaxLength(4), MinLength(4)]
        [Index(IsUnique = true)]
        public string RoomKey_Id { get; protected set; }
        [Required]
        public string RoomName { get; protected set; }

        public string AssignedEmployee_Id { get; protected set; }

        [ForeignKey(name: "AssignedEmployee_Id")]
        public Employee AssignedEmployee { get; set; }

        private RoomKey() { }
        public RoomKey(string keyCode, string roomName)
        {
            this.RoomKey_Id = keyCode;
            this.RoomName = roomName;
        }
        public void UpdateRecord(RoomKey updatedRoomKey)
        {
            this.RoomName = updatedRoomKey.RoomName;
        }
        public override string ToString()
        {
            return $"\"{RoomKey_Id}\";\"{RoomName}\"";
        }
    }
}
