using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace KeyKeeper.ViewModel
{
    public class StatusReporter : INotifyPropertyChanged
    {
        private string _status;
        private Timer _timer;
        public string Status { get { return _status; } }
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChangedEvent(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        public StatusReporter()
        {
            _timer = new Timer();
            _timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
        }
        
        public void ReportStatus(string statusMessage, int timeDuration_Seconds = 10)
        {
            _status = statusMessage;
            RaisePropertyChangedEvent(nameof(Status));
            TimerOn(timeDuration_Seconds * 1000);

        }
        private void TimerOn(int timeInterval)
        {
            _timer.Interval = timeInterval;
            _timer.Enabled = true;
        }
        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            _status = "";
            RaisePropertyChangedEvent(nameof(Status));
            _timer.Stop();
        }
    }
}
