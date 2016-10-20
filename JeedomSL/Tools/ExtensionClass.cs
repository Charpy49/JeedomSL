using JeedomSL.Pluggin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeedomSL.Tools
{
    public static class ExtensionClass
    {
        public static string ToString (this Cmd[] cmds, String objet)
        {
            Cmd tmp = cmds.GetCmd(objet);
            if (tmp != null)
                return tmp.ToString("");
            return "";
        }
        public static string ToString(this Cmd cmd,string tmp)
        {
            if (cmd != null)
                return cmd.state + cmd.unite;
            return "";
        }

        public static Cmd GetCmd(this Cmd[] cmds, String objet)
        {
            return cmds.Where(f => f.logicalId.Equals(objet)).FirstOrDefault();
           
        }

        public static Cmd GetCmdByName(this Cmd[] cmds, String objet)
        {
            return cmds.Where(f => f.name.Equals(objet)).FirstOrDefault();

        }
        public static Cmd GetCmdByConfigModeName(this Cmd[] cmds, String objet)
        {
            return cmds.Where(f => f.configuration.mode.Equals(objet)).FirstOrDefault();

        }
    }
}
