using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Management;


namespace MTUCI_CS_PC_Security
{
    internal  class Service
    {
        //List of antivirus
        public Dictionary<string, bool> AntivirusList => GetFromWmi("AntivirusProduct");
        //List of firewall
        public Dictionary<string, bool> FireWallList => GetFromWmi("FirewallProduct");
        //Internebt status
        public bool InternetIsActive => IsInternetWorking();

        /// <summary>
        /// Sends ping to Google's DNS to check internet connection
        /// </summary>
        private bool IsInternetWorking()
        {
            var ipStatus = IPStatus.Unknown;
            try
            {
                ipStatus = new Ping().Send("8.8.8.8").Status;
            }
            catch
            {
                // ignored
            }
            return ipStatus == IPStatus.Success;
        }

        /// <summary>
        /// Sends WMI query and gets display name and status for
        /// every instance found
        /// </summary>
        /// <param name="className"></param>
        /// <returns></returns>
        private Dictionary<string, bool> GetFromWmi(string className)
        {
            var resultList = new Dictionary<string, bool>();
            var scope = new ManagementScope(@"root\SecurityCenter2");
            var query = new ObjectQuery("Select * From " + className);
            var searcher = new ManagementObjectSearcher(scope, query);

            try
            {
                foreach (var list in searcher.Get())
                {
                    var name = list["displayName"].ToString()?.Trim();
                    var status = Convert.ToInt32(list["productState"]);
                    resultList.Add(name, GetStatusFromWMI(status));
                }

            }
            catch
            {
                //ignored
            }

            return resultList;
        }

        /// <summary>
        /// Converts WMI status to bool status
        /// </summary>
        private bool GetStatusFromWMI(int decStatus)
        {
            var hexStatus = decStatus.ToString("X").Substring(1, 2);
            return hexStatus is "10" or "11";
        }
    }
}
