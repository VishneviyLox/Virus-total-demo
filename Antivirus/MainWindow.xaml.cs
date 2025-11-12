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
        //Test
    }
}