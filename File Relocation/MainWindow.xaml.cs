using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using Ookii.Dialogs.Wpf;

namespace FileRenamer
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
           
        }

        private void btnSelectSource_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new VistaFolderBrowserDialog();
            if ((bool)dialog.ShowDialog(this))
            {
                txtSource.Text = dialog.SelectedPath;

                if (string.IsNullOrEmpty(txtDestination.Text))
                {
                    txtDestination.Text = dialog.SelectedPath;
                }
            }
        }


        private void btnSelectDestination_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new VistaFolderBrowserDialog();
            if ((bool)dialog.ShowDialog(this))
            {
                txtDestination.Text = dialog.SelectedPath;
            }
        }

        private void btnProcess_Click(object sender, RoutedEventArgs e)
        {
            string sourceDir = txtSource.Text;
            string destinationDir = txtDestination.Text;

            if (string.IsNullOrEmpty(sourceDir) || string.IsNullOrEmpty(destinationDir))
            {
                MessageBox.Show("Please select both source and destination directories.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                var directories = Directory.GetDirectories(sourceDir);

                foreach (var dir in directories)
                {
                    var files = Directory.GetFiles(dir);
                    if (files.Length == 1)
                    {
                        var folderName = new DirectoryInfo(dir).Name;
                        var newFileName = folderName.Replace("_", ",").Replace("-", ",");

                        var fileExtension = Path.GetExtension(files[0]);
                        var newFilePath = Path.Combine(destinationDir, newFileName + fileExtension);

                        File.Move(files[0], newFilePath);
                    }
                }

                MessageBox.Show("Files processed successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        
        //--->
    }
}
