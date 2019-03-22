using System;
using Groxy.Constants;
using Groxy.Models;
using ShellShell.Core;
using ShellShell.Core.Models;

namespace Groxy.Commands
{
    internal class SetConfigValueCommand : ShellCommand
    {
        #region Constructor

        /// <inheritdoc />
        public SetConfigValueCommand(string name, string description = "") : base(name, description)
        {
            CommandAction = Execute;
            ConfigureParameter(ParameterNames.Key, true);
            ConfigureParameter(ParameterNames.Value, true);
            ConfigureSwitch(SwitchesNames.AsEnvironmentVar);
        }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        public override void PrintUsage()
        {
            Console.WriteLine("Description:");
            Console.WriteLine("Sets and saves settings or environment variables to the groxy config");
            Console.WriteLine(
                "Environment variables will be injected in the console session when groxy run is executed.");
            Console.WriteLine("Use the /ev switch to add a environment variable.");
            Console.WriteLine("Usage:");
            Console.WriteLine("\tgroxy set -key <configName> -value <value> {/ev}");
            Console.WriteLine("\tgroxy set <configName> <value> {/ev}");
            Console.WriteLine("Groxy Settings:");
            Console.WriteLine("\tusesystemproxy [true|false] (Default true)");
            Console.WriteLine("Suggested Environment Variables:");
            Console.WriteLine("\tHTTP_PROXY <proxy>");
            Console.WriteLine("\tHTTPS_PROXY <proxy>");
        }

        #endregion

        #region Private Methods

        private void Execute(ShellShellExecutor executor)
        {
            ApplicationSettings config = ApplicationSettings.LoadSettings();
            string key = executor.GetParameterAsString(ParameterNames.Key);
            string value = executor.GetParameterAsString(ParameterNames.Value);
            bool asEnVar = GetSwitchValue(SwitchesNames.AsEnvironmentVar);
            if (asEnVar)
            {
                if (config.EnvironmentVariables.ContainsKey(key))
                    config.EnvironmentVariables[key] = value;
                else
                    config.EnvironmentVariables.Add(key, value);
            }
            else
            {
                if (config.Settings.ContainsKey(key))
                    config.Settings[key] = value;
                else
                    config.Settings.Add(key, value);
            }

            config.SaveSettings();
            Console.WriteLine($"Setting {executor.GetParameterAsString(ParameterNames.Key)} saved!");
        }

        #endregion
    }
}