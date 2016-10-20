using JeedomSL.Tools;
using JeedomSL.Tools.Message;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JeedomSL.ViewModels
{
    public class MessageViewModel : INotifyPropertyChanged
    {

        JeedomSL.Settings.AppSettings setting = new JeedomSL.Settings.AppSettings();
        public delegate void EventHandler();
        public event EventHandler DataLoadEvent;
        public string error = String.Empty;
        public MessageViewModel()
        {
            //this.Items = new ObservableCollection<ItemViewModel>();
        }

        public Message Items { get; private set; }


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


            string result = await Outils.InvokeMethod("message::all");
            App.JsonMessage = result;

            dynamic stuff = JsonConvert.DeserializeObject(result);

            if (stuff.error != null)
            {
                this.error = stuff.error.message;
            }
            else {
                  if (!String.IsNullOrEmpty(result))
                {
                    Items = JsonConvert.DeserializeObject<Message>(result);
                    this.IsDataLoaded = true;
                }
            }
            OnDataLoadEvent();

            
        }

        protected void OnDataLoadEvent()
        {
            if (DataLoadEvent != null)
            {
                DataLoadEvent();
            }
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
