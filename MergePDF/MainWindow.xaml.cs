using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;

namespace MergePDF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<LoadedFile> _loadedFiles;
        public MainWindow()
        {
            InitializeComponent();
            _loadedFiles = new List<LoadedFile>();
        }

        private void OpenFiles_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                DefaultExt = ".pdf",
                Filter = "Pdf Files|*.pdf",
                Multiselect = true
            };

            if (dlg.ShowDialog() == true)
            {
                foreach(string s in dlg.FileNames)
                {
                    _loadedFiles.Add(new LoadedFile(s));
                }
                dgFiles.ItemsSource = _loadedFiles;
                dgFiles.Items.Refresh();
            }
        }

        private void RemoveFiles_Click(object sender, RoutedEventArgs e)
        {
            _loadedFiles.Clear();
            dgFiles.ItemsSource = _loadedFiles;
            dgFiles.Items.Refresh();
        }

        private void MergeFiles_Click(object sender, RoutedEventArgs e)
        {
            if (_loadedFiles.Count() == 0)
            {
                MessageBox.Show("You have to add PDF files to merge.", "Warning");
            }
            else
            {
                SaveFileDialog dlg = new SaveFileDialog
                {
                    Filter = "Pdf Files|*.pdf",
                    RestoreDirectory = true
                };

                string fileName;

                if (dlg.ShowDialog() == true)
                {
                    fileName = dlg.FileName;
                    var filePaths = from file in _loadedFiles select file.FilePath;
                    PdfMerge.Merge(filePaths.ToArray(), dlg.FileName);
                }
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuItemAbout_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Merge PDFs");
            sb.AppendLine($"Version: {Assembly.GetExecutingAssembly().GetName().Version}");
            sb.AppendLine("Contact: bartosz.lampart@gmail.com");
            MessageBox.Show(sb.ToString(), "About");
        }
    }
}
