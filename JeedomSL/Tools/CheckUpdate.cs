using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeedomSL.Tools.CheckUpdate
{
   
    public class CheckUpdate
    {
        public string jsonrpc { get; set; }
        public string id { get; set; }
        public Result[] result { get; set; }
    }

    public class Result
    {
        public string id { get; set; }
        public string type { get; set; }
        public string logicalId { get; set; }
        public string name { get; set; }
        public string localVersion { get; set; }
        public string remoteVersion { get; set; }
        public string status { get; set; }
        public Configuration configuration { get; set; }
    }

    public class Configuration
    {
        public string version { get; set; }
        public object market_owner { get; set; }
        public int market { get; set; }
    }

}
