using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Groxy.Constants
{
    public class FilePaths
    {
        public static string GroxySettingsDirectory => @"Settings";
        public static string GroxySettingsFileName => @"GroxySettings.gs";
        public static string GroxySettingsPath => Path.Combine(GroxySettingsDirectory, GroxySettingsFileName);
    }
}
