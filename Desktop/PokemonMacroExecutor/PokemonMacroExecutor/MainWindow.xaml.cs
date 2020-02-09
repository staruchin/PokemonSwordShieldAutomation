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
        private Macro m_macro = null;

        public MainWindow()
        {
            InitializeComponent();

            m_macro = new Macro(m_controller);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var ports = SerialPort.GetPortNames();
            ComTextBox.Text = ports.FirstOrDefault();
            if (ports.Any())
            {
                Connect();
            }

            InitialDatePicker.SelectedDate = DateTime.Now;
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
        
        private void XButton_Click(object sender, RoutedEventArgs e)
        {
            m_controller.PushX(50, 50);
        }

        private void YButton_Click(object sender, RoutedEventArgs e)
        {
            m_controller.PushY(50, 50);
        }

        private void RButton_Click(object sender, RoutedEventArgs e)
        {
            m_controller.PushR(50, 50);
        }

        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
            m_controller.PushPlus(50, 50);
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            m_controller.PushHome(50, 50);
        }

        private void CaptureButton_Click(object sender, RoutedEventArgs e)
        {
            m_controller.PushCapture(50, 50);
        }

        private void UpButton_Click(object sender, RoutedEventArgs e)
        {
            m_controller.PushUp(50, 50);
        }

        private void RightButton_Click(object sender, RoutedEventArgs e)
        {
            m_controller.PushRight(50, 50);
        }

        private void DownButton_Click(object sender, RoutedEventArgs e)
        {
            m_controller.PushDown(50, 50);
        }

        private void LeftButton_Click(object sender, RoutedEventArgs e)
        {
            m_controller.PushLeft(50, 50);
        }

        private void DisableUI()
        {
            EnableOrDisableUI(false);
        }

        private void EnableUI()
        {
            EnableOrDisableUI(true);
        }

        private void EnableOrDisableUI(bool enabled)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(this); i++)
            {
                var ui = VisualTreeHelper.GetChild(this, i) as UIElement;
                if (ui != null)
                {
                    ui.IsEnabled = enabled;
                }
            }
        }

        private async void IncreaseDateButton_Click(object sender, RoutedEventArgs e)
        {
            var date = InitialDatePicker.SelectedDate;
            if (!date.HasValue)
            {
                return;
            }

            int days = 0;
            if (!int.TryParse(IncrementalDaysTextBox.Text, out days))
            {
                return;
            }

            await ExecuteAsync(() => m_macro.IncreaseDate(date.Value, days));
            InitialDatePicker.SelectedDate = date.Value + TimeSpan.FromDays(days);
        }

        

        private async void IncreaseDateByThreeDaysButton_Click(object sender, RoutedEventArgs e)
        {
            var date = InitialDatePicker.SelectedDate;
            if (!date.HasValue)
            {
                return;
            }

            await ExecuteAsync(() => m_macro.IncreaseDateByThreeDays(date.Value));
            InitialDatePicker.SelectedDate = date.Value + TimeSpan.FromDays(3);
        }

        private async void ResetAndIncreaseDateByThreeDaysButton_Click(object sender, RoutedEventArgs e)
        {
            var date = InitialDatePicker.SelectedDate;
            if (!date.HasValue)
            {
                return;
            }

            await ExecuteAsync(() => m_macro.ResetAndIncreaseDateByThreeDays(date.Value));
            InitialDatePicker.SelectedDate = date.Value + TimeSpan.FromDays(3);
        }

        private async void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            await ExecuteAsync(() => m_macro.Reset());
        }

        private async void LotoIDButton_Click(object sender, RoutedEventArgs e)
        {
            var date = InitialDatePicker.SelectedDate;
            if (!date.HasValue)
            {
                return;
            }

            await ExecuteAsync(() => m_macro.DrawLotoID(date.Value));
        }

        private async Task ExecuteAsync(Action action)
        {
            DisableUI();
            await Task.Run(action);
            EnableUI();
        }
    }
}
