using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CodeSwine_Solo_Public_Lobby.Helpers
{
    public class IPTool
    {
        private string _ipAddressV4;

        public string IpAddressV4
        {
            get 
            {
                if (_ipAddressV4 == null) _ipAddressV4 = GrabInternetAddress("https://ipv4.icanhazip.com");
                return _ipAddressV4;
            }
        }
        private string _ipAddressV6;

        public string IpAddressV6
        {
            get
            {
                if (_ipAddressV6 == null) _ipAddressV6 = GrabInternetAddress("https://ipv6.icanhazip.com");
                return _ipAddressV6;
            }
        }
        /// <summary>
        /// Gets the hosts IP Address.
        /// </summary>
        /// <returns>String value of IP.</returns>
        private string GrabInternetAddress(string url)
        {
            // Still needs check to see if we could retrieve the IP.
            // Try for ipv6 first, but if that fails get ipv4
            string ip = "";
            try
            {
                ip = new WebClient().DownloadString(url);
            }
            catch (Exception e)
            {
                ErrorLogger.LogException(e);
                ip = "IP not found.";
            }
            return ip;
        }

        public static bool ValidateIP(string ipString)
        {
            if (String.IsNullOrWhiteSpace(ipString))
            {
                return false;
            }

            IPAddress address;
            if (IPAddress.TryParse(ipString, out address))
            {
                switch (address.AddressFamily)
                {
                    case System.Net.Sockets.AddressFamily.InterNetwork:
                        // we have IPv4 
                        return true;
                    case System.Net.Sockets.AddressFamily.InterNetworkV6:
                        // we have IPv6
                        return true;
                }
            }
            return false;
        }
    }
}
