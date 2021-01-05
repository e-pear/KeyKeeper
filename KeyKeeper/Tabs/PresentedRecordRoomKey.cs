using KeyKeeper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Tabs
{
    class PresentedRecordRoomKey : IPresentedRecordOf<RoomKey>
    {
        public string Id { get; set; }
        public string Room { get; set; }

        public void ClearPresentedRecord()
        {
            Id = null;
            Room = null;
        }

        public string GetPresentedIdCode()
        {
            return Id;
        }

        public int? GetPresentedIdNumber()
        {
            int result;
            if (Int32.TryParse(Id, out result)) return result;
            else return null;
        }

        public RoomKey GetRecordFromPresentedRecord()
        {
            return new RoomKey(Id, Room);
        }

        public bool IsEqualToSelectedRecord(RoomKey selectedRecord)
        {
            if (this.Id != selectedRecord.RoomKey_Id) return false;
            if (this.Room != selectedRecord.RoomName) return false;

            return true;
        }

        public bool IsSaveable()
        {
            if (String.IsNullOrEmpty(this.Id)) return false;
            if (String.IsNullOrEmpty(this.Room)) return false;
            
            return true;
        }

        public void SetPresentedIdCode(string code)
        {
            Id = code;
        }

        public void SynchronizePresentedRecordWith(RoomKey selectedRecord)
        {
            if (selectedRecord != null)
            {
                Id = selectedRecord.RoomKey_Id;
                Room = selectedRecord.RoomName;
            }
            else ClearPresentedRecord();
        }
    }
}
