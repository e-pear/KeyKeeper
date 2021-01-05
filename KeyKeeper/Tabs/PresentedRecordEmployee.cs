using KeyKeeper.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Tabs
{
    public class PresentedRecordEmployee : IPresentedRecordOf<Employee>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }

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

        public bool IsEqualToSelectedRecord(Employee selectedRecord)
        {
            if (this.Id != selectedRecord.Employee_Id) return false;
            if (this.Name != selectedRecord.Name) return false;
            if (this.Surname != selectedRecord.Surname) return false;
            if (this.Position != selectedRecord.Position) return false;
            if (this.Department != selectedRecord.Department) return false;

            return true;
        }

        public Employee GetRecordFromPresentedRecord()
        {
            return new Employee(Id, Name, Surname, Position, Department);
        }

        public void SynchronizePresentedRecordWith(Employee selectedRecord)
        {
            if (selectedRecord != null)
            {
                Id = selectedRecord.Employee_Id;
                Name = selectedRecord.Name;
                Surname = selectedRecord.Surname;
                Position = selectedRecord.Position;
                Department = selectedRecord.Department;
            }
            else ClearPresentedRecord();
        }

        public void ClearPresentedRecord()
        {
            Id = null;
            Name = null;
            Surname = null;
            Position = null;
            Department = null;
        }

        public void SetPresentedIdCode(string code)
        {
            Id = code;
        }

        public bool IsSaveable()
        {
            if (String.IsNullOrEmpty(this.Id)) return false;
            if (String.IsNullOrEmpty(this.Name)) return false;
            if (String.IsNullOrEmpty(this.Surname)) return false;
            if (String.IsNullOrEmpty(this.Position)) return false;
            if (String.IsNullOrEmpty(this.Department)) return false;

            return true;
        }
    }
}
