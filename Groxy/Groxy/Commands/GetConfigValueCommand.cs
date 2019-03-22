using System;
using Groxy.Constants;
using Groxy.Models;
using ShellShell.Core;
using ShellShell.Core.Models;

namespace Groxy.Commands
{
    internal class GetConfigValueCommand : ShellCommand
    {
        #region Constructor

        /// <inheritdoc />
        public GetConfigValueCommand(string name, string description = "") : base(name, description)
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
            Console.WriteLine("Gets a setting or an environment variable from groxy config");
            Console.WriteLine("Usage:");
            Console.WriteLine("\tgroxy get -key <configName>");
            Console.WriteLine("\tgroxy get <configName>");
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
                Console.WriteLine($"{key} -> {config.EnvironmentVariables[key]}");
            }
            else if (config.Settings.ContainsKey(key))
            {
                Console.WriteLine($"{key} -> {config.Settings[key]}");
            }
            else
            {
                Console.WriteLine($"{key} -> Error Not Found");
            }
        }

        #endregion
    }
}