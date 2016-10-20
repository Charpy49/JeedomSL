using System;
using System.IO.IsolatedStorage;
using System.Diagnostics;
using System.Collections.Generic;


namespace JeedomSL.Settings
{

    public class AppSettings
    {
        // Our settings
        IsolatedStorageSettings settings;

        // The key names of our settings
        /*   const string CheckBoxSettingKeyName = "CheckBoxSetting";
           const string ListBoxSettingKeyName = "ListBoxSetting";
           const string RadioButton1SettingKeyName = "RadioButton1Setting";
           const string RadioButton2SettingKeyName = "RadioButton2Setting";
           const string RadioButton3SettingKeyName = "RadioButton3Setting";
          */
        const string IPSettingKeyName = "IPSetting";
        const string ApiKeySettingKeyName = "ApiKeySetting";
        const string UriPushNotificationKeyName = "UriPushNotification";
        const string GeolocKeyName = "Geoloc";

        // The default value of our settings
        /*   const bool CheckBoxSettingDefault = true;
           const int ListBoxSettingDefault = 0;
           const bool RadioButton1SettingDefault = true;
           const bool RadioButton2SettingDefault = false;
           const bool RadioButton3SettingDefault = false;
          */
        const string IPSettingDefault = "";
        const string ApiKeySettingDefault = "";
        const string UriPushNotificationDefault = "";
        const string GeolocDefault = "";

        /// <summary>
        /// Constructor that gets the application settings.
        /// </summary>
        public AppSettings()
        {
            // Get the settings for this application.
            settings = IsolatedStorageSettings.ApplicationSettings;
        }

        /// <summary>
        /// Update a setting value for our application. If the setting does not
        /// exist, then add the setting.
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool AddOrUpdateValue(string Key, Object value)
        {
            bool valueChanged = false;

            // If the key exists
            if (settings.Contains(Key))
            {
                // If the value has changed
                if (settings[Key] != value)
                {
                    // Store the new value
                    settings[Key] = value;
                    valueChanged = true;
                }
            }
            // Otherwise create the key.
            else
            {
                settings.Add(Key, value);
                valueChanged = true;
            }
            return valueChanged;
        }

        /// <summary>
        /// Get the current value of the setting, or if it is not found, set the 
        /// setting to the default setting.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public T GetValueOrDefault<T>(string Key, T defaultValue)
        {
            T value;

            // If the key exists, retrieve the value.
            if (settings.Contains(Key))
            {
                value = (T)settings[Key];
            }
            // Otherwise, use the default value.
            else
            {
                value = defaultValue;
            }
            return value;
        }

        /// <summary>
        /// Save the settings.
        /// </summary>
        public void Save()
        {
            settings.Save();
        }


        /// <summary>
        /// Property to get and set a CheckBox Setting Key.
        /// </summary>
        /*   public bool CheckBoxSetting
           {
               get
               {
                   return GetValueOrDefault<bool>(CheckBoxSettingKeyName, CheckBoxSettingDefault);
               }
               set
               {
                   if (AddOrUpdateValue(CheckBoxSettingKeyName, value))
                   {
                       Save();
                   }
               }
           }


           /// <summary>
           /// Property to get and set a ListBox Setting Key.
           /// </summary>
           public int ListBoxSetting
           {
               get
               {
                   return GetValueOrDefault<int>(ListBoxSettingKeyName, ListBoxSettingDefault);
               }
               set
               {
                   if (AddOrUpdateValue(ListBoxSettingKeyName, value))
                   {
                       Save();
                   }
               }
           }


           /// <summary>
           /// Property to get and set a RadioButton Setting Key.
           /// </summary>
           public bool RadioButton1Setting
           {
               get
               {
                   return GetValueOrDefault<bool>(RadioButton1SettingKeyName, RadioButton1SettingDefault);
               }
               set
               {
                   if (AddOrUpdateValue(RadioButton1SettingKeyName, value))
                   {
                       Save();
                   }
               }
           }


           /// <summary>
           /// Property to get and set a RadioButton Setting Key.
           /// </summary>
           public bool RadioButton2Setting
           {
               get
               {
                   return GetValueOrDefault<bool>(RadioButton2SettingKeyName, RadioButton2SettingDefault);
               }
               set
               {
                   if (AddOrUpdateValue(RadioButton2SettingKeyName, value))
                   {
                       Save();
                   }
               }
           }

           /// <summary>
           /// Property to get and set a RadioButton Setting Key.
           /// </summary>
           public bool RadioButton3Setting
           {
               get
               {
                   return GetValueOrDefault<bool>(RadioButton3SettingKeyName, RadioButton3SettingDefault);
               }
               set
               {
                   if (AddOrUpdateValue(RadioButton3SettingKeyName, value))
                   {
                       Save();
                   }
               }
           }
           */
        /// <summary>
        /// Property to get and set a Username Setting Key.
        /// </summary>
        /// 
        public string IPKey
        {
            get
            {
                return String.Format("{0}/core/api/jeeApi.php", IPSetting);
            }
        }
        public string IPSetting
        {
            get
            {
                return GetValueOrDefault<string>(IPSettingKeyName, IPSettingDefault);
            }
            set
            {
                if (AddOrUpdateValue(IPSettingKeyName, value))
                {
                    Save();
                }
            }
        }

        public string GeolocSetting
        {
            get
            {
                return GetValueOrDefault<string>(GeolocKeyName, GeolocDefault);
            }
            set
            {
                if (AddOrUpdateValue(GeolocKeyName, value))
                {
                    Save();
                }
            }
        }
        /// <summary>
        /// Property to get and set a Password Setting Key.
        /// </summary>
        public string ApiKeySetting
        {
            get
            {
                return GetValueOrDefault<string>(ApiKeySettingKeyName, ApiKeySettingDefault);
            }
            set
            {
                if (AddOrUpdateValue(ApiKeySettingKeyName, value))
                {
                    Save();
                }
            }
        }
        public string UriPushNotificationKeySetting
        {
            get
            {
                return GetValueOrDefault<string>(UriPushNotificationKeyName, UriPushNotificationDefault);
            }
            set
            {
                if (AddOrUpdateValue(UriPushNotificationKeyName, value))
                {
                    Save();
                }
            }
        }
    }
}

