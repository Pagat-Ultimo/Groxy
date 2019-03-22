namespace Groxy.Constants
{
    /// <summary>
    /// Command names
    /// </summary>
    public class CommandNames
    {
        #region Statics, Constants

        /// <summary>
        /// Config set command
        /// </summary>
        public const string Set = "set";

        /// <summary>
        /// Config get command
        /// </summary>
        public const string Get = "get";

        /// <summary>
        /// Config remove command
        /// </summary>
        public const string Remove = "remove";

        /// <summary>
        /// Groxy run command
        /// </summary>
        public const string Run = "run";

        /// <summary>
        /// Print system proxy command
        /// </summary>
        public const string SystemProxy = "sysproxy";

        /// <summary>
        /// Print version info
        /// </summary>
        public const string Version = "version";

        /// <summary>
        /// License infos
        /// </summary>
        public const string License = "license";

        #endregion
    }

    /// <summary>
    /// Parameter names
    /// </summary>
    public class ParameterNames
    {
        #region Statics, Constants

        /// <summary>
        /// Key parameter for config operations
        /// </summary>
        public const string Key = "key";

        /// <summary>
        /// Value parameter for config operations
        /// </summary>
        public const string Value = "value";

        /// <summary>
        /// Specifies the command to be executed in the run command
        /// </summary>
        public const string Program = "p";

        #endregion
    }

    /// <summary>
    /// Switch names
    /// </summary>
    public class SwitchesNames
    {
        #region Statics, Constants

        /// <summary>
        /// Activates debug output for commands
        /// </summary>
        public const string Debug = "d";

        /// <summary>
        /// Tells the run command to inject the system proxy settings
        /// </summary>
        public const string UseSysProxy = "sp";

        /// <summary>
        /// Adds the config value to the environment collection
        /// </summary>
        public const string AsEnvironmentVar = "ev";

        #endregion
    }
}