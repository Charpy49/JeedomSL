using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Reflection;
using Microsoft.Phone.Tasks;

namespace Lequipe
{
    public partial class About : PhoneApplicationPage
    {
        public About()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            textBlock1.Text += " " + Assembly.GetExecutingAssembly().FullName.Split(',')[1].Split('=')[1].ToString();
            
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

            var unMarketplaceReviewTask = new MarketplaceReviewTask();
            unMarketplaceReviewTask.Show(); 
        }
    }
}