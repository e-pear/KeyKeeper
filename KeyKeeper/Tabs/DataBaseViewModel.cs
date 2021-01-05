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
    public abstract class DataBaseViewModel<T> : Tab where T : class, IRecord
    {
        IRepositoryManagerOperations<T> _dataProvider;

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

        public ICommand GenerateAvailableIdCommand { get; }
        public ICommand SaveRecordCommand { get; }
        public ICommand UpdateRecordCommand { get; }
        public ICommand DeleteRecordCommand { get; }

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

        protected virtual void RefreshKeysCollection()
        {
            IEnumerable<T> records = _dataProvider.GetAllRecordsAsync().Result; 
            _records = new ObservableCollection<T>(records);
            RaisePropertyChangedEvent(nameof(Records));
        }

        protected virtual void SaveRecord()
        {
            _dataProvider.AddAsync(PresentedRecord.GetRecordFromPresentedRecord()).Wait();
            RefreshKeysCollection();
        }
        protected virtual void UpdateRecord()
        {
            _dataProvider.UpdateRecordAsync(PresentedRecord.GetRecordFromPresentedRecord()).Wait();
            RefreshKeysCollection();
        }
        protected virtual void DeleteRecord()
        {
            _dataProvider.RemoveAsync(SelectedRecord).Wait();
            RefreshKeysCollection();
        }
        protected virtual void GenerateAvailableId()
        {
            PresentedRecord.SetPresentedIdCode(GenerateFirstAvailable4DigitCode());
            RaisePropertyChangedEvent(nameof(PresentedRecord));
        }

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
            return code;
        }
        public override void TabRefresh()
        {
            RefreshKeysCollection();
        }
    }
}
