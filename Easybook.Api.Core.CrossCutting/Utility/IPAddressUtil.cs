using System;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace Easybook.Api.Core.CrossCutting.Utility
{
    public static class IpAddressUtil
    {
        private static readonly IPAddress Empty = IPAddress.Parse("0.0.0.0");
        private static readonly IPAddress IntranetMask1 = IPAddress.Parse("10.255.255.255");
        private static readonly IPAddress IntranetMask2 = IPAddress.Parse("172.16.0.0");
        private static readonly IPAddress IntranetMask3 = IPAddress.Parse("172.31.255.255");
        private static readonly IPAddress IntranetMask4 = IPAddress.Parse("192.168.255.255");

        /// <summary>
        /// Checks the ip version.
        /// </summary>
        /// <param name="ipAddress">The ip address.</param>
        /// <param name="mask">The mask.</param>
        /// <param name="addressBytes">The address bytes.</param>
        /// <param name="maskBytes">The mask bytes.</param>
        /// <exception cref="System.ArgumentException">
        /// The address and mask don't use the same IP standard
        /// </exception>
        private static void CheckIpVersion(IPAddress ipAddress, IPAddress mask, out byte[] addressBytes, out byte[] maskBytes)
        {
            if (mask == null)
            {
                throw new ArgumentException();
            }

            addressBytes = ipAddress.GetAddressBytes();
            maskBytes = mask.GetAddressBytes();

            if (addressBytes.Length != maskBytes.Length)
            {
                throw new ArgumentException("The address and mask don't use the same IP standard");
            }
        }

        /// <summary>
        /// Ands the specified mask.
        /// </summary>
        /// <param name="ipAddress">The ip address.</param>
        /// <param name="mask">The mask.</param>
        /// <returns></returns>
        public static IPAddress And(this IPAddress ipAddress, IPAddress mask)
        {
            byte[] addressBytes;
            byte[] maskBytes;
            CheckIpVersion(ipAddress, mask, out addressBytes, out maskBytes);

            byte[] resultBytes = new byte[addressBytes.Length];
            for (int i = 0; i < addressBytes.Length; ++i)
            {
                resultBytes[i] = (byte)(addressBytes[i] & maskBytes[i]);
            }

            return new IPAddress(resultBytes);
        }

        /// <summary>
        /// Retuns true if the ip address is one of the following
        /// IANA-reserved private IPv4 network ranges (from http://en.wikipedia.org/wiki/IP_address)
        /// Start 	      End
        /// 10.0.0.0 	    10.255.255.255
        /// 172.16.0.0 	  172.31.255.255
        /// 192.168.0.0   192.168.255.255
        /// </summary>
        /// <param name="ipAddress">The ip address.</param>
        /// <returns></returns>
        public static bool IsOnIntranet(this IPAddress ipAddress)
        {
            if (Empty.Equals(ipAddress))
            {
                return false;
            }
            bool onIntranet = IPAddress.IsLoopback(ipAddress);
            onIntranet = onIntranet || ipAddress.Equals(ipAddress.And(IntranetMask1)); //10.255.255.255
            onIntranet = onIntranet || ipAddress.Equals(ipAddress.And(IntranetMask4)); ////192.168.255.255

            onIntranet = onIntranet || (IntranetMask2.Equals(ipAddress.And(IntranetMask2))
              && ipAddress.Equals(ipAddress.And(IntranetMask3)));

            return onIntranet;
        }

        /// <summary>
        /// Gets the client ip address.
        /// </summary>
        /// <returns></returns>
        public static string GetClientIpAddress()
        {
            string ipAddress = string.Empty;
            try
            {
                ipAddress = HttpContext.Current.Request.ServerVariables.Get("REMOTE_ADDR");
                IPAddress validIp;

                if (string.IsNullOrEmpty(ipAddress) || !IPAddress.TryParse(ipAddress, out validIp) || validIp.IsOnIntranet())
                {
                    string xForwardedIp = HttpContext.Current.Request.ServerVariables.Get("HTTP_X_FORWARDED_FOR");

                    //xForwardedIp = "10.10.10.102, 203.81.91.14";
                    if (!string.IsNullOrEmpty(xForwardedIp))
                    {
                        string[] IPs = xForwardedIp.Replace(" ", "").Split(',');
                        foreach (var ip in IPs)
                        {
                            if (IPAddress.TryParse(ip, out validIp) && !validIp.IsOnIntranet())
                            {
                                ipAddress = ip;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Task.Factory.StartNew(() => EmailUtil.SendEmail("[Exception-IpAddressUtil-GetClientIpAddress]", $"{ex.StackTrace}", "alfred@easybook.com;karchoon@easybook.com"));
            }
            return ipAddress;
        }
    }
}