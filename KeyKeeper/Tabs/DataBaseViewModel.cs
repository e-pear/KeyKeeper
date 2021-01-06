using KeyKeeper.Dialogs;
using KeyKeeper.Model;
using KeyKeeper.Model.DataOperations;
using KeyKeeper.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KeyKeeper.Tabs
{
    /// <summary>
    /// Main abstraction object for CRUD view models. Provides and does all for them :).
    /// </summary>
    /// <typeparam name="T">Record representing class.</typeparam>
    public abstract class DataBaseViewModel<T> : Tab where T : class, IRecord
    {
        // data provider:
        IRepositoryManagerOperations<T> _dataProvider;

        // base properties:
        protected ObservableCollection<T> _records;
        protected T _selectedRecord;
        protected IPresentedRecordOf<T> _presentedRecord;
        public ObservableCollection<T> Records { get { return _records; } }
        public T SelectedRecord
        {
            get { return _selectedRecord; }
            set
            {
                _selectedRecord = value;
                PresentedRecord.SynchronizePresentedRecordWith(value);
                RaisePropertyChangedEvent(nameof(SelectedRecord));
                RaisePropertyChangedEvent(nameof(PresentedRecord));
            }
        }
        public IPresentedRecordOf<T> PresentedRecord { get { return _presentedRecord; } }
        // base CRUD commands:
        public ICommand GenerateAvailableIdCommand { get; }
        public ICommand SaveRecordCommand { get; }
        public ICommand UpdateRecordCommand { get; }
        public ICommand DeleteRecordCommand { get; }
        // base constructor:
        public DataBaseViewModel(IRepositoryManagerOperations<T> dataProvider, string tabTitle) : base(tabTitle)
        {
            _dataProvider = dataProvider;
            _records = new ObservableCollection<T>();

            GenerateAvailableIdCommand = new CommandRelay(GenerateAvailableId, CanExecute_GenerateAvailableIdCommand);
            SaveRecordCommand = new CommandRelay(SaveRecord, CanExecute_SaveRecordCommand);
            UpdateRecordCommand = new CommandRelay(UpdateRecord, CanExecute_UpdateCommand);
            DeleteRecordCommand = new CommandRelay(DeleteRecord, CanExecute_DeleteCommand);

            RefreshKeysCollection();
        }
        // method refreshing presented records collection after every command change:
        protected virtual void RefreshKeysCollection()
        {
            IEnumerable<T> records;

            try
            {
                records = _dataProvider.GetAllRecordsAsync().Result;
            }
            catch (Exception e)
            {
                SendTabNotification(new TabNotificationSentEventArgs() { Message = e.Message });
                records = new List<T>();
            }

            _records = new ObservableCollection<T>(records);
            RaisePropertyChangedEvent(nameof(Records));
        }
        // saves record to database:
        protected virtual void SaveRecord()
        {
            try
            {
                _dataProvider.AddAsync(PresentedRecord.GetRecordFromPresentedRecord()).Wait();
                RefreshKeysCollection();
            }
            catch (Exception e)
            {
                SendTabNotification(new TabNotificationSentEventArgs() { Message = e.Message });
                return;
            }
        }
        // updates selected record in database:
        protected virtual void UpdateRecord()
        {
            try
            {
                _dataProvider.UpdateRecordAsync(PresentedRecord.GetRecordFromPresentedRecord()).Wait();
                RefreshKeysCollection();
            }
            catch (Exception e)
            {
                SendTabNotification(new TabNotificationSentEventArgs() { Message = e.Message });
                return;
            }
        }
        // removes selected record from database:
        protected virtual void DeleteRecord()
        {
            try
            {
                _dataProvider.RemoveAsync(SelectedRecord).Wait();
                RefreshKeysCollection();
            }
            catch (Exception e)
            {
                SendTabNotification(new TabNotificationSentEventArgs() { Message = e.Message });
                return;
            }
        }
        // generates first unused id number to use in CRUD tab: 
        protected virtual void GenerateAvailableId()
        {
            PresentedRecord.SetPresentedIdCode(GenerateFirstAvailable4DigitCode());
            RaisePropertyChangedEvent(nameof(PresentedRecord));
        }
        // command execution indicator methods:
        protected virtual bool CanExecute_SaveRecordCommand()
        {
            bool condition1 = _records.Any(record => record.GetIdCode() == PresentedRecord.GetPresentedIdCode());
            bool condition2 = PresentedRecord.IsSaveable();
            return (!condition1 && condition2);
        }
        protected virtual bool CanExecute_UpdateCommand()
        {
            return SelectedRecord != null && !PresentedRecord.IsEqualToSelectedRecord(SelectedRecord) && PresentedRecord.IsSaveable();
        }
        protected virtual bool CanExecute_DeleteCommand()
        {
            return (SelectedRecord != null);
        }
        protected virtual bool CanExecute_GenerateAvailableIdCommand()
        {
            return true;
        }
        // Because of fact that id numbers are considered as unique, and a fact that implemented shallow validation doesn't allow for save/update record with redundant id. This algorithm returns first unused id number to use in CU operations.
        protected virtual string GenerateFirstAvailable4DigitCode()
        {
            List<int> rawIds = _records.Select(r => r.GetIdNumber()).OrderBy(i => i).ToList();
            string code = "EORE";

            int counter = 0;
            
            for (int i = 0; i < 10000; i++)
            {
                if (counter<rawIds.Count && i == rawIds[counter]) counter++;
                else
                {
                    code = i.ConvertToFourDigitStringCode();
                    break;
                }
            }
            DialogBoxFactory.GetInfoBox("Błąd Krytyczny:\nPrzekroczono limit bazy danych. Skontaktuj się ze swoim Administratorem."); // that will happen if you try to overload a data base and save to it 10000 records and then add a one more :)
            return code;
        }
        // Added in case of adding data outside KeyKeeper to data base. I.e. through SMSS. It simply refreshes database every time tab is selected (only CRUD tabs have implemented that ability). 
        public override void TabRefresh()
        {
            RefreshKeysCollection();
        }
    }
}
