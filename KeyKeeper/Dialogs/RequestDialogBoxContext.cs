using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Dialogs
{
    public class RequestDialogBoxContext
    {
        public StringParameter StringParameter { get; } = new StringParameter();
    }

    public class StringParameter : INotifyCollectionChanged
    {
        private ObservableCollection<string> parameters;
        
        public string this[int index]
        {
            get 
            {
                if (parameters.Count > index) return parameters[index]; 
                else return null; 
            }
            set
            {
                if (parameters.Count > index) parameters[index] = value;
                else parameters.Insert(index, value);
            }
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public StringParameter()
        {
            parameters = new ObservableCollection<string>();
            parameters.CollectionChanged += (s,e) => CollectionChanged?.Invoke(s, e);
        }
    }
}
