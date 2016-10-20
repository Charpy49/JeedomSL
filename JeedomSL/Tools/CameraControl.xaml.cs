using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using JeedomSL.Pluggin;
using System.Windows.Media.Imaging;

namespace JeedomSL.Tools
{
    public partial class CameraControl : UserControl
    {
        public CameraControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (Tag != null)
            {
                Configuration conf = (Tag as Eqlogic).configuration;
                String uri = conf.protocole + "://" + conf.ip +":"+ conf.port + "/" + conf.urlStream;
                ImageControl.Source = new BitmapImage(new Uri(uri, UriKind.Absolute));
            }
        }
    }
}
