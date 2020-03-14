﻿using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
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

        private void btnOpenFiles_Click(object sender, RoutedEventArgs e)
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
            }
        }

        private void btnMerge_Click(object sender, RoutedEventArgs e)
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
}