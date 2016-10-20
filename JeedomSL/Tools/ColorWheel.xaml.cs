using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Shapes;

using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace JeedomSL.Tools
{
    public partial class ColorWheel : UserControl
    {

        public delegate void EventHandler();
        public event EventHandler ColorChangeEvent;



        public static readonly DependencyProperty ColorProperty =
      DependencyProperty.Register("ColorHue", typeof(Color), typeof(ColorWheel), new PropertyMetadata(default(Color)));
        public Color ColorHue
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }


        WriteableBitmap writeableBmp;
        public ColorWheel()
        {
            InitializeComponent();
                  
        }

      

      /*  private void ColorWheelImage_ManipulationCompleted(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            ColorHue = writeableBmp.GetPixel((int)e.ManipulationOrigin.X, (int)e.ManipulationOrigin.Y);
            this.Visibility = Visibility.Collapsed;
            OnColorChangeEvent();
        }
        */
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
          //  writeableBmp = new WriteableBitmap((BitmapImage)ColorWheelImage.Source);

        }

        protected void OnColorChangeEvent()
        {
            if (ColorChangeEvent != null)
            {
                ColorChangeEvent();
            }
        }

        private void ColorHexagonPicker_ColorChanged(object sender, Color color)
        {
            ColorHue = color;
            this.Visibility = Visibility.Collapsed;
            OnColorChangeEvent();
        }
    }
}
