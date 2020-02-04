using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Sta.SwitchControllerLib;

namespace Sta.PokemonMacroExecutor
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private SwitchController m_controller = new SwitchController();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var ports = SerialPort.GetPortNames();
            ComTextBox.Text = ports.FirstOrDefault();
            if (ports.Any())
            {
                Connect();
            }

            DateSelector.SelectedDate = DateTime.Now;
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            Disconnect();
            Connect();
        }

        private void DisconnectButton_Click(object sender, RoutedEventArgs e)
        {
            Disconnect();
        }

        private void Connect()
        {
            try
            {
                m_controller.Connect(ComTextBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Disconnect()
        {
            m_controller.Disconnect();
        }

        private void AButton_Click(object sender, RoutedEventArgs e)
        {
            m_controller.PushA(50, 50);
        }

        private void BButton_Click(object sender, RoutedEventArgs e)
        {
            m_controller.PushB(50, 50);
        }

        private async void PlusDaysExecuteButton_Click(object sender, RoutedEventArgs e)
        {
            var date = DateSelector.SelectedDate;
            if (!date.HasValue)
            {
                return;
            }

            int days = 0;
            if (!int.TryParse(PlusDaysTextBox.Text, out days))
            {
                return;
            }

            DateSelector.IsEnabled = false;
            PlusDaysTextBox.IsEnabled = false;
            ExecutePlusDaysButton.IsEnabled = false;
            await Task.Run(() => ExecutePlusDays(date.Value, days));
            DateSelector.IsEnabled = true;
            PlusDaysTextBox.IsEnabled = true;
            ExecutePlusDaysButton.IsEnabled = true;
        }

        private void ExecutePlusDays(DateTime date, int days)
        {
            for (int i = 0; i < days; i++)
            {
                if (date.Year == 2060 && date.Month == 12 && date.Day == 31)
                {
                    return;
                }

                int previousYear = date.Year;

                date += TimeSpan.FromDays(1);

                if (previousYear < date.Year)
                {
                    // 日月年を変更
                    m_controller.PushA(50, 125);
                    m_controller.PushLeft(50, 75);
                    m_controller.PushLeft(50, 75);
                    m_controller.PushLeft(50, 75);
                    m_controller.PushUp(50, 75);

                    m_controller.PushLeft(50, 75);
                    m_controller.PushUp(50, 75);

                    m_controller.PushLeft(50, 75);
                    m_controller.PushUp(50, 75);

                    m_controller.PushA(50, 75);
                    m_controller.PushA(50, 75);

                    m_controller.PushA(50, 75);
                    m_controller.PushA(50, 75);
                    m_controller.PushA(50, 75);
                    m_controller.PushA(50, 125);
                    continue;
                }

                if (date.Day == 1)
                {
                    // 日月を変更
                    m_controller.PushA(50, 125);
                    m_controller.PushLeft(50, 75);
                    m_controller.PushLeft(50, 75);
                    m_controller.PushLeft(50, 75);
                    m_controller.PushUp(50, 75);

                    m_controller.PushLeft(50, 75);
                    m_controller.PushUp(50, 75);

                    m_controller.PushA(50, 75);

                    m_controller.PushA(50, 75);
                    m_controller.PushA(50, 75);
                    m_controller.PushA(50, 75);
                    m_controller.PushA(50, 125);
                    continue;
                }

                // 日を変更
                m_controller.PushA(50, 125);
                m_controller.PushLeft(50, 75);
                m_controller.PushLeft(50, 75);
                m_controller.PushLeft(50, 75);
                m_controller.PushUp(50, 75);

                m_controller.PushA(50, 75);
                m_controller.PushA(50, 75);
                m_controller.PushA(50, 75);
                m_controller.PushA(50, 125);
            }
        }

        private void UpButton_Click(object sender, RoutedEventArgs e)
        {
            m_controller.PushUp(50, 75);
        }
        private void RightButton_Click(object sender, RoutedEventArgs e)
        {
            m_controller.PushRight(50, 75);
        }
        private void DownButton_Click(object sender, RoutedEventArgs e)
        {
            m_controller.PushDown(50, 75);
        }
        private void LeftButton_Click(object sender, RoutedEventArgs e)
        {
            m_controller.PushLeft(50, 75);
        }
    }
}
