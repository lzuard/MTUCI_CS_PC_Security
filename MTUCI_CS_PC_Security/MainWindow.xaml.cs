

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Win32;

namespace MTUCI_CS_PC_Security
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Service service;
        private bool InternetIsActive;
        private Dictionary<string, bool> AntivirusList;
        private Dictionary<string, bool> FirewallList;




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
            InternetIsActive = service.InternetIsActive;
            if (InternetIsActive)
                InternetStatusLabel.Content = "Активно";
            else
                InternetStatusLabel.Content = "Не активно";
        }

        private void UpdateAntivirusList()
        {
            AntivirusListBox.Items.Clear();
            AntivirusList = service.AntivirusList;
            string status;
            bool tempStatus;
            foreach (var key in AntivirusList.Keys)
            {
                AntivirusList.TryGetValue(key, out tempStatus);
                AntivirusListBox.Items.Add(key + "\t" + (tempStatus ? "Активен" : "Не активен"));
            }
        }

        private void UpdateFirewallList()
        {
            FirewallListBox.Items.Clear();
            FirewallList = service.FireWallList;
            string status;
            bool tempStatus;
            foreach (var key in FirewallList.Keys)
            {
                FirewallList.TryGetValue(key, out tempStatus);
                FirewallListBox.Items.Add(key + "\t" + (tempStatus ? "Активен" : "Не активен"));
            }
        }


        private void Update_Button_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {

            var dialog = new OpenFileDialog();
            dialog.Filter = "Текстовые файлы (*.txt)|*.txt";
            try
            {
                if (dialog.ShowDialog() == true)
                {
                    var fileName = dialog.FileName;
                    var date = DateTime.Now;
                    var text = "\nПроверка безопасности "
                               + date
                               + "\nСтатус интернет-соединения "
                               + (InternetIsActive ? "Активно" : "Не активно")
                               + "\n\n Список анивирусов:\n";
                    bool tempStatus;
                    foreach (var key in AntivirusList.Keys)
                    {
                        AntivirusList.TryGetValue(key, out tempStatus);
                        text += key + "\t" + (tempStatus ? "Активен" : "Не активен") + "\n";
                    }

                    text += "\n Список межсетевых экранов:\n";
                    foreach (var key in FirewallList.Keys)
                    {
                        FirewallList.TryGetValue(key, out tempStatus);
                        text += key + "\t" + (tempStatus ? "Активен" : "Не активен") + "\n";
                    }

                    using StreamWriter file = new(fileName, append: true);
                    file.Write(text);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Не удалось сохранить данные в файл");
            }

        }
    }
}
