using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Groxy.Constants
{
    public class CommandNames
    {
        public const string Set = "set";
        public const string Get = "get";
        public const string Remove = "remove";
        public const string Run = "run";
        public const string SystemProxy = "sysproxy";
        public const string AddToPath = "path";
    }

    public class ParameterNames
    {
        public const string Key = "key";
        public const string Value = "value";
        public const string Program = "p";
    }

    public class SwitchesNames
    {
        public const string Debug = "d";
        public const string UseSysProxy = "sp";
        public const string AddToPath = "add";
    }
}
