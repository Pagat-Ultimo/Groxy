using System.IO;

namespace Groxy.Constants
{
    /// <summary>
    /// File paths and names for groxy settings
    /// </summary>
    public class FilePaths
    {
        /// <summary>
        /// Folder name for groxy settings
        /// </summary>
        public static string GroxySettingsDirectory => @"Settings";
        /// <summary>
        /// File name for groxy settings
        /// </summary>
        public static string GroxySettingsFileName => @"GroxySettings.gs";
        /// <summary>
        /// Full path to the groxy settings
        /// </summary>
        public static string GroxySettingsPath => Path.Combine(GroxySettingsDirectory, GroxySettingsFileName);
    }
}
