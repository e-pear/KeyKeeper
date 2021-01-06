using KeyKeeper.Dialogs;
using KeyKeeper.Tabs.CareTaker;
using KeyKeeper.Tabs.EmployeeBase;
using KeyKeeper.Tabs.KeyReport;
using KeyKeeper.Tabs.RoomKeyBase;
using KeyKeeper.Model.DataModel;
using KeyKeeper.Model.DataOperations;
using KeyKeeper.Model;
using KeyKeeper.Settings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KeyKeeper.ViewModel
{
    /// <summary>
    /// Main view model class.
    /// </summary>
    public class ViewModel : INotifyPropertyChanged
    {
        // Status Reporter:
        private StatusReporter _statusReporter;
        public string Status { get { return _statusReporter.Status; } }
        // Tab properties with backing fields:
        private readonly ObservableCollection<ITab> _tabs;
        private ITab _selectedTab;
        public ObservableCollection<ITab> Tabs { get { return _tabs; } }
        public ITab SelectedTab
        {
            get { return _selectedTab; }
            set 
            {
                _selectedTab = value;
                if (value != null) _selectedTab.TabRefresh();
                RaisePropertyChangedEvent(nameof(SelectedTab));

            }
        }

        // Main Menu Commands:
        public ICommand OpenRoomKeysManagerCommand { get; }
        public ICommand OpenRoomKeyReportCommand { get; }
        public ICommand OpenEmployeeDataBaseManagerCommand { get; }
        public ICommand OpenRoomKeyDataBaseManagerCommand { get; }
        public ICommand ConfigureConnectionSettingsCommand { get; }

        // Standard events
        public event PropertyChangedEventHandler PropertyChanged;

        // Standard event raisers:
        protected void RaisePropertyChangedEvent(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Constructor
        public ViewModel()
        {
            _statusReporter = new StatusReporter();
            _statusReporter.PropertyChanged += OnStatusInformerStateChanged;
            
            _tabs = new ObservableCollection<ITab>();
            _tabs.CollectionChanged += OnTabsCollectionChanged;

            OpenRoomKeysManagerCommand = new CommandRelay(OpenRoomKeyManagerTab, () => true);
            OpenRoomKeyReportCommand = new CommandRelay(OpenRoomKeyReportTab, () => true);
            OpenEmployeeDataBaseManagerCommand = new CommandRelay(OpenEmployeeBaseManagerTab, () => true);
            OpenRoomKeyDataBaseManagerCommand = new CommandRelay(OpenRoomKeyBaseManagerTab, () => true);
            ConfigureConnectionSettingsCommand = new CommandRelay(ConfigureConnection, () => true);
        }

        // Event Actions:
        protected virtual void OnTabsCollectionChanged( object sender, NotifyCollectionChangedEventArgs e)
        {
            ITab tab;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    tab = (ITab)e.NewItems[0];
                    tab.CloseRequested += OnTabCloseRequested;
                    tab.TabNotificationSent += OnTabNotificationSent;
                    break;
                case NotifyCollectionChangedAction.Remove:
                    tab = (ITab)e.OldItems[0];
                    tab.CloseRequested -= OnTabCloseRequested;
                    tab.TabNotificationSent -= OnTabNotificationSent;
                    break;
            }
        }
        protected virtual void OnTabCloseRequested(object sender, EventArgs e)
        {
            Tabs.Remove((ITab)sender);
        }
        protected virtual void OnTabNotificationSent(object sender, EventArgs e)
        {
            TabNotificationSentEventArgs args = e as TabNotificationSentEventArgs;
            _statusReporter.ReportStatus(args.Message);
        }
        protected virtual void OnStatusInformerStateChanged(object sender, EventArgs e)
        {
            RaisePropertyChangedEvent(nameof(Status));
        }
        // Command methods: (here the concrete data access objects are injected)
        protected virtual void OpenRoomKeyManagerTab() // wrapper
        {
            OpenTab(new CareTakerViewModel(new DataModelCommandOperations(), new DataModelQueryOperations()));
        }
        protected virtual void OpenRoomKeyReportTab() // wrapper
        {
            OpenTab(new ReportViewModel(new DataModelQueryOperations()));
        }
        protected virtual void OpenEmployeeBaseManagerTab() // wrapper
        {
            OpenTab(new EmployeeBaseViewModel(new RecordRepositoryManagerOperations<Employee>()));
        }
        protected virtual void OpenRoomKeyBaseManagerTab() // wrapper
        {
            OpenTab(new RoomKeyBaseViewModel(new RecordRepositoryManagerOperations<RoomKey>()));
        }
        protected virtual void OpenTab(ITab newTab) // method allows only one tab of particular type at time
        {
            var redundantTabs = Tabs.Where(tab => tab.GetType() == newTab.GetType());

            if(redundantTabs.Count() == 0)
            {
                Tabs.Add(newTab);
                SelectedTab = Tabs.Last();
            }
            else
            {
                SelectedTab = Tabs[Tabs.IndexOf(redundantTabs.First())];
            }
        }
        protected virtual void ConfigureConnection() // Method building a request dialog box, with presented actual connection string parameters, and allows for their independent change. 
        {
            string currentConnectionString = ConnectionSetting.GetCurrentConnectionString();
            string[] connectionStringRawParams = currentConnectionString.Split(';');
            List<string> connectionStringParams = new List<string>();

            foreach(string rawParam in connectionStringRawParams)
            {
                string param = rawParam.Substring(rawParam.IndexOf('=') + 1);
                connectionStringParams.Add(param);
            }

            DialogBoxFactory dialogBoxFactory = new DialogBoxFactory();

            dialogBoxFactory.HeaderMessage = "Ustawienia połączenia z bazą danych:";
            dialogBoxFactory.RequestedParameters = new List<string>() { "Nazwa/Adres Serwera", "Nazwa Bazy Danych", "Login", "Hasło" };
            dialogBoxFactory.DefaultValuesForRequestedParameters = connectionStringParams;
            dialogBoxFactory.CorrespondingRules = new List<ValidationRules>();

            RequestDialogBox dialogBox = dialogBoxFactory.GetRequestDialogBox();
            
            if((bool)dialogBox.ShowDialog())
            {
                ConnectionSetting.SetNewConnectionString(dialogBox[0], dialogBox[1], dialogBox[2], dialogBox[3]);
                if (ConnectionSetting.TestDatabaseExistance()) ReportStatus("Konfiguracja połączenia zakończona sukcesem.");
                else ReportStatus("Błąd konfiguracji. Nie odnaleziono bazy danych.");
            }
            else ReportStatus("Konfiguracja połączenia anulowana.");
        }
        protected virtual void ReportStatus(string message) // Passing messages to status reporter object.
        {
            _statusReporter.ReportStatus(message);
        }
    }
}
