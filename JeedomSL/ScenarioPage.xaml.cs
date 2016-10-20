using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using JeedomSL.Tools.Scenario;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using JeedomSL.Tools;

namespace JeedomSL
{
    public partial class ScenarioPage : PhoneApplicationPage
    {
       
        public ScenarioPage()
        {
            InitializeComponent();

            this.DataContext = App.Scenario.Items.result;
        }

        private void StackPanel_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private async void Scenario_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
 
            if (sender == null) return;
            string id = ((sender as StackPanel).Tag as Result).id;
            string result = await Outils.InvokeMethod("scenario::changeState", id, false,"state", "run");
        }
    
    }
}