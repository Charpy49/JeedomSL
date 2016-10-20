using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeedomSL.Tools.Scenario
{

    public class Scenario
    {
        public string jsonrpc { get; set; }
        public string id { get; set; }
        public Result[] result { get; set; }
    }

    public class Result
    {
        public string id { get; set; }
        public string name { get; set; }
        public string isActive { get; set; }
        public string group { get; set; }
        public string state { get; set; }
        public string lastLaunch { get; set; }
        public string mode { get; set; }
        public object schedule { get; set; }
        public string pid { get; set; }
        public string[] scenarioElement { get; set; }
        public object trigger { get; set; }
        public string timeout { get; set; }
        public string object_id { get; set; }
        public string isVisible { get; set; }
        public Display display { get; set; }
        public string description { get; set; }
        public Configuration configuration { get; set; }
        public string type { get; set; }
    }

    public class Display
    {
        public string name { get; set; }
        public string icon { get; set; }
    }

    public class Configuration
    {
        public int timeDependency { get; set; }
        public string speedPriority { get; set; }
        public string cmdNoWait { get; set; }
        public string noLog { get; set; }
    }

}

