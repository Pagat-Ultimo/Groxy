using System;
using Groxy.Constants;
using Groxy.Models;
using ShellShell.Core;
using ShellShell.Core.Models;

namespace Groxy.Commands
{
    internal class RemoveConfigValueCommand : ShellCommand
    {
        #region Constructor

        /// <inheritdoc />
        public RemoveConfigValueCommand(string name, string description = "") : base(name, description)
        {
            CommandAction = Execute;
            ConfigureParameter(ParameterNames.Key, true);
        }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        public override void PrintUsage()
        {
            Console.WriteLine("Description:");
            Console.WriteLine("removes as setting or an environment variables from groxy config");
            Console.WriteLine("Use this on non Environment settings to reset to default value");
            Console.WriteLine("Usage:");
            Console.WriteLine("\tgroxy remove -key <configName>");
            Console.WriteLine("\tgroxy remove <configName>");
            Console.WriteLine("Groxy Settings:");
            Console.WriteLine("\tusesystemproxy");
            Console.WriteLine("Suggested Environment Variables:");
            Console.WriteLine("\tHTTP_PROXY");
            Console.WriteLine("\tHTTPS_PROXY");
        }

        #endregion

        #region Private Methods

        private void Execute(ShellShellExecutor executor)
        {
            ApplicationSettings config = ApplicationSettings.LoadSettings();
            string key = executor.GetParameterAsString(ParameterNames.Key);
            if (config.EnvironmentVariables.ContainsKey(key))
            {
                config.EnvironmentVariables.Remove(key);
                config.SaveSettings();
                Console.WriteLine($"Setting {key} removed!");
            }
            else if (config.Settings.ContainsKey(key))
            {
                config.Settings.Remove(key);
                config.SaveSettings();
                Console.WriteLine($"Setting {key} removed!");
            }
            else
            {
                Console.WriteLine($"Setting {key} was not set!");
            }
        }

        #endregion
    }
}