using System;
using System.Collections.Generic;
using System.Diagnostics;
using Groxy.Constants;
using Groxy.Helper;
using Groxy.Models;
using ShellShell.Core;
using ShellShell.Core.Models;

namespace Groxy.Commands
{
    internal class RunCommand : ShellCommand
    {
        #region Constructor

        /// <inheritdoc />
        public RunCommand(string name, string description = "") : base(name, description)
        {
            CommandAction = Execute;
            ConfigureParameter(ParameterNames.Program, true);
            ConfigureSwitch(SwitchesNames.Debug);
            ConfigureSwitch(SwitchesNames.UseSysProxy);
        }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        public override void PrintUsage()
        {
            Console.WriteLine("Description:");
            Console.WriteLine("Sets environment variables for the console session, then runs the command");
            Console.WriteLine("Primary used if you are behind a corporate proxy and don't want to set proxy settings system wide.");
            Console.WriteLine("Usage:");
            Console.WriteLine("\tgroxy run -p <command> {/sp}");
            Console.WriteLine("\tgroxy <command> {/sp}");
            Console.WriteLine("Parameters:");
            Console.WriteLine("\tCommand -> The command you want to run e.g. \"git clone ...\"");
            Console.WriteLine("Switches:");
            Console.WriteLine("\t/sp   -> If the HTTP_PROXY and HTTP_PROXY environment variables should be set with the values from your system proxy");
            Console.WriteLine("\t         If you set the usesystemproxy groxy setting to true this behavior will be the default and you can omit the /sp switch");
            Console.WriteLine("\t         To do this use groxy set usesystemproxy true");
        }

        #endregion

        #region Private Methods

        private void Execute(ShellShellExecutor executor)
        {
            ApplicationSettings config = ApplicationSettings.LoadSettings();
            var environmentSets = "";
            bool debug = executor.GetSwitch(SwitchesNames.Debug);
            bool useSysProxy = executor.GetSwitch(SwitchesNames.UseSysProxy);
            foreach (KeyValuePair<string, string> envVar in config.EnvironmentVariables)
            {
                if (debug)
                    Console.WriteLine($"set {envVar.Key}={envVar.Value}");
                environmentSets += $"set {envVar.Key}={envVar.Value}&";
            }

            if (useSysProxy || (config.Settings.ContainsKey("usesystemproxy") && string.Equals(config.Settings["usesystemproxy"], "true", StringComparison.CurrentCultureIgnoreCase)))
            {
                string proxy = ProxyHelper.GetSystemProxy();
                if (!string.IsNullOrEmpty(proxy))
                    environmentSets += $"set HTTP_PROXY={proxy}&";

                string httpsProxy = ProxyHelper.GetSystemHttpsProxy();
                if (!string.IsNullOrEmpty(proxy))
                    environmentSets += $"set HTTPS_PROXY={httpsProxy}&";
            }

            var processInfo = new ProcessStartInfo
            {
                UseShellExecute = false,
                FileName = "cmd.exe",
                Arguments = @"/c " + environmentSets + executor.GetParameterAsString(ParameterNames.Program)
            };

            Console.WriteLine("Executing " + executor.GetParameterAsString(ParameterNames.Program));
            using (Process process = Process.Start(processInfo))
            {
                process?.WaitForExit();
            }
        }

        #endregion
    }
}