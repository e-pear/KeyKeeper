using KeyKeeper.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Tabs.CareTaker
{
    /// <summary>
    /// Simple object binded with particular controls in view. Provides also basic mass set and get methods.
    /// Doesn't inform view about it's changes. The parental view model do so for it.
    /// </summary>
    public class PresentedRegisteredEmployee
    {
        // backing fields:
        private ObservableCollection<RoomKey> _heldKeys;
        private string _id;
        private string _name;
        private string _surname;
        private string _position;
        private string _department;
        private bool _isRegistrationValid;
        // properties:
        public string Id { get { return _id; } }
        public string Name { get { return _name; } }
        public string Surname { get { return _surname; } }
        public string Position { get { return _position; } }
        public string Department { get { return _department; } }

        public ObservableCollection<RoomKey> HeldKeys { get { return _heldKeys; } }

        public bool IsRegistrationValid { get { return _isRegistrationValid; } }
        // constructor:
        public PresentedRegisteredEmployee()
        {
            Reset();
        }
        // mass reset method:
        public virtual void Reset()
        {
            _id = null;
            _name = null;
            _surname = null;
            _position = null;
            _department = null;

            _heldKeys = new ObservableCollection<RoomKey>();

            _isRegistrationValid = false;
        }
        // keys collection refresh method:
        public virtual void RefreshHeldKeys(IEnumerable<RoomKey> keys)
        {
            _heldKeys = new ObservableCollection<RoomKey>(keys);
        }
        // mass setter:
        public virtual void RegisterNewEmployee(Employee employee, IEnumerable<RoomKey> keys)
        {
            _id = employee.Employee_Id;
            _name = employee.Name;
            _surname = employee.Surname;
            _position = employee.Position;
            _department = employee.Department;

            _heldKeys = new ObservableCollection<RoomKey>(keys);

            _isRegistrationValid = true;
        }
        // handy getter:
        public Employee GetRegisteredEmployee()
        {
            if (IsRegistrationValid) return new Employee(Id, Name, Surname, Position, Department);
            else return null;
        }
    }
}
