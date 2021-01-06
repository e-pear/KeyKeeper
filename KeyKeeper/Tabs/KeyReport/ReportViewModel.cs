using KeyKeeper.Model.DataOperations;
using KeyKeeper.Model;
using KeyKeeper.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using KeyKeeper.Dialogs;

namespace KeyKeeper.Tabs.KeyReport
{
    public class ReportViewModel : Tab
    {
        IDataModelQueryOperations _queryProvider;
        // Tab properties with backing fields:
        private ObservableCollection<RoomKey> _keys;
        private RoomKey _selectedKey;
        public ObservableCollection<RoomKey> Keys { get { return _keys; } }
        public RoomKey SelectedKey 
        {
            get { return _selectedKey; }
            set 
            { 
                _selectedKey = value; 
                RaisePropertyChangedEvent(nameof(SelectedKey));

                if (SelectedKey != null && SelectedKey.AssignedEmployee_Id != null)
                {
                    try
                    {
                        Detail.SetAsEmployeeDetail(_queryProvider.GetEmployeeByOwnedKeyAsync(SelectedKey).Result);
                        RaisePropertyChangedEvent(nameof(Detail));
                    }
                    catch (Exception e)
                    {
                        SendTabNotification(new TabNotificationSentEventArgs() { Message = e.Message });
                    }
                }
                else
                {
                    Detail.SetAsGatehouseDetail();
                    RaisePropertyChangedEvent(nameof(Detail));
                }
            
            }
        }
        
        public PresentedDetail Detail { get; }

        public ICommand ShowAllRoomKeysCommand { get; }
        public ICommand ShowHandoveredRoomKeysCommand { get; }
        public ICommand ShowRemainingRoomKeysCommand { get; }
        public ICommand ShowSpecifiedKeyCommand { get; }


        public ReportViewModel(IDataModelQueryOperations queryProvider) : base("Raport o stanie kluczy")
        {
            _queryProvider = queryProvider;

            Detail = new PresentedDetail();

            ShowAllRoomKeysCommand = new CommandRelay(ShowAllRoomKeys, () => true);
            ShowHandoveredRoomKeysCommand = new CommandRelay(ShowHandoveredRoomKeys, () => true);
            ShowRemainingRoomKeysCommand = new CommandRelay(ShowRemainingRoomKeys, () => true);
            ShowSpecifiedKeyCommand = new CommandRelay(ShowSpecifiedKey, () => true);

            ResetKeysCollection();
        }
        protected virtual void ResetKeysCollection(IEnumerable<RoomKey> collection)
        {
            _keys = new ObservableCollection<RoomKey>(collection);
            RaisePropertyChangedEvent(nameof(Keys));
        }
        protected virtual void ResetKeysCollection()
        {
            _keys = new ObservableCollection<RoomKey>();
            RaisePropertyChangedEvent(nameof(Keys));
        }
        protected virtual void ResetPresentedDetail()
        {
            Detail.SetAsGatehouseDetail();
            RaisePropertyChangedEvent(nameof(Detail));
        }

        protected virtual void ShowAllRoomKeys()
        {
            IEnumerable<RoomKey> results;
            try
            {
                results = _queryProvider.GetAllRoomKeysAsync().Result;
            }
            catch (Exception e)
            {
                SendTabNotification(new TabNotificationSentEventArgs() { Message = e.Message });
                return;
            }
            if (results.Count() == 0) DialogBoxFactory.GetInfoBox("Kolekcja jest pusta. Proszę skontaktować się z Administratorem bazy danych.").Show();
            ResetKeysCollection(results);
            ResetPresentedDetail();
        }

        protected virtual void ShowHandoveredRoomKeys()
        {
            IEnumerable<RoomKey> results;
            try
            {
                results = _queryProvider.GetHandoveredRoomKeysAsync().Result;
            }
            catch (Exception e)
            {
                SendTabNotification(new TabNotificationSentEventArgs() { Message = e.Message });
                return;
            }
            if (results.Count() == 0) DialogBoxFactory.GetInfoBox("Kolekcja jest pusta. Nie wydano żadnego klucza.").Show();
            ResetKeysCollection(results);
            ResetPresentedDetail();
        }

        protected virtual void ShowRemainingRoomKeys()
        {
            IEnumerable<RoomKey> results;
            try
            {
                results = _queryProvider.GetAvailableRoomKeysAsync().Result;
            }
            catch (Exception e)
            {
                SendTabNotification(new TabNotificationSentEventArgs() { Message = e.Message });
                return;
            }
            if (results.Count() == 0) DialogBoxFactory.GetInfoBox("Kolekcja jest pusta. Wszystkie klucze zostały wydane.").Show();
            ResetKeysCollection(results);
            ResetPresentedDetail();
        }
        protected virtual void ShowSpecifiedKey()
        {
            DialogBoxFactory dialogFactory = new DialogBoxFactory();
            RequestDialogBox dialogBox;
            RoomKey key;
            string id;

            dialogFactory.HeaderMessage = "Proszę podać numer szukanego klucza.";
            dialogFactory.RequestedParameters = new List<string>() { "Numer Identyfikacyjny" };
            dialogFactory.DefaultValuesForRequestedParameters = new List<string>();
            dialogFactory.CorrespondingRules = new List<ValidationRules> { ValidationRules.StringTyped4DigitCode };

            dialogBox = dialogFactory.GetRequestDialogBox();
            if ((bool)dialogBox.ShowDialog())
            {
                id = dialogBox[0];
            }
            else return;

            try
            {
               key = _queryProvider.GetRoomKeyByIdAsync(id).Result;
            }
            catch (Exception e)
            {
                SendTabNotification(new TabNotificationSentEventArgs() { Message = e.Message });
                return;
            }
             
            if (key == null)
            {
                DialogBoxFactory.GetInfoBox($"Brak wyników. Klucz o numerze: {id} nie istnieje w Bazie.").Show();
                ResetKeysCollection();
            }
            else ResetKeysCollection(new List<RoomKey>() { key});
            ResetPresentedDetail();
        }

    }
}
