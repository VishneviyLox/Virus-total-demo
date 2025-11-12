using System.Windows;
using System.Windows.Controls;

namespace Antivirus.UserControls
{
    public partial class VirusInfoUserControl : UserControl
    {
        public VirusInfoUserControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty AntivirusNameProperty =
            DependencyProperty.Register("AntivirusName", typeof(string), typeof(VirusInfoUserControl), new PropertyMetadata(""));

        public string AntivirusName
        {
            get => (string)GetValue(AntivirusNameProperty);
            set => SetValue(AntivirusNameProperty, value);
        }

        public static readonly DependencyProperty VirusNameProperty =
           DependencyProperty.Register("VirusName", typeof(string), typeof(VirusInfoUserControl), new PropertyMetadata(""));
        public string VirusName
        {
            get => (string)GetValue(VirusNameProperty);
            set => SetValue(VirusNameProperty, value);
        }
        public static readonly DependencyProperty VirusTypeProperty =
            DependencyProperty.Register("VirusType", typeof(string), typeof(VirusInfoUserControl), new PropertyMetadata(""));
        public string VirusType
        {
            get => (string)GetValue(VirusTypeProperty);
            set => SetValue(VirusTypeProperty, value);
        }
    }
}