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
    /// <summary>
    /// The view model class for "CareTakerView". It implements main application goal: room key managing.
    /// </summary>
    public class CareTakerViewModel : Tab
    {
        //Data provider/commander objects abstractions:
        IDataModelCommandOperations _dataCommandProvider;
        IDataModelQueryOperations _dataQueryProvider;
        
        //Presented Employee:
        public PresentedRegisteredEmployee RegisteredEmployee { get; }

        //Commands:
        public ICommand RegisterEmployeeCommand { get; }
        public ICommand HandoverTheKeyCommand { get; }
        public ICommand TakeTheKeyBackCommand { get; }
        public ICommand ResetRegistrationCommand { get; }

        //Constructor:
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
        //Register employee algorithm:
        protected virtual void RegisterEmployee()
        {
            int numberOfPotentiallyRequestedEmployees;
            string idUserCode;
            List<Employee> potentialEmployees;
            IEnumerable<Employee> employees = UI_RequestEmployeeNames(); // requesting name and surname parameters from user

            if (employees == null) // Triggers when user cancels providing requested informations.
            {
                DialogBoxFactory.GetInfoBox("Rejestracja anulowana!").Show();
                return;
            }

            numberOfPotentiallyRequestedEmployees = employees.Count();

            switch ((numberOfPotentiallyRequestedEmployees))
            {
                case 0: // Triggers when requested employee wasn't found.
                    
                    DialogBoxFactory.GetInfoBox("Rejestracja anulowana!\n\nPracownik nie istnieje w Bazie.").Show();
                    break;
                
                case 1: // Triggers when exactly one record was found.

                    RegisteredEmployee.RegisterNewEmployee(employees.First(), DB_RequestEmployeesOwnedKeys(employees.First()));
                    break;
                
                default: // Triggers when there are mor than one matching record - 2nd step verification.
                    
                    idUserCode = UI_RequestId("Proszę podać numer identyfikacyjny Pracownika:"); // requesting an id parameter
                    if(idUserCode == null) // User resigned from providing employee id.
                    {
                        DialogBoxFactory.GetInfoBox("Rejestracja anulowana!").Show();
                        return;
                    }

                    potentialEmployees = employees.Where(e => e.Employee_Id == idUserCode).ToList();

                    if (potentialEmployees.Count == 0) // User provided wrong employee id.
                    {
                        DialogBoxFactory.GetInfoBox($"Nie znaleziono pracownika: {potentialEmployees[0].Name} {potentialEmployees[0]} o numerze ID: {idUserCode}.").Show();
                        return;
                    }
                    else if(potentialEmployees.Count > 1) // That's a scary situation. Somehow 2 identical "unique" ids was found. Well... that should not happend :) Unless someone do something bad in data base.
                    {
                        DialogBoxFactory.GetInfoBox("Wykryto poważny błąd bazy Danych: Duplikacja numeru identyfikacyjnego pracownika. Skontaktuj się ze swoim Administratorem.").Show();
                        return;
                    }
                    else // User provided a correct Id number.
                    {
                        RegisteredEmployee.RegisterNewEmployee(potentialEmployees[0], DB_RequestEmployeesOwnedKeys(potentialEmployees[0]));
                    }
                    break;
            }

            RaisePropertyChangedEvent(nameof(RegisteredEmployee)); // Informing the view.

        }
        // It does what it's name describes :)
        protected virtual void ClearRegistration()
        {
            RegisteredEmployee.Reset();
            RaisePropertyChangedEvent(nameof(RegisteredEmployee));
        }
        // Handover room key procedure algorithm:
        protected virtual void HandoverTheKey()
        {
            RoomKey searchedKey;
            string requestedKeyId = UI_RequestId("Proszę podać numer klucza:"); // requesting an id parameter

            if (requestedKeyId == null) // Triggers when user cancels providing requested informations.
            {
                DialogBoxFactory.GetInfoBox("Procedura anulowana!").Show();
                return;
            }
            
            try // potential exception handler
            {
                searchedKey = _dataQueryProvider.GetRoomKeyByIdAsync(requestedKeyId).Result;
            }
            catch (Exception e)
            {
                SendTabNotification(new TabNotificationSentEventArgs() { Message = e.Message });
                return;
            }
            
            if (searchedKey == null) // Triggers when user provided wrong key id.
            {
                DialogBoxFactory.GetInfoBox("Klucz nie został odnaleziony w Bazie.").Show();
                return;
            }
            else if(searchedKey.AssignedEmployee_Id != null) // Triggers when user provided id of key which was already handovered.
            {
                DialogBoxFactory.GetInfoBox("Klucz został wydany innemu pracownikowi.").Show();
                return;
            }
            else // Triggers when user provided correct key id and key was found on gatehouse.
            {
                try // potential exception handler
                {
                    _dataCommandProvider.HandOverTheRoomKeyToEmployeeAsync(searchedKey, RegisteredEmployee.GetRegisteredEmployee()).Wait();
                }
                catch (Exception e)
                {
                    SendTabNotification(new TabNotificationSentEventArgs() { Message = e.Message });
                    return;
                }
                DialogBoxFactory.GetInfoBox("Wydano klucz!").Show(); // Handover key operation confirmation.
            }
            RegisteredEmployee.RefreshHeldKeys(DB_RequestEmployeesOwnedKeys(RegisteredEmployee.GetRegisteredEmployee())); // Refresh information about taken keys by registered employee.
            RaisePropertyChangedEvent(nameof(RegisteredEmployee)); // Informing the view.
        }
        // Retrieving key algorithm:
        protected virtual void TakeTheKeyBack()
        {
            string requestedKeyId = UI_RequestId("Proszę podać numer klucza:"); // Requesting the retrieved key id number.

            if (requestedKeyId == null) // Triggers when user cancels providing requested informations.
            {
                DialogBoxFactory.GetInfoBox("Procedura anulowana!").Show();
                return;
            }

            List<RoomKey> searchedKeys = RegisteredEmployee.HeldKeys.Where(hk => hk.RoomKey_Id == requestedKeyId).ToList();

            if (searchedKeys.Count == 0) // Situation where provided key wasnt previously handovered to registered employee OR id doesn't exist in database. Any way the registered employee isn't allowed to return provided key.
            {
                DialogBoxFactory.GetInfoBox($"Pracownik nie jest uprawniony do zwrotu klucza nr:\n{requestedKeyId}").Show();
                return;
            }
            else if (searchedKeys.Count == 1) // Everything it's ok and employee may return key.
            {
                try // potential exception handler
                {
                    _dataCommandProvider.TakeTheRoomKeyAsync(searchedKeys[0]).Wait();
                }
                catch (Exception e)
                {
                    SendTabNotification(new TabNotificationSentEventArgs() { Message = e.Message });
                    return;
                }
                DialogBoxFactory.GetInfoBox("Przyjęto klucz!").Show(); // Returning key fact confirmation.
            }
            else // That's a scary situation. Somehow 2 identical "unique" ids was found. Well... that should not happend :) Better call dBase Admin.
            {
                DialogBoxFactory.GetInfoBox("Wykryto poważny błąd bazy Danych: Duplikacja numeru identyfikacyjnego klucza. Skontaktuj się ze swoim Administratorem.").Show();
                return;
            }
            RegisteredEmployee.RefreshHeldKeys(DB_RequestEmployeesOwnedKeys(RegisteredEmployee.GetRegisteredEmployee())); // Refresh information about taken keys by registered employee.
            RaisePropertyChangedEvent(nameof(RegisteredEmployee)); // Informing the view.
        }
        // Methods indicating commands can either be or not to be executed. Well all but registration employee command. Employee always can be registrated on gatehouse.
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

        // Retrieving initial informations from user.
        private IEnumerable<Employee> UI_RequestEmployeeNames()
        {
            DialogBoxFactory dialogFactory = new DialogBoxFactory();
            RequestDialogBox dialogBox;
            string name, surname;

            IEnumerable<Employee> employees;

            // dialog box building part:
            dialogFactory.HeaderMessage = "Proszę podać Imię i Nazwisko:";
            dialogFactory.RequestedParameters = new List<string>() { "Imię", "Nazwisko" };
            dialogFactory.DefaultValuesForRequestedParameters = new List<string>();
            dialogFactory.CorrespondingRules = new List<ValidationRules>();

            dialogBox = dialogFactory.GetRequestDialogBox();
            // dialog handling:
            if ((bool)dialogBox.ShowDialog())
            {
                name = dialogBox[0];
                surname = dialogBox[1];
            }
            else return null;
            // data access part:
            try
            {
                return employees = _dataQueryProvider.GetEmployeesByNamesAsync(name, surname).Result;
            }
            catch (Exception e)
            {
                SendTabNotification(new TabNotificationSentEventArgs() { Message = e.Message });
                return new List<Employee>();
            }
        }
        // Retreieving id information from user.
        private string UI_RequestId(string message)
        {
            DialogBoxFactory dialogFactory = new DialogBoxFactory();
            RequestDialogBox dialogBox;
            string id;
            // dialog box building part:
            dialogFactory.HeaderMessage = message;
            dialogFactory.RequestedParameters = new List<string>() { "Numer Identyfikacyjny" };
            dialogFactory.DefaultValuesForRequestedParameters = new List<string>();
            dialogFactory.CorrespondingRules = new List<ValidationRules> { ValidationRules.StringTyped4DigitCode };

            dialogBox = dialogFactory.GetRequestDialogBox();
            // dialog handling:
            if ((bool)dialogBox.ShowDialog())
            {
                id = dialogBox[0];
            }
            else return null;
            
            return id;
        }
        // Query wrapper:
        private IEnumerable<RoomKey> DB_RequestEmployeesOwnedKeys(Employee employee)
        {
            try // potential exception handler
            {
                return _dataQueryProvider.GetAllRoomKeysOwnedByEmployeeAsync(employee).Result;
            }
            catch (Exception e)
            {
                SendTabNotification(new TabNotificationSentEventArgs() { Message = e.Message });
                return new List<RoomKey>();
            } 
        }

    }
}
