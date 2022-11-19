

using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MTUCI_CS_PC_Security
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Brush _greenBrush;
        private static Brush _redBrush;

        private Service service;




        public MainWindow()
        {
            InitializeComponent();

            service=new Service();

            Update();
        }


        private void Update()
        {
            UpdateInternetStatus();
            UpdateAntivirusList();
            UpdateFirewallList();
        }

        private void UpdateInternetStatus()
        {
            if (service.InternetIsActive)
                InternetStatusLabel.Content = "Активно";
            else
                InternetStatusLabel.Content = "Не активно";
        }

        private void UpdateAntivirusList()
        {
            AntivirusListBox.Items.Clear();
            Dictionary<string, bool> antivirusList = service.AntivirusList;
            string status;
            bool tempStatus;
            foreach (var key in antivirusList.Keys)
            {
                antivirusList.TryGetValue(key, out tempStatus);
                status = tempStatus ? "Активен" : "Не активен";
                AntivirusListBox.Items.Add(key + "\t"+status);
            }
        }

        private void UpdateFirewallList()
        {
            FirewallListBox.Items.Clear();
            Dictionary<string, bool> firewallList = service.FireWallList;
            string status;
            bool tempStatus;
            foreach (var key in firewallList.Keys)
            {
                firewallList.TryGetValue(key, out tempStatus);
                status = tempStatus ? "Активен" : "Не активен";
                FirewallListBox.Items.Add(key + "\t" + status);
            }
        }


        private void Update_Button_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not ready yet");
        }
    }
}
