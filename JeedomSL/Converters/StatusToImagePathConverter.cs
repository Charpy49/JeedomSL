using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace JeedomSL.Converters
{
    public class StatusToImagePathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var status = (String)value;
            var path = String.Empty;

            switch (status)
            {
                case "1":
                    path = @"\Assets\appbar.check.png";
                    break;
                
                default: path = @"\Assets\appbar.cancel.png";
                    break;
            }

            return path;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
