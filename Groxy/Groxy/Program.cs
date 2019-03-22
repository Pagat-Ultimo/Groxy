using System;
using Groxy.Commands;
using Groxy.Constants;
using ShellShell.Core;
using ShellShell.Core.Commands;
using ShellShell.Core.Constants;

namespace Groxy
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var shell = new ShellShellExecutor();
            var runCmd = new RunCommand(CommandNames.Run);
            shell.ConfigureCommand(runCmd);

            shell.ConfigureCommand(new SetConfigValueCommand(CommandNames.Set));
            shell.ConfigureCommand(new GetConfigValueCommand(CommandNames.Get));
            shell.ConfigureCommand(new RemoveConfigValueCommand(CommandNames.Remove));
            shell.ConfigureCommand(new GetSystemProxyCommand(CommandNames.SystemProxy));
            shell.ConfigureCommand(new AddToPathCommand(BasicCommandNames.AddToPathCommandName));
            shell.ConfigureCommand(new GetVersionCommand(CommandNames.Version));
            shell.ConfigureCommand(new GetLicenseCommand(CommandNames.License));

            shell.UseDefaultCommand = true;
            shell.DefaultCommand = runCmd;

            try
            {
                shell.SetArguments(args);
                shell.Execute();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}