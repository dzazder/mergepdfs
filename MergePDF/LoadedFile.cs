using System;
using System.Collections.Generic;
using System.Text;

namespace MergePDF
{
    public class LoadedFile
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }

        public LoadedFile() { }

        public LoadedFile(string filePath)
        {
            FilePath = filePath;
            if (filePath.IndexOf("\\") == -1)
            {
                FileName = filePath;
            }
            else
            {
                FileName = filePath.Substring(filePath.LastIndexOf("\\") + 1);
            }
        }

        public LoadedFile(string filePath, string fileName)
        {
            FilePath = filePath;
            FileName = fileName;
        }
    }
}
