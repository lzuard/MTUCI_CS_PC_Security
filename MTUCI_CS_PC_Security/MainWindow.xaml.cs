

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
        public static Brush _redBrush;


        public MainWindow()
        {
            InitializeComponent();

            _greenBrush = new SolidColorBrush(Color.FromRgb(141, 199, 105));
            _redBrush = new SolidColorBrush(Color.FromRgb(235, 108, 108));

            Update();
        }


        private void Update()
        {
            InternetStatusUpdate();
            FireWallStatusUpdate();
            AntivirusCheck();
        }

        private void InternetStatusUpdate ()
        {
            if (Service.IsInternetWorking())
            {
                InternetTitle.Background = _greenBrush;
                SetLabelParams(InternetStatus, _greenBrush, "Активно");
            }
            else
            {
                InternetTitle.Background = _redBrush;
                SetLabelParams(InternetStatus, _greenBrush, "Не активно");
            }
        }

        private void FireWallStatusUpdate()
        {
            if (Service.IsFirewallInstalled())
            {
                SetLabelParams(FireWallInstall,_greenBrush,"Установлен");
                if (Service.IsFireWallWorking())
                {
                    SetLabelParams(FireWallStatus, _greenBrush, "Активен");
                    FireWallTitle.Background = _greenBrush;
                }
                else
                {
                    SetLabelParams(FireWallStatus, _redBrush, "Не активен");
                    FireWallTitle.Background = _redBrush;
                }
            }
            else
            {
                SetLabelParams(FireWallInstall, _redBrush, "Не установлен");
                SetLabelParams(FireWallStatus, _redBrush, "Не активен");
                FireWallTitle.Background = _redBrush;
            }
        }

        private void SetLabelParams(Label label,Brush color, string content)
        {
            label.Background = color;
            label.Content = content;
        }
        private void AntivirusCheck()
        {
            if (Service.IsAntivirusInstalled())
            {
                SetLabelParams(AntivirusInstall,_greenBrush,"Установлен");
                if (Service.IsFireWallWorking())
                {
                    SetLabelParams(AntivirusStatus, _greenBrush, "Активен");
                    AntivirusTitle.Background = _greenBrush;
                }
                else
                {
                    SetLabelParams(AntivirusStatus, _redBrush, "Не активен");
                    AntivirusTitle.Background = _redBrush;
                }
            }
            else
            {
                SetLabelParams(AntivirusInstall, _redBrush, "Не установлен");
                SetLabelParams(AntivirusStatus, _redBrush, "Не активен");
                AntivirusTitle.Background = _redBrush;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }
    }
}
