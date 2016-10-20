using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Windows.Phone.Speech.Recognition;
using JeedomSL.Tools;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Windows.Phone.Speech.Synthesis;

namespace JeedomSL
{
    public partial class RecoPage : PhoneApplicationPage
    {
        private SpeechRecognizerUI recoWithUI;
        public RecoPage()
        {
            InitializeComponent();
        }

        private async void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
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
    }
}