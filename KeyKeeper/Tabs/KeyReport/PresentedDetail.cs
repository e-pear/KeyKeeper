using KeyKeeper.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Tabs.KeyReport
{
    /// <summary>
    /// Just a dumb container to cooperate with view through bindings.
    /// Provides detailed information for view about key status. However can't inform the view about it's changes. The parental view model object does this for it.
    /// </summary>
    public class PresentedDetail
    {
        // properties:
        public bool IsKeyStoredAtTheGateHouse { get; private set; }
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Position { get; private set; }
        public string Department { get; private set; }
        // constructor:
        public PresentedDetail()
        {
            SetAsGatehouseDetail();
        }
        // mass setter:
        public void SetAsGatehouseDetail()
        {
            IsKeyStoredAtTheGateHouse = true;

            Id = null;
            Name = null;
            Surname = null;
            Position = null;
            Department = null;
        }
        // mass seter:
        public void SetAsEmployeeDetail(string employeeId, string employeeName, string employeeSurname, string employeePosition, string employeeDepartment)
        {
            IsKeyStoredAtTheGateHouse = false;

            Id = employeeId;
            Name = employeeName;
            Surname = employeeSurname;
            Position = employeePosition;
            Department = employeeDepartment;
        }
        // "a handier" mass seter:
        public void SetAsEmployeeDetail(Employee employee)
        {
            SetAsEmployeeDetail(employee.Employee_Id, employee.Name, employee.Surname, employee.Position, employee.Department);
        }
    }
}
