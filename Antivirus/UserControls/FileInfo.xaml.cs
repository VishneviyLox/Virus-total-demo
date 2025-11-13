using Antivirus.Services;
using Antivirus.View;
using Antivirus.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Antivirus.UserControls
{
    /// <summary>
    /// Логика взаимодействия для FileInfo.xaml
    /// </summary>
    public partial class FileInfo : UserControl
    {
        public FileInfo()
        {
            InitializeComponent();
        }

        void RestartClick(object obj, RoutedEventArgs e)
        {
            WindowService.Instance.Show<MainWindowView, MainWindowViewModel>();
        }
    }
}
