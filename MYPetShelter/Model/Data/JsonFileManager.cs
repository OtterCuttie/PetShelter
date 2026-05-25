using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Model.Data
{
    public class JsonFileManager : FileManager
    {
        public override string FileExtension => ".json";

        private readonly JsonSerializerOptions _options;

        public JsonFileManager()
        {
            _options = new JsonSerializerOptions
            {
                WriteIndented = true,  // Красивое форматирование
                PropertyNamePolicy = JsonNamingPolicy.CamelCase,  // camelCase для JSON
                IncludeFields = false,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
        }

        public override void Save<T>(string path, T data)
        {
            try
            {
                BackupIfExists(path);
                string fullPath = GetFullPath(path);
                string jsonString = System.Text.Json.JsonSerializer.Serialize(data, _options);
                File.WriteAllText(fullPath, jsonString, System.Text.Encoding.UTF8);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Ошибка сохранения JSON: {ex.Message}", ex);
            }
        }

        public override T Load<T>(string path)
        {
            try
            {
                string fullPath = GetFullPath(path);
                if (!File.Exists(fullPath))
                    return default(T);

                string jsonString = File.ReadAllText(fullPath, System.Text.Encoding.UTF8);
                return System.Text.Json.JsonSerializer.Deserialize<T>(jsonString, _options);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Ошибка загрузки JSON: {ex.Message}", ex);
            }
        }

        /// Специальный метод для сохранения коллекции приютов
        public void SaveShelters(string path, List<Shelter> shelters)
        {
            Save(path, shelters);
        }

        /// Загрузка коллекции приютов
        public List<Shelter> LoadShelters(string path)
        {
            return Load<List<Shelter>>(path) ?? new List<Shelter>();
        }
    }
}
