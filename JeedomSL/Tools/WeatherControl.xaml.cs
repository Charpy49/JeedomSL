using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Data;
using JeedomSL.Converters;
using System.Globalization;

namespace JeedomSL.Tools
{
    public partial class WeatherControl : UserControl
    {
        public WeatherControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            JeedomSL.Pluggin.Eqlogic myObj = null;
            if (Tag != null)
            {
                myObj = Tag as JeedomSL.Pluggin.Eqlogic;

                TemperatureValue.Text = myObj.cmds.ToString("temperature");
                HumidityValue.Text = myObj.cmds.ToString("humidity");
                VilleName.Text = myObj.configuration.city_name;
                WeatherValue.Text = myObj.cmds.ToString("condition");
                SunriseValue.Text = DateTime.ParseExact(myObj.cmds.ToString("sunrise").PadLeft(4,'0'),"HHmm", CultureInfo.InvariantCulture).ToString("HH:mm") ;
                SunsetValue.Text = DateTime.ParseExact(myObj.cmds.ToString("sunset").PadLeft(4, '0'), "HHmm", CultureInfo.InvariantCulture).ToString("HH:mm");
                Binding tmpBinding = new Binding()
                { Converter = new MeteoToImagePathConverter() };
                tmpBinding.Source = myObj.cmds.ToString("condition"); 

                MeteoImage.SetBinding(Image.SourceProperty, tmpBinding);
            }
        }
    }
}
