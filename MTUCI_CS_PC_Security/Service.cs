using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;


namespace MTUCI_CS_PC_Security
{
    internal class Service
    {
        public static string ConnectionStatus()
        {
            string status="Активно";
            var ipStatus=IPStatus.Unknown;
            try
            {
                ipStatus = new Ping().Send("8.8.8.8").Status;
            }
            catch(PingException e)
            {
                status = "Не активно";
            }
            return status;
        }
    }

}
