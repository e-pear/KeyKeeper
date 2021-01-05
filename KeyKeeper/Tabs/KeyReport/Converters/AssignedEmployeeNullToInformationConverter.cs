using KeyKeeper.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace KeyKeeper.Tabs.KeyReport.Converters
{
    public class AssignedEmployeeNullToInformationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            RoomKey key = value as RoomKey;
            if (key.AssignedEmployee_Id == null) return "Klucz w zasobach Portierni";
            else return "Klucz wydany Pracownikowi";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
