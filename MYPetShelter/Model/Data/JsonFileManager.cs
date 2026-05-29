using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Model.Data
{
    public class JsonFileManager : FileManager
    {
        public JsonFileManager(string name)
            : base(name)
        {

        }
        public JsonFileManager(string name,string folderPath,string fileName,string fileExtension): base(name, folderPath, fileName, "json")
        {

        }

        public override void Serialize(List<Shelter<Pet>> shelters)
        {
            if (shelters == null)
                return;

            JArray sheltersArray = new();

            foreach (var shelter in shelters)
            {
                JObject shelterObject = new();

                shelterObject["Name"] =shelter.Name;

                shelterObject["Capacity"] =shelter.Capacity;

                shelterObject["HasOpenArea"] =shelter.HasOpenArea;

                JArray petsArray = new();

                foreach (var pet in shelter.GetPets())
                {
                    JObject petObject =JObject.FromObject(pet);

                    // сохраняем тип животного
                    petObject["Type"] =pet.GetType().Name;

                    petsArray.Add(petObject);
                }

                shelterObject["Pets"] =petsArray;

                sheltersArray.Add(shelterObject);
            }

            Directory.CreateDirectory(FolderPath);

            File.WriteAllText(FullPath,sheltersArray.ToString());
        }

        public override List<Shelter<Pet>>
            Deserialize()
        {
            if (!File.Exists(FullPath))return null;

            string json =File.ReadAllText(FullPath);

            JArray sheltersArray =JArray.Parse(json);

            List<Shelter<Pet>> shelters =new();

            foreach (JObject shelterObject in sheltersArray)
            {
                Shelter<Pet> shelter =new Shelter<Pet>(shelterObject["Name"].ToString(), (int)shelterObject["Capacity"], (bool)shelterObject["HasOpenArea"]);

                JArray petsArray =(JArray)shelterObject["Pets"];

                foreach (JObject petObject in petsArray)
                {
                    string type =petObject["Type"]?.ToString();

                    if (string.IsNullOrWhiteSpace(type))
                        continue;

                    // удаляем Type перед созданием объекта
                    petObject.Remove("Type");

                    Pet pet = null;

                    switch (type)
                    {
                        case "Cat":
                            pet =petObject.ToObject<Cat>();
                            break;

                        case "Dog":
                            pet =petObject.ToObject<Dog>();
                            break;

                        case "Rabbit":
                            pet =petObject.ToObject<Rabbit>();
                            break;

                        case "Fox":
                            pet =petObject.ToObject<Fox>();
                            break;

                        case "Parrot":
                            pet =petObject.ToObject<Parrot>();
                            break;

                        case "Raccoon":
                            pet =petObject.ToObject<Raccoon>();
                            break;
                    }

                    if (pet != null)
                    {
                        shelter.AddPet(pet);
                    }
                }

                shelters.Add(shelter);
            }
            return shelters;
        }
    }
}
