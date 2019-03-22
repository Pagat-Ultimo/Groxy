using System.Net;

namespace Groxy.Helper
{
    internal class ProxyHelper
    {
        #region Public Methods

        /// <summary>
        /// Gets the HTTP system proxy
        /// </summary>
        /// <returns></returns>
        public static string GetSystemProxy()
        {
            var myWebRequest = (HttpWebRequest) WebRequest.Create("http://www.microsoft.com");

            // Obtain the 'Proxy' of the  Default browser.  
            IWebProxy proxy = myWebRequest.Proxy;
            // Print the Proxy Url to the console.
            return proxy.GetProxy(myWebRequest.RequestUri).ToString();
        }

        /// <summary>
        /// Gets the HTTPS system proxy
        /// </summary>
        /// <returns></returns>
        public static string GetSystemHttpsProxy()
        {
            var myWebRequest = (HttpWebRequest) WebRequest.Create("https://www.microsoft.com");

            // Obtain the 'Proxy' of the  Default browser.  
            IWebProxy proxy = myWebRequest.Proxy;
            // Print the Proxy Url to the console.
            return proxy.GetProxy(myWebRequest.RequestUri).ToString();
        }

        #endregion
    }
}