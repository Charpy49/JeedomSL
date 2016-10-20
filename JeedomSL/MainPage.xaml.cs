using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using JeedomSL.Resources;
using Microsoft.Phone.Notification;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using JeedomSL.Pluggin;
using Windows.Phone.Speech.Recognition;
using JeedomSL.Tools;
using Windows.Phone.Speech.Synthesis;
using JeedomSL.ViewModels;
using Windows.Devices.Geolocation;
using Windows.Media;
namespace JeedomSL
{
    public partial class MainPage : PhoneApplicationPage
    {

        private static JeedomSL.Settings.AppSettings setting = new JeedomSL.Settings.AppSettings();

        // Constructeur
        public MainPage()
        {
            HttpNotificationChannel pushChannel;

            // The name of our push channel.
            string channelName = "Ch@rpySoft.Weedom.Channel";

            InitializeComponent();

            this.DataContext = App.Menu.Items;
            App.ViewModel.DataLoadEvent += ViewModel_DataLoadEvent;
            App.Messages.DataLoadEvent += Messages_DataLoadEvent;
            App.CheckUpdate.DataLoadEvent += CheckUpdate_DataLoadEvent;
            // App.Menu.PropertyChanged += Menu_PropertyChanged;
            App.Scenario.DataLoadEvent += Scenario_DataLoadEvent;
            // Try to find the push channel.
            pushChannel = HttpNotificationChannel.Find(channelName);

            // If the channel was not found, then create a new connection to the push service.
            if (pushChannel == null)
            {
                pushChannel = new HttpNotificationChannel(channelName);

                // Register for all the events before attempting to open the channel.
                pushChannel.ChannelUriUpdated += new EventHandler<NotificationChannelUriEventArgs>(PushChannel_ChannelUriUpdated);
                pushChannel.ErrorOccurred += new EventHandler<NotificationChannelErrorEventArgs>(PushChannel_ErrorOccurred);

                // Register for this notification only if you need to receive the notifications while your application is running.
                pushChannel.ShellToastNotificationReceived += new EventHandler<NotificationEventArgs>(PushChannel_ShellToastNotificationReceived);

                pushChannel.Open();

                // Bind this new channel for toast events.
                pushChannel.BindToShellToast();

            }
            else
            {
                // The channel was already open, so just register for all the events.
                pushChannel.ChannelUriUpdated += new EventHandler<NotificationChannelUriEventArgs>(PushChannel_ChannelUriUpdated);
                pushChannel.ErrorOccurred += new EventHandler<NotificationChannelErrorEventArgs>(PushChannel_ErrorOccurred);

                // Register for this notification only if you need to receive the notifications while your application is running.
                pushChannel.ShellToastNotificationReceived += new EventHandler<NotificationEventArgs>(PushChannel_ShellToastNotificationReceived);

                // Display the URI for testing purposes. Normally, the URI would be passed back to your web service at this point.
                System.Diagnostics.Debug.WriteLine(pushChannel.ChannelUri.ToString());
                setting.UriPushNotificationKeySetting = pushChannel.ChannelUri.ToString();
                setting.Save();

                //MessageBox.Show(String.Format("Channel Uri is {0}",
                //  pushChannel.ChannelUri.ToString()));

            }



        }

        private void CheckUpdate_DataLoadEvent()
        {
            if(App.CheckUpdate.IsDataLoaded)
            {

                (this.listbox.Items[5] as MenuItemViewModel).Count = App.CheckUpdate.Items.result.Count(e => e.status.Equals("update")).ToString();
            }
        }

        private void Scenario_DataLoadEvent()
        {
            if (App.Scenario.IsDataLoaded)
            {
                (this.listbox.Items[1] as MenuItemViewModel).Visible = true;
                // this.jeedomBtn.IsEnabled = true;
            }
            else
            {
                if (!String.IsNullOrEmpty(App.Scenario.error))
                {
                    MessageBox.Show(App.Scenario.error, "Attention", MessageBoxButton.OK);
                }
                else
                    MessageBox.Show("Vérifier votre connexion internet", "Attention", MessageBoxButton.OK);
            }
        }

        private void Messages_DataLoadEvent()
        {
            if (App.Messages.IsDataLoaded)
            {
                (this.listbox.Items[7] as MenuItemViewModel).Count = App.Messages.Items.result.Count().ToString();
                (this.listbox.Items[7] as MenuItemViewModel).Visible = true;

                // this.jeedomBtn.IsEnabled = true;
            }
            else
            {
                if (!String.IsNullOrEmpty(App.Messages.error))
                {
                    MessageBox.Show(App.ViewModel.error, "Attention", MessageBoxButton.OK);
                }
                else
                    MessageBox.Show("Vérifier votre connexion internet", "Attention", MessageBoxButton.OK);
            }
        }

        private void Menu_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ViewModel_DataLoadEvent()
        {
            if (App.ViewModel.IsDataLoaded)
            {
                (this.listbox.Items[0] as MenuItemViewModel).Visible = true;
               // this.jeedomBtn.IsEnabled = true;
            }
            else
            {
                if (!String.IsNullOrEmpty(App.ViewModel.error))
                {
                    MessageBox.Show(App.ViewModel.error, "Attention", MessageBoxButton.OK);
                }
                else
                MessageBox.Show("Vérifier votre connexion internet", "Attention", MessageBoxButton.OK);
            }
            progbar.IsIndeterminate = false;
            progbar.Visibility = Visibility.Collapsed;
        }

        private void buttonNavigate_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Page2.xaml?NavigatedFrom=Main Page", UriKind.Relative));
        }


        void PushChannel_ChannelUriUpdated(object sender, NotificationChannelUriEventArgs e)
        {

            Dispatcher.BeginInvoke(() =>
            {
                // Display the new URI for testing purposes.   Normally, the URI would be passed back to your web service at this point.
                System.Diagnostics.Debug.WriteLine(e.ChannelUri.ToString());
                //MessageBox.Show(String.Format("Channel Uri is {0}",
                //  e.ChannelUri.ToString()));
                setting.UriPushNotificationKeySetting = e.ChannelUri.ToString();
                setting.Save();


            });
        }

        void PushChannel_ErrorOccurred(object sender, NotificationChannelErrorEventArgs e)
        {
            // Error handling logic for your particular application would be here.
            Dispatcher.BeginInvoke(() =>
                MessageBox.Show(String.Format("A push notification {0} error occurred.  {1} ({2}) {3}",
                    e.ErrorType, e.Message, e.ErrorCode, e.ErrorAdditionalData))
                    );
        }

        void PushChannel_ShellToastNotificationReceived(object sender, NotificationEventArgs e)
        {
            StringBuilder message = new StringBuilder();
            string relativeUri = string.Empty;

            message.AppendFormat("Received Toast {0}:\n", DateTime.Now.ToShortTimeString());

            // Parse out the information that was part of the message.
            foreach (string key in e.Collection.Keys)
            {
                message.AppendFormat("{0}: {1}\n", key, e.Collection[key]);

                if (string.Compare(
                    key,
                    "wp:Param",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.CompareOptions.IgnoreCase) == 0)
                {
                    relativeUri = e.Collection[key];
                }
            }

            // Display a dialog of all the fields in the toast.
            Dispatcher.BeginInvoke(() => MessageBox.Show(message.ToString()));

        }
        // Charger les données pour les éléments ViewModel
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.New)
            {
                Windows.Phone.Speech.VoiceCommands.VoiceCommandService.InstallCommandSetsFromFileAsync(new Uri("ms-appx:///WeedomVoiceReconition.xml"));
            }

            if (!String.IsNullOrEmpty(setting.ApiKeySetting))
            {
                if (!App.ViewModel.IsDataLoaded)
                {
                    App.ViewModel.LoadData();
                }
                if (!App.Messages.IsDataLoaded)
                {
                    App.Messages.LoadData();
                }
                if (!App.Menu.IsDataLoaded)
                {
                    App.Menu.LoadData();
                }
                if (!App.Scenario.IsDataLoaded)
                {
                    App.Scenario.LoadData();
                }
                if (!App.CheckUpdate.IsDataLoaded)
                {
                    App.CheckUpdate.LoadData();
                }
            }
            else Button_ParameterClick(null, null);


            if (App.Geolocator == null && !String.IsNullOrWhiteSpace(setting.GeolocSetting))
            {
                App.Geolocator = new Geolocator();
                App.Geolocator.DesiredAccuracy = PositionAccuracy.High;
                App.Geolocator.MovementThreshold = 100; // The units are meters.
                
                App.Geolocator.PositionChanged += geolocator_PositionChanged;
            }
        }
        protected override void OnRemovedFromJournal(System.Windows.Navigation.JournalEntryRemovedEventArgs e)
        {
            if (App.Geolocator != null)
            {
                App.Geolocator.PositionChanged -= geolocator_PositionChanged;
                App.Geolocator = null;
            }
        }

        void geolocator_PositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {

            if (!App.RunningInBackground)
            {

                Outils.PostGeolocToJeedom(args.Position.Coordinate);

                Dispatcher.BeginInvoke(() =>
                {
                 //   LatitudeTextBlock.Text = args.Position.Coordinate.Latitude.ToString("0.00");
                  //  LongitudeTextBlock.Text = args.Position.Coordinate.Longitude.ToString("0.00");
                });
            }
            else
            {
                /*Microsoft.Phone.Shell.ShellToast toast = new Microsoft.Phone.Shell.ShellToast();
                toast.Content = args.Position.Coordinate.Point.Position.Latitude.ToString("0.00");
                toast.Title = "Location: ";
                //toast.NavigationUri = new Uri("/Page2.xaml", UriKind.Relative);
                toast.Show();*/
                Outils.PostGeolocToJeedom(args.Position.Coordinate);


            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // String result="";


            NavigationService.Navigate(new Uri("/PlugginPage.xaml" , UriKind.Relative));
            //    var taskload = LoadPlugin();
            // taskload.Wait();
            //if(taskload.IsCompleted)
            // result = taskload.Result;

            // Pluggins myPlugg = JsonConvert.DeserializeObject<Pluggins>(result);
            //JObject json = JObject.Parse(result);
            //MessageBox.Show(json.ToString());

        }

        private void Button_ParameterClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/PageSettings.xaml", UriKind.Relative));

        }

       

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (e.AddedItems.Count < 1) return;
            if(!String.IsNullOrEmpty((e.AddedItems[0] as ViewModels.MenuItemViewModel).NavigatePage)){
                NavigationService.Navigate(new Uri((e.AddedItems[0] as ViewModels.MenuItemViewModel).NavigatePage, UriKind.Relative));
            }

            (e.AddedItems[0] as ViewModels.MenuItemViewModel).OnClick();
            (sender as ListBox).SelectedItem = null;
        }

        // Exemple de code pour la conception d'une ApplicationBar localisée
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Définit l'ApplicationBar de la page sur une nouvelle instance d'ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Crée un bouton et définit la valeur du texte sur la chaîne localisée issue d'AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Crée un nouvel élément de menu avec la chaîne localisée d'AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}