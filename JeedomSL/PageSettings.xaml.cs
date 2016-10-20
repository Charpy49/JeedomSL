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
using JeedomSL.Tools;
using ZXing.Mobile;
using Newtonsoft.Json.Linq;

namespace JeedomSL
{
    public partial class PageSettings : PhoneApplicationPage
    {
        Settings.AppSettings settings = new Settings.AppSettings();
        UIElement customOverlayElement = null;
        MobileBarcodeScanner scanner;

        public PageSettings()
        {
            InitializeComponent();
            scanner = new MobileBarcodeScanner(this.Dispatcher);
            scanner.Dispatcher = this.Dispatcher;

            if (App.ViewModel.Items != null && App.ViewModel.Items.result.Where(e => e.eqLogics.Where(r => r.eqType_name.Equals("geoloc")).Count() > 0).Count() > 0)
            {
                var dyn = from geoloc in App.ViewModel.Items.result
                          .SelectMany(e => e.eqLogics).Where(y => y.eqType_name.Equals("geoloc"))
                          .SelectMany(r => r.cmds).ToList()
                              //.Where(f => f.configuration.mode.Equals("dynamic")).ToList()
                          select new PhoneGeoloc { ID = geoloc.id, Name = geoloc.name };

                if (dyn != null)
                {
                    geolocID.ItemsSource = dyn.ToList();
                    if (dyn.ToList().Count()>0)
                    {
                        textBlock4.Visibility = Visibility.Visible;
                        geolocID.Visibility = Visibility.Visible;
                    }

                }
            }

            /*  ApplicationBar = new ApplicationBar();
              ApplicationBar.IsMenuEnabled = true;
              ApplicationBar.IsVisible = true;
              ApplicationBar.Opacity = 1.0;

              ApplicationBarIconButton doneButton = new ApplicationBarIconButton(new Uri("/Images/appbar.check.rest.png", UriKind.Relative));
              doneButton.Text = "done";
              doneButton.Click += new EventHandler(doneButton_Click);

              ApplicationBarIconButton cancelButton = new ApplicationBarIconButton(new Uri("/Images/appbar.cancel.rest.png", UriKind.Relative));
              cancelButton.Text = "cancel";
              cancelButton.Click += new EventHandler(cancelButton_Click);

              ApplicationBar.Buttons.Add(doneButton);
              ApplicationBar.Buttons.Add(cancelButton);*/
        }

        void doneButton_Click(object sender, EventArgs e)
        {
            Uri result;
            settings.IPSetting = textBoxUsername.Text;
            settings.UriPushNotificationKeySetting = uriPushNotification.Text;
            //settings.GeolocSetting = geolocID.SelectedItem != null?( geolocID.SelectedItem as PhoneGeoloc).ID:"";
            if (!Uri.TryCreate(settings.IPKey, UriKind.Absolute, out result))
            {
                MessageBox.Show("Erreur dans l'url de l'api", "Erreur", MessageBoxButton.OK);
                textBoxUsername.BorderBrush = new SolidColorBrush(Colors.Red);
                textBoxUsername.Focus();
            }
            else
            {
                if (geolocID.SelectedItem != null)
                    settings.GeolocSetting = (geolocID.SelectedItem as PhoneGeoloc).ID;
                settings.ApiKeySetting = passwordBoxPassword.Text;
                settings.Save();
                NavigationService.GoBack();
            }
        }

        void cancelButton_Click(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs z)
        {
            if(!string.IsNullOrEmpty(settings.GeolocSetting))
            geolocID.SelectedItem = geolocID.Items.OfType<PhoneGeoloc>().First(e => e.ID == settings.GeolocSetting);
        }

        private void ScanProperties(object sender, RoutedEventArgs e)
        {
            //Tell our scanner to use the default overlay
            scanner.UseCustomOverlay = false;
            //We can customize the top and bottom text of our default overlay
            scanner.TopText = "Scanner le Qr Code du pluggin notify My Windos Phone";
            scanner.BottomText = "Camera will automatically scan barcode\r\n\r\nPress the 'Back' button to Cancel";

            //Start scanning
            scanner.Scan().ContinueWith(t =>
            {
                if (t.Result != null)
                    HandleScanResult(t.Result);
            });
        }

        void HandleScanResult(ZXing.Result result)
        {
            string msg = "";

            if (result != null && !string.IsNullOrEmpty(result.Text))
            {



                this.Dispatcher.BeginInvoke(() =>
                {
                    try
                    {
                        JObject res = JObject.Parse(result.Text);
                        passwordBoxPassword.Text = res["apiKey"].ToString();
                        textBoxUsername.Text = res["url_external"].ToString();
                    }catch(Exception dd)
                    {
                        MessageBox.Show("Erreur de Code Barre");
                    }
                  //  

                //Go back to the main page
                NavigationService.Navigate(new Uri("/PageSettings.xaml", UriKind.Relative));

                //Don't allow to navigate back to the scanner with the back button
               // NavigationService.RemoveBackEntry();
                });
            }else
            {
                MessageBox.Show("Erreur de Code Barre");
            }
        }
        private void geolocID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (e.AddedItems.Count > 0)
            //{
            //    dynamic item = e.AddedItems[0];
            //    settings.GeolocSetting = item.ID;
            //    = e.AddedItems[0]
            //}
            //e.
        }
    }

    internal class PhoneGeoloc
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }
}