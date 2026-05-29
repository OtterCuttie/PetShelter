using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Core;

namespace Model.Data
{
    public abstract class FileManager : MyFileManager
    {
        protected FileManager(string name): base(name)
        {
        }

        protected FileManager(string name,string folderPath,string fileName,string fileExtension): base(name, folderPath, fileName, fileExtension)
        {
        }

        public abstract void Serialize(List<Shelter<Pet>> shelters);

        public abstract List<Shelter<Pet>> Deserialize();
    }
}
