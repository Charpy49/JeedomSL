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
    public partial class ModeControl : UserControl
    {
        public ModeControl()
        {
            InitializeComponent();
        }
        private async void modeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                PhoneGeoloc item = e.AddedItems[0] as PhoneGeoloc;
                await Outils.InvokeMethod("cmd::execCmd", item.ID);


            }
            //e.
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (Tag != null)
            {
                Cmd[] cmds = (Tag as Eqlogic).cmds;

                string currentMode = cmds.First(ff => ff.eqType == "mode" &&
                            ff.display.generic_type == "MODE_STATE").state;



                var dyn = from geoloc in cmds
                       .Where(rr => rr.eqType == "mode" &&
                            rr.display.generic_type == "MODE_SET_STATE" &&
                            rr.isVisible == "1").ToList()
                              //.Where(f => f.configuration.mode.Equals("dynamic")).ToList()
                          select new PhoneGeoloc { ID = geoloc.id, Name = geoloc.name };

                modeList.ItemsSource = dyn.ToList();
                  modeList.SelectedItem = modeList.Items.OfType<PhoneGeoloc>().First(r =>r.Name == currentMode);

                modeList.SelectionChanged += modeList_SelectionChanged;

                /*     // Cmd commande = cmd.GetCmd("enable");
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
                     */
            }
        }

    }
}
