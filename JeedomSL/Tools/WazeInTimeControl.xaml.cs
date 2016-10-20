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
    public partial class WazeInTimeControl : UserControl
    {
        public WazeInTimeControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if(Tag != null)
            {
                Eqlogic el = Tag as Eqlogic;
                StartHour.Text = el.cmds.ToString("lastrefresh");
                StartTime.Text = el.cmds.ToString("time1");
                TrajetDepart.Text = "Via: "+el.cmds.ToString("routename1");

                ReturnHour.Text = el.cmds.ToString("lastrefreshret");
                ReturnTime.Text = el.cmds.ToString("timeret1");
                TrajetRetour.Text = "Via: " + el.cmds.ToString("routeretname1");

            }
        }
    }
}
