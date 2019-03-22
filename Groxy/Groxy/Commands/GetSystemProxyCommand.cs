using System;
using Groxy.Helper;
using ShellShell.Core;
using ShellShell.Core.Models;

namespace Groxy.Commands
{
    internal class GetSystemProxyCommand : ShellCommand
    {
        #region Constructor

        /// <inheritdoc />
        public GetSystemProxyCommand(string name, string description = "") : base(name, description)
        {
            CommandAction = Execute;
        }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        public override void PrintUsage()
        {
            Console.WriteLine("Description:");
            Console.WriteLine("Prints out the current system proxy");
            Console.WriteLine("Usage:");
            Console.WriteLine("\tgroxy sysproxy");
        }

        #endregion

        #region Private Methods

        private void Execute(ShellShellExecutor executor)
        {
            // Obtain the 'Proxy' of the  Default browser.  
            string proxy = ProxyHelper.GetSystemProxy();
            string proxyS = ProxyHelper.GetSystemHttpsProxy();
            // Print the Proxy Url to the console.
            if (!string.IsNullOrEmpty(proxy))
            {
                Console.WriteLine("Http Proxy: {0}", proxy);
            }
            else
            {
                Console.WriteLine("Http Proxy is null; no Http proxy will be used");
            }

            if (!string.IsNullOrEmpty(proxy))
            {
                Console.WriteLine("Https Proxy: {0}", proxyS);
            }
            else
            {
                Console.WriteLine("Https Proxy is null; no Https proxy will be used");
            }
        }

        #endregion
    }
}