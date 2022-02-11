using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;

namespace SharePoint
{
    public partial class MainWindow : Window
    {
        public MainWindow() => InitializeComponent();

        private void OnRegister(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(e.Uri.AbsoluteUri);
            e.Handled = true;
        }
    }
}
