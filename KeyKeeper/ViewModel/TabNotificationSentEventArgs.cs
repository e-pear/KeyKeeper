using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.ViewModel
{
    /// <summary>
    /// Container for tab notivication events messages.
    /// </summary>
    class TabNotificationSentEventArgs : EventArgs
    {
        public string Message { get; set; }
    }
}
