using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KeyKeeper.Dialogs
{
    /// <summary>
    /// No worry this one is classic. No fancy runtime render here :), MVVM pattern either...
    /// Interaction logic for InfoBox.xaml
    /// </summary>
    public partial class InfoBox : Window
    {
        public InfoBox(string info)
        {
            InitializeComponent();
            this.Owner = Application.Current.MainWindow;
            this.InfoText.Text = info;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
