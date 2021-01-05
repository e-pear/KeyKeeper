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
    public class EmployeeBaseViewModel : DataBaseViewModel<Employee>
    {
        public EmployeeBaseViewModel(IRepositoryManagerOperations<Employee> dataProvider) : base(dataProvider, "Baza Pracowników") 
        {
            _presentedRecord = new PresentedRecordEmployee();
        }
    }
}
