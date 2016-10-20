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
using Newtonsoft.Json.Linq;
using System.Net.Http;
using JeedomSL.Tools;

namespace JeedomSL
{
    public partial class PlugginPage : PhoneApplicationPage
    {
       
        public PlugginPage()
        {
            InitializeComponent();

            PlugginPivot.DataContext = App.ViewModel.Items.result;
        }

            

    }
}