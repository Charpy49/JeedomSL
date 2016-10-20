using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeedomSL.Pluggin
{

    public class Pluggins
    {
        public string jsonrpc { get; set; }
        public int id { get; set; }
        public Result[] result { get; set; }
    }

    public class Result
    {
        public string id { get; set; }
        public string name { get; set; }
        public string father_id { get; set; }
        public string isVisible { get; set; }
        public object position { get; set; }
        public object configuration { get; set; }
        public Display display { get; set; }
        public Eqlogic[] eqLogics { get; set; }
    }

    public class Display
    {
        public string icon { get; set; }
        public string tagColor { get; set; }
        public string tagTextColor { get; set; }
    }

    public class Eqlogic
    {
        public string id { get; set; }
        public string name { get; set; }
        public string logicalId { get; set; }
        public string object_id { get; set; }
        public string eqType_name { get; set; }
        public object eqReal_id { get; set; }
        public string isVisible { get; set; }
        public string isEnable { get; set; }
        public Configuration configuration { get; set; }
        public object specificCapatibilities { get; set; }
        public string timeout { get; set; }
        public Category category { get; set; }
        public object display { get; set; }
        public string order { get; set; }
        public Cmd[] cmds { get; set; }
    }

    public class Configuration
    {
        public string createtime { get; set; }
        public Mode[] modes { get; set; }
        public string updatetime { get; set; }
        public string autorefresh { get; set; }
        public string ip { get; set; }
        public string port { get; set; }
        public string urlStream { get; set; }
        public string protocole { get; set; }
        public string mac { get; set; }
        public string broadcastIP { get; set; }
        public string refreshCron { get; set; }
        public string city { get; set; }
        public string fullMobileDisplay { get; set; }
        public string modeImage { get; set; }
        public string city_name { get; set; }
        public string category { get; set; }
        public string id { get; set; }
        public string alwaysOn { get; set; }
        public string type { get; set; }
        public string model { get; set; }
        public string modelName { get; set; }
        public string softwareVersion { get; set; }
        public string always_active { get; set; }
        public string armed_visible { get; set; }
        public string immediateState_visible { get; set; }
        public string historizedState { get; set; }
        public Zone[] zones { get; set; }
        public Release[] release { get; set; }
        public object[] raz { get; set; }
        public object[] razImmediate { get; set; }
        public object[] activationOk { get; set; }
        public Activationko[] activationKo { get; set; }
        public Activationimmediateok[] activationImmediateOk { get; set; }
        public string product_name { get; set; }
        public int manufacturer_id { get; set; }
        public int product_type { get; set; }
        public int product_id { get; set; }
        public string serverID { get; set; }
        public string fileconf { get; set; }
        public string conf_version { get; set; }
        public string commentaire { get; set; }
        public string battery_type { get; set; }
        public string batteryStatus { get; set; }
        public string batteryStatusDatetime { get; set; }

        public string mode { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string coordinate { get; set; }

    }



    public class Mode
    {
        public string name { get; set; }
        public object[] inAction { get; set; }
        public object[] outAction { get; set; }
        public string zone { get; set; }
    }

    public class Zone
    {
        public string name { get; set; }
        public Action[] actions { get; set; }
        public Actionsimmediate[] actionsImmediate { get; set; }
        public Trigger[] triggers { get; set; }
    }

    public class Action
    {
        public string cmd { get; set; }
        public Options options { get; set; }
    }

    public class Options
    {
        public string title { get; set; }
        public string message { get; set; }
    }

    public class Actionsimmediate
    {
        public string cmd { get; set; }
        public Options1 options { get; set; }
    }

    public class Options1
    {
        public string title { get; set; }
        public string message { get; set; }
    }

    public class Trigger
    {
        public string cmd { get; set; }
        public string armedDelay { get; set; }
        public string waitDelay { get; set; }
        public string invert { get; set; }
    }

    public class Release
    {
        public string cmd { get; set; }
        public Options2 options { get; set; }
    }

    public class Options2
    {
        public string title { get; set; }
        public string message { get; set; }
    }

    public class Activationko
    {
        public string cmd { get; set; }
        public Options3 options { get; set; }
    }

    public class Options3
    {
        public string title { get; set; }
        public string message { get; set; }
    }

    public class Activationimmediateok
    {
        public string cmd { get; set; }
        public Options4 options { get; set; }
    }

    public class Options4
    {
        public string title { get; set; }
        public string message { get; set; }
    }

    public class Category
    {
        public string heating { get; set; }
        public string security { get; set; }
        public string energy { get; set; }
        public string light { get; set; }
        public string automatism { get; set; }
        public string multimedia { get; set; }
        public string _default { get; set; }
    }

    public class Cmd
    {
        public string id { get; set; }
        public string logicalId { get; set; }
        public string eqType { get; set; }
        public string name { get; set; }
        public string order { get; set; }
        public string type { get; set; }
        public string subType { get; set; }
        public string eqLogic_id { get; set; }
        public string isHistorized { get; set; }
        public string unite { get; set; }
        public object cache { get; set; }
        public string eventOnly { get; set; }
        public Configuration1 configuration { get; set; }
        public Template template { get; set; }
        public Display1 display { get; set; }
        public string value { get; set; }
        public string isVisible { get; set; }
        public string state { get; set; }
    }

    public class Configuration1
    {
        public string day { get; set; }
        public string data { get; set; }
        public string id { get; set; }
        public string minValue { get; set; }
        public string maxValue { get; set; }
        public string state { get; set; }
        public string armed { get; set; }
        public string mode { get; set; }
        public string instanceId { get; set; }
        public string _class { get; set; }
        public string value { get; set; }
        public string returnStateValue { get; set; }
        public string returnStateTime { get; set; }
    }

    public class Template
    {
        public string dashboard { get; set; }
        public string mobile { get; set; }
    }

    public class Display1
    {
        public string icon { get; set; }
        public object invertBinary { get; set; }
        public string generic_type { get; set; }
    }


}
