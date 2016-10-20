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

namespace JeedomSL.Tools
{
    public partial class StoreControle : UserControl
    {
        public StoreControle()
        {
            InitializeComponent();
        }

        private void UpButton_Click(object sender, RoutedEventArgs e)
        {
            string ID;
            ID = (this.Tag as Eqlogic).cmds.GetCmdByName("Up").id;
            Outils.InvokeMethod("cmd::execCmd", ID);

        }

        private void DownButton_Click(object sender, RoutedEventArgs e)
        {
            string ID;
            ID = (this.Tag as Eqlogic).cmds.GetCmdByName("Down").id;
            Outils.InvokeMethod("cmd::execCmd", ID);

        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            string ID;
            ID = (this.Tag as Eqlogic).cmds.GetCmdByName("Stop").id;
            Outils.InvokeMethod("cmd::execCmd", ID);

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.Tag == null) return;
            Etat.Text = (this.Tag as Eqlogic).cmds.GetCmdByName("Etat").state;
        }
    }
}
