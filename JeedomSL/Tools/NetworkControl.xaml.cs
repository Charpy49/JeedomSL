using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;
using System.Windows.Data;
using JeedomSL.Converters;

namespace JeedomSL.Tools
{
    public partial class NetworkControl : UserControl
    {
        public NetworkControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            JeedomSL.Pluggin.Eqlogic myObj = null;
            if (Tag != null)
            {
                myObj = Tag as JeedomSL.Pluggin.Eqlogic;

                LatencyValue.Text = myObj.cmds.Where(f => f.logicalId.Equals("latency")).FirstOrDefault().state + "ms";
                Binding tmpBinding = new Binding()
                { Converter = new StatusToImagePathConverter() };
                tmpBinding.Source = myObj.cmds.Where(f => f.logicalId.Equals("ping")).FirstOrDefault().state;

                ImageStatus.SetBinding( Image.SourceProperty,tmpBinding);
            }
        }
    }
}
