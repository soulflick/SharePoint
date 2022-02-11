using SharePoint.Classes;
using System.Windows;
using System.Windows.Controls;

namespace SharePoint.Controls
{
    public partial class DetailImageControl : UserControl
    {
        public DetailImageControl() => InitializeComponent();

        private void btnAddImage_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext == null) return;
            var context = DataContext as ItemDetail;

            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == true)
            {
                string[] files = openFileDialog.FileNames;

                foreach (var path in files)
                {
                    MyPicture pic = new MyPicture(path);
                    if (pic.invalid) return;

                    context.Images.Add(pic);
                }
            }
        }
    }
}
