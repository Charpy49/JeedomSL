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
using System.Windows.Data;
using JeedomSL.Converters;

namespace JeedomSL.Tools
{
    public partial class AlarmControl : UserControl
    {
        public AlarmControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (Tag != null)
            {
                Cmd[] cmd = (Tag as Eqlogic).cmds;
                // Cmd commande = cmd.GetCmd("enable");
                Binding tmpBinding = new Binding()
                { Converter = new AlarmToImageConverter() };
                tmpBinding.Source = cmd.GetCmd("enable").state;

                AlarmStatus.SetBinding(Image.SourceProperty, tmpBinding);

                Binding tmp2Binding = new Binding()
                { Converter = new StatusToImageConverter() };
                tmp2Binding.Source = cmd.GetCmd("immediatState").state;

                ImmediatStatus.SetBinding(Image.SourceProperty, tmp2Binding);

                Binding tmp3Binding = new Binding()
                { Converter = new StatusToImageConverter() };
                tmp3Binding.Source = cmd.GetCmd("state").state;

                StatutStatus.SetBinding(Image.SourceProperty, tmp3Binding);

            }
        }

        private void AlarmStatus_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (Tag != null)
            {
                Cmd[] cmd = (Tag as Eqlogic).cmds;
                // Cmd commande = cmd.GetCmd("enable");
                if (cmd.GetCmd("enable").state.Equals("0"))
                {
                    Outils.InvokeMethod("cmd::execCmd", cmd.GetCmd("armed").id);
                    cmd.GetCmd("enable").state = "1";
                }
                else
                {
                    Outils.InvokeMethod("cmd::execCmd", cmd.GetCmd("released").id);
                    cmd.GetCmd("enable").state = "0";

                }
                UserControl_Loaded(this, null);

            }
        }
    }
}
