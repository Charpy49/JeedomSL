using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using JeedomSL.Pluggin;

namespace JeedomSL.Tools
{
    public abstract class TemplateSelector : ContentControl
    {
        public abstract DataTemplate SelectTemplate(object item, DependencyObject container);

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);

            ContentTemplate = SelectTemplate(newContent, this);
        }
    }

    public class PlugginTemplateSelector : TemplateSelector
    {
        public DataTemplate ZwaveLight
        {
            get;
            set;
        }

        public DataTemplate ZwaveStore
        {
            get;
            set;
        }

        public DataTemplate ModePlug
        {
            get;
            set;
        }

        public DataTemplate ZwaveMotion
        {
            get;
            set;
        }
        public DataTemplate ZwaveSmoke
        {
            get;
            set;
        }
        public DataTemplate Camera
        {
            get;
            set;
        }
        public DataTemplate WazeInTime
        {
            get;
            set;
        }
        public DataTemplate PhilippsHue
        {
            get;
            set;
        }
        public DataTemplate Weather
        {
            get;
            set;
        }
        public DataTemplate Network
        {
            get;
            set;
        }
        public DataTemplate Alarm
        {
            get;
            set;
        }
        public DataTemplate Geolocator
        {
            get;
            set;
        }
        public DataTemplate Default
        {
            get;
            set;
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var pluggin = item as Eqlogic;
            if (pluggin != null)
            {
                switch (pluggin.eqType_name)
                {
                    case "philipsHue": return PhilippsHue;
                    case "openzwave":
                        switch (pluggin.configuration.product_type)
                        {
                            case 512:return ZwaveLight;//light
                            case 769:return ZwaveStore;//volet roulant
                            case 770: return ZwaveStore;//volet roulant
                            case 2048: return ZwaveMotion; //motion
                            case 3074: return ZwaveSmoke; //smoke
                            default: return Default;
                        }
                            ;
                    case "networks": return Network;
                    case "weather": return Weather;
                    case "alarm": return Alarm;
                    case "camera": return Camera;
                    case "wazeintime": return WazeInTime;
                    case "geoloc": return Geolocator;
                    case "mode": return ModePlug;
                    default: return Default;
                }
               
            }

            return null;
        }
    }
}
