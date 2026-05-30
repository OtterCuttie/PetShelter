using Model;
using Model.Data;
using PetShelter;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Model.Core;
using System.Collections.Generic;

namespace PetShelter
{
    public partial class MainWindow : Window
    {
        // Поле для хранения менеджера данных
        private DataManager _dataManager;
        private ShelterRepository _shelterRepository;
        private bool _isInitializing = true;

        // Конструктор - вызывается при запуске программы
        public MainWindow()
        {
            InitializeComponent(); // Загружает кнопки и списки из XAML
            var fileManager = new JsonFileManager("Shelters", "Data", "shelters", "json");
            var reportGenerator = new ReportGenerator();
            _dataManager = new DataManager(fileManager, reportGenerator);   //  менеджер данных
            _shelterRepository = new ShelterRepository(_dataManager);
            LoadShelters();
            LoadrprtFormat();
            _isInitializing = false;
        }

        // Загружает список приютов в комбобокс
        private void LoadShelters()
        {
            // Получаем все приюты из репозитория
            var shelters = _shelterRepository.GetAll().ToList();

            // Добавляем пункт "Все приюты" в начало списка
            var allSheltersItem = new Shelter<Pet>("Все приюты", 0, false);
            shelters.Insert(0, allSheltersItem);   //Добавление элемента в нужную позицию со сдвигом остальных.

            // Заполняем выпадающий список
            cboxShelters.ItemsSource = shelters;
            cboxShelters.DisplayMemberPath = "Название"; 
            cboxShelters.SelectedIndex = 0; // Выбираем "Все приюты" автоматически
        }

        // Обработчик кнопки "Показать питомцев"
        private void BtnShow_Animals(object sender, RoutedEventArgs e)
        {
            Shelter<Pet> chkShelter = null;
            if (cboxShelters.SelectedItem == null) chkShelter = null;
            chkShelter = (Shelter<Pet>)cboxShelters.SelectedItem;         // 1.) выбранный приют
            if (chkShelter.Name == "Все приюты") chkShelter = null;

            Type animalType = null;
            if (cboxAnimalTypes.SelectedItem == null) animalType = null;
            var chkType = (ComboBoxItem)cboxAnimalTypes.SelectedItem;         // 2.) выбранный тип животного
            object contentT = chkType.Content;
            string typeText = contentT.ToString();

            if (typeText == "Cat") animalType = typeof(Cat);
            else if (typeText == "Dog") animalType = typeof(Dog);
            else if (typeText == "Rabbit") animalType = typeof(Rabbit);
            else if (typeText == "Fox") animalType = typeof(Fox);
            else if (typeText == "Raccoon") animalType = typeof(Raccoon);
            else if (typeText == "Parrot") animalType = typeof(Parrot);

            // 3.) Проверяем, нужна ли только открытая территория
            bool? onlyOpenArea = null; // bool? позволяет сделать переменную null (сам bool только true/false)
            if (ckOnlyOpenArea.IsChecked == true) onlyOpenArea = true; // если пользователь отметил галочку

            // 4.) фильтрация питомцев
            var filteredPets = _dataManager.GetFilteredPets(chkShelter, animalType, onlyOpenArea);
        }

        // 5.) формат отчёта
        private void LoadReportFormat()
        {
            // Создаём нужный пункт и делаем его выбранным
            ComboBoxItem selectedItem = new ComboBoxItem();

            if (_dataManager.CurrentReportFormat == "json")
                selectedItem.Content = "JSON";
            else
                selectedItem.Content = "XML";

            cboxReportFormat.SelectedItem = selectedItem;
        }
        private void CboxRprtFormat(object sender, SelectionChangedEventArgs e)
        {
            // Пропускаем, если это начальная инициализация
            if (_isInitializing) return;
            string oldFormat = _dataManager.CurrentReportFormat;

            ComboBoxItem selectedItem = (ComboBoxItem)cboxReportFormat.SelectedItem;
            string newFormat = selectedItem.Content.ToString().ToLower();

            if (oldFormat != newFormat)
            {
                CopyAllReports(oldFormat, newFormat);
                _dataManager.CurrentReportFormat = newFormat;
            }
        }
            private void CopyAllReports(string oldFormat, string newFormat)
        {
            string reportsPath = Path.Combine(Directory.GetCurrentDirectory(), "Reports");
            if (!Directory.Exists(reportsPath))  return;
            string[] oldFiles = Directory.GetFiles(reportsPath, $"*.{oldFormat}");
            foreach (string f in oldFiles)
            {
                string text = File.ReadAllText(f);
                string newFile = f.Replace(oldFormat, newFormat);
                File.WriteAllText(newFile, text);
            }
        }

                                                             // 6.) Сохраняем отчёт в файл
            string rprtPath = _dataManager.SaveCurrentReport(filteredPets, chkShelter);
            string fileName = System.IO.Path.GetFileName(rprtPath);

                                                            // 7.) Открываем окно с таблицей результатов
            ResultsWindow resultsWindow = new ResultsWindow(filteredPets, _dataManager, chkShelter);
            resultsWindow.ShowDialog();
        }
    }
}
