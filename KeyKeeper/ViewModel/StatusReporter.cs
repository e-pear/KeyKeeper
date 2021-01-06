using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace KeyKeeper.ViewModel
{
    /// <summary>
    /// Little object governing status bar at the bottom of main window. Provides a second channel for passing app informations to user.
    /// </summary>
    public class StatusReporter : INotifyPropertyChanged
    {
        private string _status;
        private Timer _timer;
        public string Status { get { return _status; } } // this one is binded to textblock.text property in statusbar control of main window
        
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
        
        public void ReportStatus(string statusMessage, int timeDuration_Seconds = 10) // updates/displays status message
        {
            _status = statusMessage;
            RaisePropertyChangedEvent(nameof(Status));
            TimerOn(timeDuration_Seconds * 1000); // starts displaying information in status bar for defined duration of time

        }
        private void TimerOn(int timeInterval)
        {
            _timer.Interval = timeInterval;
            _timer.Enabled = true;
        }
        private void OnTimedEvent(object source, ElapsedEventArgs e) // ends displaying information after defined time interval
        {
            _status = "";
            RaisePropertyChangedEvent(nameof(Status));
            _timer.Stop();
        }
    }
}
