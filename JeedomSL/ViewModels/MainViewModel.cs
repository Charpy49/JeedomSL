using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using JeedomSL.Resources;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using JeedomSL.Pluggin;
using Newtonsoft.Json;
using JeedomSL.Tools;

namespace JeedomSL.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        JeedomSL.Settings.AppSettings setting = new JeedomSL.Settings.AppSettings();
        public delegate void EventHandler();
        public event EventHandler DataLoadEvent;

        public string error= String.Empty;

        public MainViewModel()
        {
            //this.Items = new ObservableCollection<ItemViewModel>();
        }

        /// <summary>
        /// Collection pour les objets ItemViewModel.
        /// </summary>
        public Pluggin.Pluggins Items { get; private set; }

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

        
           String result = await Outils.InvokeMethod("object::full");
            App.JsonDashboard = result;

            //
            dynamic stuff = JsonConvert.DeserializeObject(result);
            if (stuff.error != null)
            {
                this.error = stuff.error.message;
            }
            else {
                
                if (!String.IsNullOrEmpty(result))
                {
                    Items = JsonConvert.DeserializeObject<Pluggins>(result);
                    this.IsDataLoaded = true;
                }
            }
            OnDataLoadEvent();

            /*
            if (String.IsNullOrEmpty(setting.IPSetting)) return;
  
            var jsonObject = new JObject();

            jsonObject.Add("jsonrpc", "2.0");
            jsonObject.Add("id", 1);
            jsonObject.Add("method", "object::full");
            JObject myParam = new JObject();
            myParam.Add("apikey", setting.ApiKeySetting);
            jsonObject.Add("params", myParam);

            try {

                string url =  setting.IPKey;
                string JsonStringParams = jsonObject.ToString(); //"{\"jsonrpc\": \"2.0\", \"method\": \"plugin::listPlugin\", \"params\" : {\"apikey\": \"bae8j2vfxveutlx23bvs\"}, \"id\" : 1}";
                var searchParameters = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("request", JsonStringParams) };

                using (var httpclient = new HttpClient())
                {
                    using (var content = new FormUrlEncodedContent(searchParameters))
                    {
                        using (var responseMessages = await httpclient.PostAsync(new Uri(url, UriKind.Absolute), content))
                        {
                            string x = await responseMessages.Content.ReadAsStringAsync();
                            if (!String.IsNullOrEmpty(x))
                            {
                                Items = JsonConvert.DeserializeObject<Pluggins>(x);
                                this.IsDataLoaded = true;
                            }
                            OnDataLoadEvent();
                            // MessageBox.Show(result);
                        }
                    }
                }
            }catch(UriFormatException e) {

                throw e;
            }
            */

        
            
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