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
using JeedomSL.Pluggin;

namespace JeedomSL.Tools
{
    public partial class BulbControle : UserControl
    {


    

        public static readonly DependencyProperty SwitchOnProperty =
       DependencyProperty.Register("SwitchOn", typeof(Color), typeof(BulbControle), new PropertyMetadata(default(Color)));
        public Color SwitchOn
        {
            get { return (Color)GetValue(SwitchOnProperty); }
            set { SetValue(SwitchOnProperty, value); }
        }

        public BulbControle()
        {
            InitializeComponent();
        }

        private void UserControl_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this.Tag == null) return;
            string ID;
            Cmd[] cmd = (this.Tag as Eqlogic).cmds;
            Cmd commande = cmd.GetCmdByName("Etat 1");
            if (commande.state.Equals("1"))
                ID = cmd.GetCmdByName("Off 1").id;
            else
                ID = cmd.GetCmdByName("On 1").id;
            commande.state = commande.state.Equals("1") ? "0" : "1";
            Outils.InvokeMethod("cmd::execCmd", ID);
            UserControl_Loaded(sender, null);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (Tag != null)
            {
                Cmd[] cmd = (Tag as Eqlogic).cmds;
                Cmd commande = cmd.GetCmdByName("Etat 1");
                if (commande.state.Equals("1"))
                {
                    
                     SwitchOn = Colors.White;
                }
                else
                {
                    SwitchOn = Colors.Transparent;
                }
            }
        }
    }
}
