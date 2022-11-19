using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace MTUCI_CS_PC_Security
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly Service _service;                        //WMI interactions class       
        private bool _internetIsActive;                  //Internet status
        private Dictionary<string, bool> _antivirusList; //List of antivirus
        private Dictionary<string, bool> _firewallList;  //List of firewalls


        /// <summary>
        /// Window constructor 
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            _service=new Service();
            Update();
        }


        /// <summary>
        /// Starts up all update functions
        /// </summary>
        private void Update()
        {
            UpdateInternetStatus();
            UpdateAntivirusList();
            UpdateFirewallList();
        }

        /// <summary>
        /// Updates internet status and sends result to UI
        /// </summary>
        private void UpdateInternetStatus()
        {
            _internetIsActive = _service.InternetIsActive;
            if (_internetIsActive)
                InternetStatusLabel.Content = "Активно";
            else
                InternetStatusLabel.Content = "Не активно";
        }

        /// <summary>
        /// Updates antivirus list and sends result to UI
        /// </summary>
        private void UpdateAntivirusList()
        {
            AntivirusListBox.Items.Clear();
            _antivirusList = _service.AntivirusList;
            foreach (var key in _antivirusList.Keys)
            {
                _antivirusList.TryGetValue(key, out var tempStatus);
                AntivirusListBox.Items.Add(key + "\t" + (tempStatus ? "Активен" : "Не активен"));
            }
        }

        /// <summary>
        /// Updates firewall list and sends result to UI
        /// </summary>
        private void UpdateFirewallList()
        {
            FirewallListBox.Items.Clear();
            _firewallList = _service.FireWallList;
            foreach (var key in _firewallList.Keys)
            {
                _firewallList.TryGetValue(key, out var tempStatus);
                FirewallListBox.Items.Add(key + "\t" + (tempStatus ? "Активен" : "Не активен"));
            }
        }

        /// <summary>
        /// Button update handler
        /// </summary>
        private void Update_Button_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }

        /// <summary>
        /// Button save handler
        /// </summary>
        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {

            var dialog = new OpenFileDialog
            {
                Filter = "Текстовые файлы (*.txt)|*.txt"
            };
            try
            {
                if (dialog.ShowDialog() == true)
                {
                    var fileName = dialog.FileName;
                    var date = DateTime.Now;
                    var text = "\nПроверка безопасности "
                               + date
                               + "\nСтатус интернет-соединения "
                               + (_internetIsActive ? "Активно" : "Не активно")
                               + "\n\n Список анивирусов:\n";

                    bool tempStatus;
                    foreach (var key in _antivirusList.Keys)
                    {
                        _antivirusList.TryGetValue(key, out tempStatus);
                        text += key + "\t" + (tempStatus ? "Активен" : "Не активен") + "\n";
                    }

                    text += "\n Список межсетевых экранов:\n";
                    foreach (var key in _firewallList.Keys)
                    {
                        _firewallList.TryGetValue(key, out tempStatus);
                        text += key + "\t" + (tempStatus ? "Активен" : "Не активен") + "\n";
                    }

                    using StreamWriter file = new(fileName, append: true);
                    file.Write(text);
                }
            }
            catch
            {
                MessageBox.Show("Не удалось сохранить данные в файл");
            }

        }
    }
}
