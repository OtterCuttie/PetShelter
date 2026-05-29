using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Model;
using Model.Data;
using System;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PetShelter.Wpf;

namespace PetShelter
{
    public partial class MainWindow : Window
    {
        // Поле для хранения менеджера данных
        private DataManager _dataManager;

        // Конструктор - вызывается при запуске программы
        public MainWindow()
        {
            InitializeComponent(); // Загружает кнопки и списки из XAML

            // Создаём менеджер данных
            _dataManager = new DataManager();

            // Загружаем приюты в выпадающий список
            LoadShelters();

            // Показываем статус
            statusText.Text = "Program is ready";
        }

        // Загружает список приютов в комбобокс
        private void LoadShelters()
        {
            // Получаем все приюты из репозитория
            var shelters = _dataManager.ShelterRepository.GetAll().ToList();

            // Добавляем пункт "Все приюты" в начало списка
            var allSheltersItem = new Shelter<Pet>("Все приюты", 0, false);
            shelters.Insert(0, allSheltersItem);   //Добавление элемента в нужную позицию со сдвигом остальных.

            // Заполняем выпадающий список
            cmShelters.ItemsSource = shelters;
            cmShelters.SelectedIndex = 0; // Выбираем "Все приюты" автоматически
        }

        // Обработчик кнопки "Показать питомцев"
        private void BtnShow_Animals(object sender, RoutedEventArgs e)
        {
            // 1. Берём то, что выбрал пользователь в выпадающем списке приютов
            var selectedShelter = (Shelter<Pet>)cmShelters.SelectedItem;
            if (selectedShelter.Name == "Все приюты")
                selectedShelter = null; // ищем во всех приютах

            // 2. выбранный тип животного
            var selectedType = (ComboBoxItem)cmAnimalTypes.SelectedItem;
            object content = selectedType.Content;
            string typeText = content.ToString();

            Type animalType = null;
            if (typeText == "Cat")      animalType = typeof(Cat);
            else if (typeText == "Dog")     animalType = typeof(Dog);
            else if (typeText == "Rabbit")      animalType = typeof(Rabbit);
            else if (typeText == "Fox")     animalType = typeof(Fox);
            else if (typeText == "Raccoon")   animalType = typeof(Raccoon);
            else if (typeText == "Parrot")      animalType = typeof(Parrot);
            // Если "All", то animalType остаётся null

            // 3. Проверяем, нужна ли только открытая территория
            bool? onlyOpenArea = null;
            if (chkOnlyOpenArea.IsChecked == true)
            {
                onlyOpenArea = true;
            }

            // 4. Выполняем фильтрацию питомцев
            var filteredPets = _dataManager.GetFilteredPets(
                selectedShelter,
                animalType,
                onlyOpenArea
            );

            // 5. Показываем количество найденных питомцев
            statusText.Text = "Found pets: " + filteredPets.Count;

            // 6. Определяем формат отчёта
            ComboBoxItem selectedFormat = (ComboBoxItem)cmbReportFormat.SelectedItem;
            string formatName = selectedFormat.Content.ToString();

            if (formatName == "JSON")
            {
                _dataManager.CurrentReportFormat = "json";
            }
            else
            {
                _dataManager.CurrentReportFormat = "xml";
            }

            // 7. Сохраняем отчёт в файл
            string reportPath = _dataManager.SaveCurrentReport(filteredPets, selectedShelter);
            string fileName = System.IO.Path.GetFileName(reportPath);
            statusText.Text = "Saved: " + fileName;

            // 8. Открываем окно с таблицей результатов
            ResultsWindow resultsWindow = new ResultsWindow(filteredPets, _dataManager, selectedShelter);
            resultsWindow.Owner = this;
            resultsWindow.ShowDialog();

            // 9. Обновляем статус после закрытия окна
            statusText.Text = "Ready";
        }
    }
}
