using System;
using System.Windows;

namespace Sta.PokemonMacroExecutor2.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeWindowSize();
        }

        private void InitializeWindowSize()
        {
            double clientWidth = Properties.Settings.Default.ImageViewerAreaWidth / 3d * 4d;
            double clientHeight = Properties.Settings.Default.ImageViewerAreaHeight / 3d * 5d;

            double borderWidth = SystemParameters.ResizeFrameVerticalBorderWidth;
            double borderHeight = SystemParameters.ResizeFrameHorizontalBorderHeight;
            double captionHeight = SystemParameters.CaptionHeight;

            Width = clientWidth + borderWidth + borderWidth;
            Height = clientHeight + borderHeight + borderHeight + captionHeight;
        }
    }
}
