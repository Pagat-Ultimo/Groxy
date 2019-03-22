using System;
using ShellShell.Core;
using ShellShell.Core.Models;

namespace Groxy.Commands
{
    internal class GetVersionCommand : ShellCommand
    {
        #region Constructor

        /// <inheritdoc />
        public GetVersionCommand(string name, string description = "") : base(name, description)
        {
            CommandAction = Execute;
        }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        public override void PrintUsage()
        {
            Console.WriteLine("Description:");
            Console.WriteLine("Prints out the version info");
            Console.WriteLine("Usage:");
            Console.WriteLine("\tgroxy version");
        }

        #endregion

        #region Private Methods

        private void Execute(ShellShellExecutor executor)
        {
            Console.WriteLine("Version 1.0");
            Console.WriteLine("Maintainer: Christian Kovacs");
            Console.WriteLine("GitHub: https://github.com/Pagat-Ultimo/Groxy");
        }

        #endregion
    }
}
