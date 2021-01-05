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

namespace KeyKeeper.Tabs.RoomKeyBase
{
    public class RoomKeyBaseViewModel : DataBaseViewModel<RoomKey>
    {

        public RoomKeyBaseViewModel(IRepositoryManagerOperations<RoomKey> dataProvider) : base(dataProvider, "Baza Kluczy") 
        {
            _presentedRecord = new PresentedRecordRoomKey(); 
        }
    }
}
