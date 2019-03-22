using System;
using System.Collections.Generic;
using System.IO;
using Groxy.Constants;
using Newtonsoft.Json;

namespace Groxy.Models
{
    internal class ApplicationSettings
    {
        /// <summary>
        /// Holds values which should be set as environment variables
        /// </summary>
        public Dictionary<string, string> EnvironmentVariables { get; set; }

        /// <summary>
        /// Settings for the groxy application
        /// </summary>
        public Dictionary<string, string> Settings { get; set; }

        /// <summary>
        /// Loads an ApplicationSettings object from the stored config
        /// </summary>
        /// <returns></returns>
        public static ApplicationSettings LoadSettings()
        {
            string pathToGroxy = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            if(pathToGroxy == null)
                throw new Exception("Path to groxy not found");
            string settingsPath = Path.Combine(pathToGroxy, FilePaths.GroxySettingsPath);
            if (!File.Exists(settingsPath))
            {
                var settings = new ApplicationSettings
                {
                    EnvironmentVariables = new Dictionary<string, string>(),
                    Settings = new Dictionary<string, string>()
                };
                return settings;
            }

            string json = File.ReadAllText(settingsPath);
            return JsonConvert.DeserializeObject<ApplicationSettings>(json);
        }

        /// <summary>
        /// Saves the current config to a file
        /// </summary>
        /// <returns></returns>
        public void SaveSettings()
        {
            try
            {
                string pathToGroxy = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                if (pathToGroxy == null)
                    throw new Exception("config could not be loaded!");
                string settingsPath = Path.Combine(pathToGroxy, FilePaths.GroxySettingsPath);
                string settingsDirectory = Path.Combine(pathToGroxy, FilePaths.GroxySettingsDirectory);
                string json = JsonConvert.SerializeObject(this);
                if (!Directory.Exists(settingsDirectory))
                {
                    Directory.CreateDirectory(settingsDirectory);
                }

                File.WriteAllText(settingsPath, json);
            }
            catch (Exception)
            {
                throw new Exception("Groxy settings could not be loaded");
            }
        }
    }
}