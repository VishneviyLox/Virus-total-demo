using Antivirus.Services;
using Antivirus.View;
using Antivirus.ViewModel;
using System.Windows;

namespace Antivirus
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowService.Instance.Show<MainWindowView, MainWindowViewModel>();
        }

        private async void TestsClick(object sender, RoutedEventArgs e)
        {
            VirusTotalService virusTotalService = new VirusTotalService();
            var dialog = new Microsoft.Win32.OpenFileDialog();

            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                // Open document
                string fileName = dialog.FileName;
                string scanResult = await virusTotalService.ScanFile(fileName);
            }
        }
    }
}