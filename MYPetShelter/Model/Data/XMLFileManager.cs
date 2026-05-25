using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data
{
    public class XMLFileManager : FileManager
    {
        public override string FileExtension => ".xml";

        public override void Save<T>(string path, T data)
        {
            try
            {
                BackupIfExists(path);
                string fullPath = GetFullPath(path);

                XMLFileManager xmlSerializer = new XMLFileManager(typeof(T));

                using (FileStream fs = new FileStream(fullPath, FileMode.Create))
                using (StreamWriter writer = new StreamWriter(fs, Encoding.UTF8))
                {
                    xmlSerializer.Serialize(writer, data);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Ошибка сохранения XML: {ex.Message}", ex);
            }
        }

        public override T Load<T>(string path)
        {
            try
            {
                string fullPath = GetFullPath(path);
                if (!File.Exists(fullPath))
                    return default(T);

                XMLFileManager xmlSerializer = new XMLFileManager(typeof(T));

                using (FileStream fs = new FileStream(fullPath, FileMode.Open))
                using (StreamReader reader = new StreamReader(fs, Encoding.UTF8))
                {
                    return (T)xmlSerializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Ошибка загрузки XML: {ex.Message}", ex);
            }
        }

        /// Сохранение с дополнительными настройками для совместимости
        public void SaveWithTypes<T>(string path, T data, Type[] extraTypes)
        {
            try
            {
                BackupIfExists(path);
                string fullPath = GetFullPath(path);

                XMLFileManager xmlSerializer = new XMLFileManager(typeof(T), extraTypes);

                using (FileStream fs = new FileStream(fullPath, FileMode.Create))
                using (StreamWriter writer = new StreamWriter(fs, Encoding.UTF8))
                {
                    xmlSerializer.Serialize(writer, data);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Ошибка сохранения XML с типами: {ex.Message}", ex);
            }
        }
    }
}
