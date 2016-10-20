using JeedomSL.Tools;
using Microsoft.Phone.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Phone.Speech.Recognition;
using Windows.Phone.Speech.Synthesis;

namespace JeedomSL.ViewModels
{
    public class MenuViewModel : INotifyPropertyChanged
    {

      
        public MenuViewModel()
        {
            this.Items = new ObservableCollection<MenuItemViewModel>();
        }

        public ObservableCollection<MenuItemViewModel> Items { get; private set; }

        private SpeechRecognizerUI recoWithUI;

        /// <summary>
        /// Exemple de propriété ViewModel ; cette propriété est utilisée dans la vue pour afficher sa valeur à l\"aide d\"une liaison
        /// </summary>
        /// <returns></returns>

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Crée et ajoute quelques objets ItemViewModel dans la collection Items.
        /// </summary>
        public async void LoadData()
        {


            Items.Add(new MenuItemViewModel { MenuItem = "Dashboard", Image = "/Assets/Param/home64.png", NavigatePage = "/PlugginPage.xaml", SubMenu= "Retrouver vos objets",Visible = false});
            Items.Add(new MenuItemViewModel { MenuItem = "Scenario", Image = "/Assets/Param/services64.png", NavigatePage = "/ScenarioPage.xaml", SubMenu="Liste de vos scénarios", Visible = false });
            MenuItemViewModel mi = new MenuItemViewModel { MenuItem = "Cortana", Image = "/Assets/Param/Microphone64.png", SubMenu="Côntroler Jeedom avec Cortana", Visible = true };
            mi.Click += RecoJeedom;
            Items.Add(mi);
            mi = new MenuItemViewModel { MenuItem = "Redémarrer Jeedom", Image = "/Assets/Param/Restart64.png", Visible = true };
            mi.Click += RestartJeedom;
                 
            Items.Add(mi);
            mi = new MenuItemViewModel { MenuItem = "Arreter Jeedom", Image = "/Assets/Param/Shutdown64.png", Visible = true };
            mi.Click += StopJeedom;
            Items.Add(mi);
            mi = new MenuItemViewModel { MenuItem = "Update Jeedom", Image = "/Assets/Param/Updates64.png", Count="2", Visible = true };
            mi.Click += UpdateJeedom;
            Items.Add(mi);
            Items.Add(new MenuItemViewModel { MenuItem = "Paramètres", Image = "/Assets/Param/settings64.png", NavigatePage="/PageSettings.xaml" ,SubMenu="Paramètres de Jeedom", Visible = true });
            Items.Add(new MenuItemViewModel { MenuItem = "Messages", Image = "/Assets/Param/Message64.png", NavigatePage="/MessagesPage.xaml", SubMenu="Messages de Jeedom", Visible = false });
            Items.Add(new MenuItemViewModel { MenuItem = "A propos...", Image = "/Assets/Param/info64.png" ,NavigatePage="/About.xaml", Visible = true });
            mi = new MenuItemViewModel { MenuItem = "Commentaire", Image = "/Assets/Param/Comments64.png",  SubMenu ="Laisser un commentaire sur le store", Visible = true };
            mi.Click += Eval;
            Items.Add(mi);
            this.IsDataLoaded = true;
        }

        private async void RecoJeedom(object sender)
        {
            this.recoWithUI = new SpeechRecognizerUI();

            // Start recognition (load the dictation grammar by default).
            SpeechRecognitionUIResult recoResult = await recoWithUI.RecognizeWithUIAsync();

            String result = await Outils.RecoInteract(recoResult.RecognitionResult.Text);
            if (!String.IsNullOrEmpty(result))
            {
                JObject res = JsonConvert.DeserializeObject<JObject>(result);
                if (res["result"] != null && !String.IsNullOrEmpty(res["result"].ToString()))
                {
                    SpeechSynthesizer synth = new SpeechSynthesizer();

                    await synth.SpeakTextAsync(res["result"].ToString());
                }
            }
        }

        private void Eval(object sender)
        {
            MarketplaceReviewTask review = new MarketplaceReviewTask();
            review.Show();
        }
        private void SendMail(object sender)
        {
            EmailComposeTask emailComposeTask = new EmailComposeTask();

            emailComposeTask.Subject = "[Weedom] Bugs Suggestions";
            emailComposeTask.Body = "Votre Message";
            emailComposeTask.To = "cedric.charpentier@gmail.com";

            emailComposeTask.Show();
        }
        private void UpdateJeedom(object sender)
        {
            Outils.InvokeMethod("update::update");
        }

        private void StopJeedom(object sender)
        {
            Outils.InvokeMethod("jeeNetwork::halt");
        }

        private void RestartJeedom(object sender)
        {
            Outils.InvokeMethod("jeeNetwork::reboot");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

