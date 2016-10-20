using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using JeedomSL.Tools;
using Coding4Fun.Toolkit.Controls;
using JeedomSL.Tools.Message;

namespace JeedomSL
{
    public partial class MessagesPage : PhoneApplicationPage
    {
        public MessagesPage()
        {
            InitializeComponent();
            this.DataContext = App.Messages.Items; 
                
        }

        private async void TextBlock_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            string result = await Outils.InvokeMethod("message::removeAll");
            this.DataContext = null;
        }

       
    }
}