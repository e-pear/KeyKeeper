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
    /// Tab control interface. Provides interactions with tabs (borrowed and heavily refactored from my other project: SSTC). 
    /// </summary>
    public interface ITab
    {
        string TabTitle { get; }
        ICommand CloseCommand { get; }

        event EventHandler CloseRequested;
        event EventHandler TabNotificationSent;
        void TabRefresh();
    }
}
