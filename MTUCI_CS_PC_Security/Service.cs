using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.NetworkInformation;
using System.Management;
using Microsoft.Win32;
using System.Runtime.InteropServices;



namespace MTUCI_CS_PC_Security
{
    internal  class Service
    {
        public Dictionary<string, bool> AntivirusList => GetFromWmi("AntivirusProduct");

        public Dictionary<string, bool> FireWallList => GetFromWmi("FirewallProduct");

        public bool InternetIsActive => IsInternetWorking();

        private bool IsInternetWorking()
        {
            var ipStatus = IPStatus.Unknown;
            try
            {
                ipStatus = new Ping().Send("8.8.8.8").Status;
            }
            catch
            {
            }

            return ipStatus == IPStatus.Success;
        }


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
                    var name = list["displayName"].ToString().Trim();
                    var status = Convert.ToInt32(list["productState"]);
                    resultList.Add(name, GetStatusFromWMI(status));
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return resultList;
        }

        /// <summary>
        /// Checks WMI's status of an instance and returns true if working
        /// </summary>
        private bool GetStatusFromWMI(int decStatus)
        {
            var hexStatus = decStatus.ToString("X").Substring(1, 2);
            return hexStatus is "10" or "11";
        }
    }
}
