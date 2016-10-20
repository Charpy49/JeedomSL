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
    public partial class HueControle : UserControl
    {
        

        public static readonly DependencyProperty ColorProperty =
       DependencyProperty.Register("ColorHue", typeof(Color), typeof(HueControle), new PropertyMetadata(default(Color)));
        public Color ColorHue
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        public static readonly DependencyProperty SwitchOnProperty =
       DependencyProperty.Register("SwitchOn", typeof(Color), typeof(HueControle), new PropertyMetadata(default(Color)));
        public Color SwitchOn
        {
            get { return (Color)GetValue(SwitchOnProperty); }
            set { SetValue(SwitchOnProperty, value); }
        }

        public static readonly DependencyProperty LuminosityProperty =
      DependencyProperty.Register("Luminosity", typeof(UInt16), typeof(HueControle), new PropertyMetadata(default(UInt16)));
        public UInt16 Luminosity
        {
            get { return (UInt16)GetValue(LuminosityProperty); }
            set { SetValue(LuminosityProperty, value); }
        }

        public HueControle()
        {
            InitializeComponent();
            ColorWheelValue.ColorChangeEvent += ColorWheelValue_ColorChangeEvent;


        }

        private void ColorWheelValue_ColorChangeEvent()
        {
             if (this.Tag == null) return;
            string ID;
            Cmd[] cmd = (this.Tag as Eqlogic).cmds;
            Cmd commande = cmd.GetCmd("color_state");
            ID = cmd.GetCmd("color").id;

            commande.state = "#" + ColorWheelValue.ColorHue.R.ToString("X2") + ColorWheelValue.ColorHue.G.ToString("X2") + ColorWheelValue.ColorHue.B.ToString("X2") ; 
            Outils.InvokeMethod("cmd::execCmd", ID, "color",commande.state);

            UserControl_Loaded(this, null);
        }

        private void UserControl_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
           
            if (this.Tag == null) return;
            string ID;
            Cmd[] cmd = (this.Tag as Eqlogic).cmds;
            Cmd commande = cmd.GetCmd("luminosity_state");
            if (commande == null) return;
            if (!commande.state.Equals("0"))
                ID = cmd.GetCmd("off").id;
            else
                ID = cmd.GetCmd("on").id;
            commande.state = commande.state.Equals("0") ? "254" : "0";
            Outils.InvokeMethod("cmd::execCmd", ID);

            UserControl_Loaded(sender, null);

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (Tag != null)
            {
                Cmd[] cmd = (Tag as Eqlogic).cmds;
                Cmd commande = cmd.GetCmd("luminosity_state");
                if (commande !=null && !commande.state.Equals("0"))
                {
                    commande = cmd.GetCmd("color_state");

                    ColorHue = Colors.Transparent;
                    SwitchOn = HSBColor.HexToColor(commande.state);
                }
                else
                {
                    ColorHue = Colors.Transparent;
                    SwitchOn = Colors.Transparent;
                }
            }
        }

       

     

        private void LuminositySlider_ManipulationCompleted(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            if (this.Tag == null) return;
            string ID;
            Cmd[] cmd = (this.Tag as Eqlogic).cmds;
            Cmd commande = cmd.GetCmd("luminosity_state");
            ID = cmd.GetCmd("luminosity").id;

            if(commande != null)// dans le cas des groupe de lumiere
                commande.state = ((int)LuminositySlider.Value).ToString();
            Outils.InvokeMethod("cmd::execCmd", ID,"slider", ((int)LuminositySlider.Value).ToString());

            UserControl_Loaded(sender, null);
        }

        private void Image_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            ColorWheelValue.Visibility = Visibility.Visible;

           
        }
    }
}
