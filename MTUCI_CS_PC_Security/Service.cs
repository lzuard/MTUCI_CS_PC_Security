using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.NetworkInformation;
using System.Management;
using Microsoft.Win32;
using System.Runtime.InteropServices;



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

        private static bool IsAntivirusStatusActive(int decStatus)
        {
            var hexStatus = decStatus.ToString("X").Substring(1, 2);
            return hexStatus is "10" or "11";
        }

        public static bool IsAntivirusInstalled()
        {
            var antivirusList=new Dictionary<string,bool>();
            var scope = new ManagementScope(@"root\SecurityCenter2");
            var query = new ObjectQuery("Select * From " + "AntivirusProduct");
            var searcher = new ManagementObjectSearcher(scope,query);
            try
            {
                foreach (var list in searcher.Get())
                {
                    var name = list["displayName"].ToString().Trim();
                    var status = Convert.ToInt32(list["productState"]);
                    antivirusList.Add(name,IsAntivirusStatusActive(status));
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return antivirusList.Count > 0;
        }

        public static bool IsAntivirusWorking()
        {
            return false;
        }
    }

}
