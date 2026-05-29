using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data
{
    public abstract class MyFileManager
    {
        public string Name { get; }

        public string FolderPath { get; private set; }
        public string FileName { get; private set; }
        public string FileExtension { get; private set; }

        public string FullPath
        {
            get
            {
                if (string.IsNullOrEmpty(FolderPath) || string.IsNullOrEmpty(FileName))
                    return string.Empty;

                string ext =string.IsNullOrEmpty(FileExtension)? "": $".{FileExtension}";

                return Path.Combine(FolderPath,FileName + ext);
            }
        }

        protected MyFileManager(string name)
        {
            Name = name;
            FolderPath = string.Empty;
            FileName = string.Empty;
            FileExtension = string.Empty;
        }

        protected MyFileManager(string name,string folderPath,string fileName,string fileExtension = "txt")
        {
            Name = name;
            FolderPath = folderPath;
            FileName = fileName;
            FileExtension = fileExtension;
        }

        public void SelectFolder(string path)
        {
            FolderPath = path;
        }

        public void ChangeFileName(string fileName)
        {
            FileName = fileName;
        }

        public virtual void ChangeFileExtension(string ext)
        {
            FileExtension = ext;
        }

        public void CreateFile()
        {
            if (!File.Exists(FullPath))
            {
                Directory.CreateDirectory(FolderPath);
                using (File.Create(FullPath)) { }
            }
        }

        public void DeleteFile()
        {
            if (File.Exists(FullPath))
                File.Delete(FullPath);
        }
    }
}
