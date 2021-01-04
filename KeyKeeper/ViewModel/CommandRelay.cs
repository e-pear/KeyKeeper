using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KeyKeeper.ViewModel
{
    // Something like RelayCommand class...
    // Used by ViewModels to prevent multiplying ICommand objects
    public class CommandRelay : ICommand
    {
        private Action action;
        private Action<object> action_p; // that one supports actions with parameters
        private Func<bool> canExecute;

        public CommandRelay(Action action, Func<bool> canExecute)
        {
            this.action = action;
            this.canExecute = canExecute;
        }

        public CommandRelay(Action<object> action, Func<bool> canExecute)
        {
            this.action_p = action;
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public bool CanExecute(object parameter)
        {
            return canExecute();
        }

        public void Execute(object parameter)
        {
            if (action_p != null) action_p(parameter);
            else action();
        }
    }
}
