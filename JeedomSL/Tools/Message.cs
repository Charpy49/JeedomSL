using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JeedomSL.Tools.Message
{

    public class Message
    {
        public string jsonrpc { get; set; }
        public int id { get; set; }
        public Result[] result { get; set; }
    }

    public class Result
    {
        public string id { get; set; }
        public string date { get; set; }
        public string logicalId { get; set; }
        public string plugin { get; set; }
        private string _message;
        public string message
        {
            get
            {
               return  WebUtility.HtmlDecode(_message);
            }
            set { _message = value; }
        }
        public string action { get; set; }
    }

}
