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
    /// <summary>
    /// Converter class used to indicate whether key is handovered or stays in gatehouse resources. Work in corresponding datagrid control object.
    /// </summary>
    public class AssignedEmployeeNullToInformationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            RoomKey key = value as RoomKey;
            if (key.AssignedEmployee_Id == null) return "Klucz w zasobach Portierni";
            else return "Klucz wydany Pracownikowi";
        }
        
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) // not needed
        {
            throw new NotImplementedException();
        }
    }
}
