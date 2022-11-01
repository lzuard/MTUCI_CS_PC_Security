using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;


namespace MTUCI_CS_PC_Security
{
    internal static class Service
    {
        public static bool IsInternetWorking()
        {
            var ipStatus=IPStatus.Unknown;
            try
            {
                ipStatus = new Ping().Send("8.8.8.8").Status;
            }
            catch{}

            return ipStatus==IPStatus.Success;
        }

        public static bool IsFirewallInstalled()
        {
            return false;
        }

        public static bool IsFireWallWorking()
        {
            return false;
        }

        public static bool IsAntivirusInstalled()
        {
            return false;
        }

        public static bool IsAntivirusWorking()
        {
            return false;
        }
    }

}
