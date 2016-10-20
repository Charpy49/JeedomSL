using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace JeedomSL.ViewModels
{
    public delegate void ClickEventHandler(object sender);


    public class MenuItemViewModel : INotifyPropertyChanged
    {
         public event ClickEventHandler Click;
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        // Invoke the Changed event; called whenever list changes
        public void OnClick()
        {
            if (Click != null)
                Click(this);
        }
        private String _menuItem;

        public String MenuItem
        {
            get { return _menuItem; }
            set { _menuItem = value; }
        }

        private String _image;

        public String Image
        {
            get { return _image; }
            set { _image = value; }
        }
        private String _navigatePage;

        public String NavigatePage
        {
            get { return _navigatePage; }
            set { _navigatePage = value; }
        }
        private string _count;
        public string Count { get { return _count; } set { _count = value;OnPropertyChanged("Count"); } }
        public string SubMenu { get; internal set; }

        private bool _vivible;
        public bool Visible
        {
            get { return _vivible; }
            set
            {
                _vivible = value;
                OnPropertyChanged("Visible");
            }
        }
    }
}