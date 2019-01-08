using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Groxy.Constants;
using Newtonsoft.Json;

namespace Groxy.Models
{
    class ApplicationSettings
    {
        public Dictionary<string,string> EnviromentVariables { get; set; }
        public static ApplicationSettings LoadSettings()
        {
            if (!File.Exists(FilePaths.GroxySettingsPath))
            {
                var settings = new ApplicationSettings();
                settings.EnviromentVariables = new Dictionary<string, string>();
                return settings;
            }

            var json = File.ReadAllText(FilePaths.GroxySettingsPath);
            return JsonConvert.DeserializeObject<ApplicationSettings>(json);
        }

        public bool SaveSettings()
        {
            try
            {
                var json = JsonConvert.SerializeObject(this);
                if (!Directory.Exists(FilePaths.GroxySettingsDirectory))
                {
                    Directory.CreateDirectory(FilePaths.GroxySettingsDirectory);
                }
                File.WriteAllText(FilePaths.GroxySettingsPath, json);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
