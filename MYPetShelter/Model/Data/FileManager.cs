using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data
{

    public abstract class FileManager
    {
        public abstract string FileExtension { get; }

        public abstract void Save<T>(string path, T data);

        public abstract T Load<T>(string path);

        protected bool FileExists(string path)
        {
            string fullPath = path + FileExtension;
            return File.Exists(fullPath);
        }

        protected string GetFullPath(string path) => path + FileExtension;

        protected void BackupIfExists(string path)
        {
            string fullPath = GetFullPath(path);
            if (File.Exists(fullPath))
            {
                string backupPath = fullPath + ".backup";
                File.Copy(fullPath, backupPath, true);
            }
        }
    }
}
