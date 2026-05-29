using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Model.Core;
namespace Model.Data
{
    /// Генератор отчетов "Подборка_№Х_от_дата"
    public class ReportGenerator
    {
        private readonly string _reportsFolder;

        public ReportGenerator(
            string reportsFolder = "Reports")
        {
            _reportsFolder = reportsFolder;

            if (!Directory.Exists(_reportsFolder))
            {
                Directory.CreateDirectory(
                    _reportsFolder);
            }
        }

        public void CreateReport(
            List<Pet> pets)
        {
            if (pets == null || !pets.Any())
                return;

            string fileName =
                $"Подборка_{DateTime.Now:dd_MM_yyyy_HH_mm_ss}.txt";

            string fullPath =
                Path.Combine(
                    _reportsFolder,
                    fileName);

            StringBuilder builder = new();

            builder.AppendLine(
                "Отчет по животным");

            builder.AppendLine(
                $"Дата: {DateTime.Now}");

            builder.AppendLine(
                new string('-', 50));

            foreach (var pet in pets)
            {
                builder.AppendLine(
                    $"Тип: {pet.GetType().Name}");

                builder.AppendLine(
                    $"Кличка: {pet.Name}");

                builder.AppendLine(
                    $"Возраст: {pet.Age}");

                builder.AppendLine(
                    $"Вес: {pet.Weight}");

                builder.AppendLine(
                    $"Особые приметы: {pet.SpecialMarks}");

                builder.AppendLine(
                    $"Клаустрофобия: {pet.HasClaustrophobia}");

                // CAT
                if (pet is Cat cat)
                {
                    builder.AppendLine(
                        $"Средняя длина шерсти: " +
                        $"{cat.AverageFurLength}");

                    builder.AppendLine(
                        $"Ловит грызунов: " +
                        $"{cat.CatchesRodentsme}");
                }

                // DOG
                else if (pet is Dog dog)
                {
                    builder.AppendLine(
                        $"Порода: {dog.Breed}");

                    builder.AppendLine(
                        $"Дрессирован: " +
                        $"{dog.IsTrained}");
                }

                // RABBIT
                else if (pet is Rabbit rabbit)
                {
                    builder.AppendLine(
                        $"Шерсть: {rabbit.Fur}");

                    builder.AppendLine(
                        $"Длина ушей: " +
                        $"{rabbit.LenghOfEarsg}");
                }

                // FOX
                else if (pet is Fox fox)
                {
                    builder.AppendLine(
                        $"Охотничьи навыки: " +
                        $"{fox.HuntingSkills}");

                    builder.AppendLine(
                        $"Приручена: " +
                        $"{fox.TamingLevel}");
                }

                // RACCOON
                else if (pet is Raccoon raccoon)
                {
                    builder.AppendLine(
                        $"Уровень разрушения: " +
                        $"{raccoon.DestructionLevel}");

                    builder.AppendLine(
                        $"Ручной: " +
                        $"{raccoon.HandFed}");
                }

                // PARROT
                else if (pet is Parrot parrot)
                {
                    builder.AppendLine(
                        $"Длина крыла: " +
                        $"{parrot.WingLength}");

                    builder.AppendLine(
                        $"Разговаривает: " +
                        $"{parrot.IsTalking}");
                }

                builder.AppendLine(
                    new string('-', 50));
            }

            File.WriteAllText(
                fullPath,
                builder.ToString());
        }
    }
}
