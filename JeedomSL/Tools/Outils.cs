using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using Windows.Devices.Geolocation;

namespace JeedomSL.Tools
{
    public static class Outils
    {
        private static JeedomSL.Settings.AppSettings setting = new JeedomSL.Settings.AppSettings();

        public static async Task<string> RecoInteract(string query)
        {
            //  HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(Url);
            //  webRequest.Credentials = Credentials;
            string result = String.Empty;
            JObject jsonObject = new JObject();
            jsonObject["jsonrpc"] = "2.0";
            jsonObject["id"] = "1";
            jsonObject["method"] = "interact::tryToReply";
            JObject myParam = new JObject();
            myParam.Add("apikey", setting.ApiKeySetting);
            myParam.Add("query", query);
            jsonObject.Add("params", myParam);


            string url = setting.IPKey;
            string JsonStringParams = jsonObject.ToString(); //"{\"jsonrpc\": \"2.0\", \"method\": \"plugin::listPlugin\", \"params\" : {\"apikey\": \"bae8j2vfxveutlx23bvs\"}, \"id\" : 1}";
            var searchParameters = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("request", JsonStringParams) };

            /*   using (var httpclient = new HttpClient())
               {
                   using (var content = new FormUrlEncodedContent(searchParameters))
                   {
                       using (var responseMessages = await httpclient.PostAsync(new Uri(url, UriKind.Absolute), content))
                       {
                           result = await responseMessages.Content.ReadAsStringAsync();
                           // Items = JsonConvert.DeserializeObject<Pluggins>(x);
                           // MessageBox.Show(result);
                       }
                   }
               }

               return result;*/
            result = await SendRequestAsync(url, searchParameters);
            return result;
        }




        public static async Task<string> PostGeolocToJeedom(Geocoordinate position)
        {

            List<KeyValuePair<string, string>> param = new List<KeyValuePair<string, string>>();
            param.Add(new KeyValuePair<string, string>("api", setting.ApiKeySetting));
            param.Add(new KeyValuePair<string, string>("type", "geoloc"));
            param.Add(new KeyValuePair<string, string>("id", setting.GeolocSetting));
            param.Add(new KeyValuePair<string, string>("value", position.Point.Position.Latitude.ToString("0.000000",CultureInfo.InvariantCulture)+","+ position.Point.Position.Longitude.ToString("0.000000", CultureInfo.InvariantCulture)));

            return await SendRequestAsync(setting.IPKey, param);
        }
            
        public static async Task<string> InvokeMethod(string a_sMethod, string id, params object[] a_params)
        {
            //  HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(Url);
            //  webRequest.Credentials = Credentials;
            string result;

            int nbreParam;
            JObject jsonObject = new JObject();
            jsonObject["jsonrpc"] = "2.0";
            jsonObject["id"] = "1";
            jsonObject["method"] = a_sMethod;
            JObject myParam = new JObject();
            myParam.Add("apikey", setting.ApiKeySetting);
            myParam.Add("id", id);
            if (a_params != null )
            {
                if (a_params.Length > 0)
                {
                    JObject props = new JObject();

                    if (a_params.Length >= 2)
                    {
                        nbreParam = a_params.Length / 2;
                        //JObject myParam2 = new JObject();

                        for (int i = 0; i < nbreParam; i++)
                        {
                            props.Add((string)a_params[i * 2], (string)a_params[i * 2 + 1]);
                        }

                    }
                    else {
                        foreach (var p in a_params)
                        {
                            props.Add(p);
                        }
                    }
                    myParam.Add("options", props);
                }
            
            }
        
            jsonObject.Add("params", myParam);


            string url = setting.IPKey;
            string JsonStringParams = jsonObject.ToString(); //"{\"jsonrpc\": \"2.0\", \"method\": \"plugin::listPlugin\", \"params\" : {\"apikey\": \"bae8j2vfxveutlx23bvs\"}, \"id\" : 1}";
            var searchParameters = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("request", JsonStringParams) };

            /*  using (var httpclient = new HttpClient())
              {
                  using (var content = new FormUrlEncodedContent(searchParameters))
                  {
                      using (var responseMessages = await httpclient.PostAsync(new Uri(url, UriKind.Absolute), content))
                      {
                          result = await responseMessages.Content.ReadAsStringAsync();
                          // Items = JsonConvert.DeserializeObject<Pluggins>(x);
                          // MessageBox.Show(result);
                      }
                  }
              }*/
            result = await SendRequestAsync(url, searchParameters);


            return result;

        }

        public static async Task<string> InvokeMethod(string a_sMethod, string id, bool asParam = true, params object[] a_params)
        {
            //  HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(Url);
            //  webRequest.Credentials = Credentials;
            string result;

            int nbreParam;
            JObject jsonObject = new JObject();
            jsonObject["jsonrpc"] = "2.0";
            jsonObject["id"] = "1";
            jsonObject["method"] = a_sMethod;
            JObject myParam = new JObject();
            myParam.Add("apikey", setting.ApiKeySetting);
            myParam.Add("id", id);
            if (a_params != null && asParam)
            {
                if (a_params.Length > 0)
                {
                    JObject props = new JObject();

                    if (a_params.Length >= 2)
                    {
                        nbreParam = a_params.Length / 2;
                        //JObject myParam2 = new JObject();

                        for (int i = 0; i < nbreParam; i++)
                        {
                            props.Add((string)a_params[i * 2], (string)a_params[i * 2 + 1]);
                        }

                    }
                    else
                    {
                        foreach (var p in a_params)
                        {
                            props.Add(p);
                        }
                    }
                    myParam.Add("options", props);
                }
            }
            else if (a_params != null && !asParam)
            {
                if (a_params.Length > 0)
                {
                    JObject props = new JObject();

                    if (a_params.Length >= 2)
                    {
                        nbreParam = a_params.Length / 2;
                        //JObject myParam2 = new JObject();

                        for (int i = 0; i < nbreParam; i++)
                        {
                            myParam.Add((string)a_params[i * 2], (string)a_params[i * 2 + 1]);
                        }

                    }
                    else
                    {
                        foreach (var p in a_params)
                        {
                            props.Add(p);
                        }
                    }
                    
                }
            }

            jsonObject.Add("params", myParam);


            string url = setting.IPKey;
            string JsonStringParams = jsonObject.ToString(); //"{\"jsonrpc\": \"2.0\", \"method\": \"plugin::listPlugin\", \"params\" : {\"apikey\": \"bae8j2vfxveutlx23bvs\"}, \"id\" : 1}";
            var searchParameters = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("request", JsonStringParams) };

            /*  using (var httpclient = new HttpClient())
              {
                  using (var content = new FormUrlEncodedContent(searchParameters))
                  {
                      using (var responseMessages = await httpclient.PostAsync(new Uri(url, UriKind.Absolute), content))
                      {
                          result = await responseMessages.Content.ReadAsStringAsync();
                          // Items = JsonConvert.DeserializeObject<Pluggins>(x);
                          // MessageBox.Show(result);
                      }
                  }
              }*/
            result = await SendRequestAsync(url, searchParameters);


            return result;

        }

        public static async Task<string> InvokeMethod(string a_sMethod)
        {
            //  HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(Url);
            //  webRequest.Credentials = Credentials;
            string result = string.Empty;
            JObject jsonObject = new JObject();
            jsonObject["jsonrpc"] = "2.0";
            jsonObject["id"] = "1";
            jsonObject["method"] = a_sMethod;
            JObject myParam = new JObject();
            myParam.Add("apikey", setting.ApiKeySetting);
            jsonObject.Add("params", myParam);


            string url = setting.IPKey;
            string JsonStringParams = jsonObject.ToString(); //"{\"jsonrpc\": \"2.0\", \"method\": \"plugin::listPlugin\", \"params\" : {\"apikey\": \"bae8j2vfxveutlx23bvs\"}, \"id\" : 1}";
            var searchParameters = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("request", JsonStringParams) };

            result = await SendRequestAsync(url, searchParameters);




            return result;


        }

        public static async Task<String> SendRequestAsync(string url, List<KeyValuePair<string, string>> searchParameters)
        {
            string result = String.Empty;
            HttpResponseMessage responseMessage = null;

            using (var httpclient = new HttpClient())
            {
                using (var content = new FormUrlEncodedContent(searchParameters))
                {

                    try
                    {
                        responseMessage = await httpclient.PostAsync(new Uri(url, UriKind.Absolute), content);
                    }
                    catch (Exception ex)
                    {
                        if (responseMessage == null)
                        { responseMessage = new HttpResponseMessage(); }
                        responseMessage.StatusCode = HttpStatusCode.InternalServerError;
                        responseMessage.ReasonPhrase = string.Format("RestHttpClient.SendRequest failed: {0}", ex).Replace(Environment.NewLine,String.Empty);
                    }

                }
            }
            if(responseMessage != null && responseMessage.StatusCode == HttpStatusCode.OK)
                result = await responseMessage.Content.ReadAsStringAsync();
            else
                result = @"{""jsonrpc"": ""2.0"", ""error"": {""code"": "+(int)responseMessage.StatusCode+@", ""message"": """+ responseMessage.StatusCode+" "+responseMessage.ReasonPhrase+@"""}, ""id"": ""1""}";

            return result;
        }

    }
    

}
