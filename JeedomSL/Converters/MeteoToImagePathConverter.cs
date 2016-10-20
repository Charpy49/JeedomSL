using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace JeedomSL.Converters
{
    public class MeteoToImagePathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var status = ((String)value).ToLower();
            var path = String.Empty;

            if (status.Contains("orage") || status.Contains("storm"))
            {
                return @"\Assets\Weather\storm-128.png";
            }
            else if (status.Contains("brouillard") || status.Contains("brumeux") || status.Contains("fog"))
            {
                return @"\Assets\Weather\fog-day-128.png";
            }
            else if (status.Contains("pluie") || status.Contains("pluvieux") || status.Contains("rain"))
            {
                return @"\Assets\Weather\rain-128.png";
            }
            else if (status.Contains("averse") || status.Contains("shower"))
            {
                return @"\Assets\Weather\little-rain-128.png";
            }
            else if (status.Contains("nuage") || status.Contains("cloud"))
            {
                return @"\Assets\Weather\partly-cloudy-day-128.png";
            }
            else if (status.Contains("neige") || status.Contains("snow"))
            {
                return @"\Assets\Weather\snow-128.png";
            }
            else if (status.Contains("soleil") || status.Contains("beau") || status.Contains("fair") || status.Contains("sun") || status.Contains("dégagé") || status.Contains("clear"))
            {
                return @"\Assets\Weather\sun-2-128.png";
            }


            return path;



        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class AlarmToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var status = ((String)value).ToLower();
            var path = String.Empty;

            if (status.Equals("0"))
            {
                return @"\Assets\Alarm\padlock-5-128.png";
            }
            else if (status.Equals("1"))
            {
                return @"\Assets\Alarm\padlock-7-128.png";
            }
            else return @"\Assets\Alarm\padlock-5-128.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class StatusToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var status = ((String)value).ToLower();
            var path = String.Empty;

            if (status.Equals("0"))
            {
                return @"\Assets\appbar.cancel.png";
            }
            else if (status.Equals("1"))
            {
                return @"\Assets\appbar.check.png";
            }
            else return @"\Assets\appbar.cancel.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class NumToIconConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return "";
            var result = "";
            var count = ((string)value).ToLower();
            switch (count)
            {
                case "1": result = "\u2776"; break;
                case "2": result = "\u2777"; break;
                case "3": result = "\u2778"; break;
                case "4": result = "\u2779"; break;
                case "5": result = "\u277A"; break;
                case "6": result = "\u277B"; break;
                case "7": result = "\u277C"; break;
                case "8": result = "\u277D"; break;
                case "9": result = "\u277E"; break;
                case "10": result = "\u277F"; break;
                case "11": result = "\u24EB"; break;
                case "12": result = "\u24EC"; break;
                case "13": result = "\u24ED"; break;
                case "14": result = "\u24EE"; break;
                case "15": result = "\u24EF"; break;
                case "16": result = "\u24F0"; break;
                case "17": result = "\u24F1"; break;
                case "18": result = "\u24F2"; break;
                case "19": result = "\u24F3"; break;
                case "20": result = "\u24F4"; break;
            }
            return result;

        }



        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";
        }

    }
}
