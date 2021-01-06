using KeyKeeper.Dialogs;
using KeyKeeper.Model;
using KeyKeeper.Model.DataOperations;
using KeyKeeper.Model.DataModel;
using KeyKeeper.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KeyKeeper.Tabs.CareTaker
{
    public class CareTakerViewModel : Tab
    {
        IDataModelCommandOperations _dataCommandProvider;
        IDataModelQueryOperations _dataQueryProvider;
        
        //Presented Employee:
        public PresentedRegisteredEmployee RegisteredEmployee { get; }

        public ICommand RegisterEmployeeCommand { get; }
        public ICommand HandoverTheKeyCommand { get; }
        public ICommand TakeTheKeyBackCommand { get; }
        public ICommand ResetRegistrationCommand { get; }

        public CareTakerViewModel(IDataModelCommandOperations dataCommandProvider, IDataModelQueryOperations dataQueryProvider):base("Menadżer Kluczy")
        {
            _dataCommandProvider = dataCommandProvider;
            _dataQueryProvider = dataQueryProvider;
            
            RegisteredEmployee = new PresentedRegisteredEmployee();

            RegisterEmployeeCommand = new CommandRelay(RegisterEmployee, () => true);
            HandoverTheKeyCommand = new CommandRelay(HandoverTheKey, CanExecute_HandOverTheKey);
            TakeTheKeyBackCommand = new CommandRelay(TakeTheKeyBack, CanExecute_TakeTheKeyBack);
            ResetRegistrationCommand = new CommandRelay(ClearRegistration, CanExecute_ClearRegistration);
        }
        protected virtual void RegisterEmployee()
        {
            int numberOfPotentiallyRequestedEmployees;
            string idUserCode;
            List<Employee> potentialEmployees;
            IEnumerable<Employee> employees = UI_RequestEmployeeNames();

            if (employees == null)
            {
                DialogBoxFactory.GetInfoBox("Rejestracja anulowana!").Show();
                return;
            }

            numberOfPotentiallyRequestedEmployees = employees.Count();

            switch ((numberOfPotentiallyRequestedEmployees))
            {
                case 0:
                    
                    DialogBoxFactory.GetInfoBox("Rejestracja anulowana!\n\nPracownik nie istnieje w Bazie.").Show();
                    break;
                
                case 1:

                    RegisteredEmployee.RegisterNewEmployee(employees.First(), DB_RequestEmployeesOwnedKeys(employees.First()));
                    break;
                
                default:
                    
                    idUserCode = UI_RequestId("Proszę podać numer identyfikacyjny Pracownika:");
                    if(idUserCode == null)
                    {
                        DialogBoxFactory.GetInfoBox("Rejestracja anulowana!").Show();
                        return;
                    }

                    potentialEmployees = employees.Where(e => e.Employee_Id == idUserCode).ToList();

                    if (potentialEmployees.Count == 0)
                    {
                        DialogBoxFactory.GetInfoBox($"Nie znaleziono pracownika: {potentialEmployees[0].Name} {potentialEmployees[0]} o numerze ID: {idUserCode}.").Show();
                        return;
                    }
                    else if(potentialEmployees.Count > 1)
                    {
                        DialogBoxFactory.GetInfoBox("Wykryto poważny błąd bazy Danych: Duplikacja numeru identyfikacyjnego pracownika. Skontaktuj się ze swoim Administratorem.").Show();
                        return;
                    }
                    else
                    {
                        RegisteredEmployee.RegisterNewEmployee(potentialEmployees[0], DB_RequestEmployeesOwnedKeys(potentialEmployees[0]));
                    }
                    break;
            }

            RaisePropertyChangedEvent(nameof(RegisteredEmployee));

        }
        protected virtual void ClearRegistration()
        {
            RegisteredEmployee.Reset();
            RaisePropertyChangedEvent(nameof(RegisteredEmployee));
        }
        protected virtual void HandoverTheKey()
        {
            string requestedKeyId = UI_RequestId("Proszę podać numer klucza:");

            if (requestedKeyId == null)
            {
                DialogBoxFactory.GetInfoBox("Procedura anulowana!").Show();
                return;
            }

            RoomKey searchedKey = _dataQueryProvider.GetRoomKeyByIdAsync(requestedKeyId).Result;

            if (searchedKey == null)
            {
                DialogBoxFactory.GetInfoBox("Klucz nie został odnaleziony w Bazie.").Show();
                return;
            }
            else if(searchedKey.AssignedEmployee_Id != null)
            {
                DialogBoxFactory.GetInfoBox("Klucz został wydany innemu pracownikowi.").Show();
                return;
            }
            else
            {
                _dataCommandProvider.HandOverTheRoomKeyToEmployeeAsync(searchedKey,RegisteredEmployee.GetRegisteredEmployee()).Wait();
                DialogBoxFactory.GetInfoBox("Wydano klucz!").Show();
            }
            RegisteredEmployee.RefreshHeldKeys(DB_RequestEmployeesOwnedKeys(RegisteredEmployee.GetRegisteredEmployee()));
            RaisePropertyChangedEvent(nameof(RegisteredEmployee));
        }
        protected virtual void TakeTheKeyBack()
        {
            string requestedKeyId = UI_RequestId("Proszę podać numer klucza:");

            if (requestedKeyId == null)
            {
                DialogBoxFactory.GetInfoBox("Procedura anulowana!").Show();
                return;
            }

            List<RoomKey> searchedKeys = RegisteredEmployee.HeldKeys.Where(hk => hk.RoomKey_Id == requestedKeyId).ToList();

            if (searchedKeys.Count == 0)
            {
                DialogBoxFactory.GetInfoBox($"Pracownik nie jest uprawniony do zwrotu klucza nr:\n{requestedKeyId}").Show();
                return;
            }
            else if (searchedKeys.Count == 1)
            {
                _dataCommandProvider.TakeTheRoomKeyAsync(searchedKeys[0]).Wait();
                DialogBoxFactory.GetInfoBox("Przyjęto klucz!").Show();
            }
            else
            {
                DialogBoxFactory.GetInfoBox("Wykryto poważny błąd bazy Danych: Duplikacja numeru identyfikacyjnego klucza. Skontaktuj się ze swoim Administratorem.").Show();
                return;
            }
            RegisteredEmployee.RefreshHeldKeys(DB_RequestEmployeesOwnedKeys(RegisteredEmployee.GetRegisteredEmployee()));
            RaisePropertyChangedEvent(nameof(RegisteredEmployee));
        }
        protected virtual bool CanExecute_HandOverTheKey()
        {
            return RegisteredEmployee.IsRegistrationValid;
        }
        protected virtual bool CanExecute_TakeTheKeyBack()
        {
            bool condition1 = RegisteredEmployee.IsRegistrationValid;
            bool condition2 = RegisteredEmployee.HeldKeys.Count > 0;

            return condition1 && condition2;
        }
        protected virtual bool CanExecute_ClearRegistration()
        {
            return RegisteredEmployee.IsRegistrationValid;
        }

        // Retrieving initial informations from employee:
        private IEnumerable<Employee> UI_RequestEmployeeNames()
        {
            DialogBoxFactory dialogFactory = new DialogBoxFactory();
            RequestDialogBox dialogBox;
            string name, surname;

            IEnumerable<Employee> employees;

            dialogFactory.HeaderMessage = "Proszę podać Imię i Nazwisko:";
            dialogFactory.RequestedParameters = new List<string>() { "Imię", "Nazwisko" };
            dialogFactory.DefaultValuesForRequestedParameters = new List<string>();
            dialogFactory.CorrespondingRules = new List<ValidationRules>();

            dialogBox = dialogFactory.GetRequestDialogBox();
            if ((bool)dialogBox.ShowDialog())
            {
                name = dialogBox[0];
                surname = dialogBox[1];
            }
            else return null;

            return employees = _dataQueryProvider.GetEmployeesByNamesAsync(name, surname).Result;
        }
        private string UI_RequestId(string message)
        {
            DialogBoxFactory dialogFactory = new DialogBoxFactory();
            RequestDialogBox dialogBox;
            string id;

            dialogFactory.HeaderMessage = message;
            dialogFactory.RequestedParameters = new List<string>() { "Numer Identyfikacyjny" };
            dialogFactory.DefaultValuesForRequestedParameters = new List<string>();
            dialogFactory.CorrespondingRules = new List<ValidationRules> { ValidationRules.StringTyped4DigitCode };

            dialogBox = dialogFactory.GetRequestDialogBox();
            if ((bool)dialogBox.ShowDialog())
            {
                id = dialogBox[0];
            }
            else return null;

            return id;
        }

        private IEnumerable<RoomKey> DB_RequestEmployeesOwnedKeys(Employee employee)
        {
            return _dataQueryProvider.GetAllRoomKeysOwnedByEmployeeAsync(employee).Result;
        }


    }
}
