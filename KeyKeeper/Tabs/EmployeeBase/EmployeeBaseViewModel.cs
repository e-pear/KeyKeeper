using KeyKeeper.Model;
using KeyKeeper.Model.DataOperations;
using KeyKeeper.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Tabs.EmployeeBase
{
    /// <summary>
    /// Mighty CRUD view model object for Employee CRUD view.
    /// </summary>
    public class EmployeeBaseViewModel : DataBaseViewModel<Employee>
    {
        // Constructor:
        public EmployeeBaseViewModel(IRepositoryManagerOperations<Employee> dataProvider) : base(dataProvider, "Baza Pracowników") 
        {
            _presentedRecord = new PresentedRecordEmployee();
        }
    }
}
