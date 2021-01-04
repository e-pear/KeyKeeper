using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KeyKeeper.ViewModel
{
    /// <summary>
    /// Base class for all tabs (borrowed and slightly refactored from my other project: SSTC).
    /// </summary>
    public abstract class Tab : ITab, INotifyPropertyChanged
    {
        // BASE PROPERTIES
        public string TabTitle { get; }

        public ICommand CloseCommand { get; }
        // ITAB EVENTS
        public event EventHandler CloseRequested;
        public event EventHandler TabNotificationSent;
        // INOTIFYPROPERTY... EVENTS
        public event PropertyChangedEventHandler PropertyChanged;
        // TAB EVENT RAISERS
        protected void RaisePropertyChangedEvent(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected void SendTabNotification(EventArgs notificationEventArgs)
        {
            TabNotificationSent?.Invoke(this, notificationEventArgs);
        }
        // TAB CONSTRUCTOR
        public Tab(string tabTitle)
        {
            TabTitle = tabTitle;

            CloseCommand = new CommandRelay(CloseTab, () => true);
        }
        // TAB CLOSING COMMAND RELATED METHODS
        private void CloseTab()
        {
            if (ClosingAction()) CloseRequested?.Invoke(this, EventArgs.Empty);
            else return;
        }
        protected virtual bool ClosingAction() { return true; }
        // TAB ALTERING CLOASING OPERATION FUNCTIONALITY
        /// <summary>
        /// Enables conditioning of tab closing process to the inheriting classes.
        /// </summary>
        protected virtual bool CanExecute_ToggleRenamingCommand() { return true; }
        // TAB REFRESH FUNCTIONALITY
        /// <summary>
        /// Method is called every time tab is (re)selected. I may not find a practical use of it in this particular project, but... who knows?
        /// </summary>
        public virtual void TabRefresh() { }
    }
}
