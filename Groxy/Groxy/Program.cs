using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Groxy.Constants;
using Groxy.Models;
using ShellShell.Core.Models;

namespace Groxy
{
    class Program
    {
        static ShellShell.Core.ShellShell Shell = null;
        static void Main(string[] args)
        {
            Shell = new ShellShell.Core.ShellShell();
            var runCmd = new ShellCommand(CommandNames.Run, RunCommand);
            runCmd.ConfigureParameter(ParameterNames.Program, true, "");
            runCmd.ConfigureSwitch(SwitchesNames.Debug, false);
            runCmd.ConfigureSwitch(SwitchesNames.UseSysProxy, false);
            Shell.ConfigureCommand(runCmd);

            var cmd = new ShellCommand(CommandNames.Set, SetConfigValue);
            cmd.ConfigureParameter(ParameterNames.Key, true, "");
            cmd.ConfigureParameter(ParameterNames.Value, true, "");
            Shell.ConfigureCommand(cmd);

            var cmd2 = new ShellCommand(CommandNames.Get, GetConfigValue);
            cmd2.ConfigureParameter(ParameterNames.Key, true, "");
            Shell.ConfigureCommand(cmd2);

            var cmd3 = new ShellCommand(CommandNames.Remove, RemoveConfigValue);
            cmd3.ConfigureParameter(ParameterNames.Key, true, "");
            Shell.ConfigureCommand(cmd3);

            var systemProxyCmd = new ShellCommand(CommandNames.SystemProxy, GetSystemProxyCommand);
            Shell.ConfigureCommand(systemProxyCmd);

            try
            {
                Shell.SetArguments(args);
                Shell.Execute();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void SetConfigValue()
        {
            var config = ApplicationSettings.LoadSettings();
            var key = Shell.GetParameterAsString(ParameterNames.Key);
            var value = Shell.GetParameterAsString(ParameterNames.Value);
            if (config.EnviromentVariables.ContainsKey(key))
                config.EnviromentVariables[key] = value;
            else
                config.EnviromentVariables.Add(key, value);
            config.SaveSettings();
            Console.WriteLine($"Setting {Shell.GetParameterAsString(ParameterNames.Key)} saved!");
        }

        static void GetConfigValue()
        {
            var config = ApplicationSettings.LoadSettings();
            var key = Shell.GetParameterAsString(ParameterNames.Key);
            if (config.EnviromentVariables.ContainsKey(key))
            {
                Console.WriteLine($"{key} -> {config.EnviromentVariables[key]}");
            }
            else
            {
                Console.WriteLine($"{key} -> Error Not Found");
            }
        }

        static void RemoveConfigValue()
        {
            var config = ApplicationSettings.LoadSettings();
            var key = Shell.GetParameterAsString(ParameterNames.Key);
            if (config.EnviromentVariables.ContainsKey(key))
            {
                config.EnviromentVariables.Remove(key);
                config.SaveSettings();
                Console.WriteLine($"Setting {Shell.GetParameterAsString(ParameterNames.Key)} removed!");
            }
        }

        static void RunCommand()
        {
            var config = ApplicationSettings.LoadSettings();
            var enviromentSets = "";
            bool debug = Shell.GetSwitch(SwitchesNames.Debug);
            bool useSysProxy = Shell.GetSwitch(SwitchesNames.UseSysProxy);
            foreach (var envVar in config.EnviromentVariables)
            {
                if(debug)
                    Console.WriteLine($"set {envVar.Key}={envVar.Value}");
                enviromentSets += $"set {envVar.Key}={envVar.Value}&";
            }

            if (useSysProxy)
            {
                string proxy = GetSystemProxy();
                if(!string.IsNullOrEmpty(proxy))
                 enviromentSets += $"set HTTP_PROXY={proxy}&";
            }

            var processInfo = new ProcessStartInfo
            {
                UseShellExecute = false,
                FileName = "cmd.exe",
                Arguments = @"/c " + enviromentSets + Shell.GetParameterAsString(ParameterNames.Program)
            };

            Console.WriteLine("Executing " + Shell.GetParameterAsString(ParameterNames.Program));
            using (var process = Process.Start(processInfo))
            {
                process.WaitForExit();
            }
        }

        static void GetSystemProxyCommand()
        {
            // Obtain the 'Proxy' of the  Default browser.  
            string proxy = GetSystemProxy();
            // Print the Proxy Url to the console.
            if (!string.IsNullOrEmpty(proxy))
            {
                Console.WriteLine("Proxy: {0}", proxy);
            }
            else
            {
                Console.WriteLine("Proxy is null; no proxy will be used");
            }
        }

        private static string GetSystemProxy()
        {
            HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create("http://www.microsoft.com");

            // Obtain the 'Proxy' of the  Default browser.  
            IWebProxy proxy = myWebRequest.Proxy;
            // Print the Proxy Url to the console.
            if (proxy != null)
            {
                return proxy.GetProxy(myWebRequest.RequestUri).ToString();
            }
            else
            {
               return "";
            }
        }

    }
}
