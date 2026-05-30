using Model;
using Model.Core;
using Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace PetShelter.Wpf
{
    public partial class ResultsWindow : Window
    {
        private List<Pet> _pets;
        private DataManager _dataManager;
        private Shelter<Pet> _shelter;

        // Можно ли добавлять питомцев? (только если выбран конкретный приют)
        private bool CanEdit => _shelter != null && _shelter.Name != "Все приюты";

        public ResultsWindow(List<Pet> pets, DataManager dataManager, Shelter<Pet> shelter)
        // Отфильтрованный список питомцев, Менеджер данных (для добавления/удаления), приют 
        {
            InitializeComponent();

            _pets = pets;
            _dataManager = dataManager;
            _shelter = shelter;

            // Заполняем таблицу питомцами
            dgPets.ItemsSource = _pets;

            // Информация
            string shelterTitle = shelter?.Name ?? "Все приюты";
            txtInfo.Text = $"Приют: {shelterTitle}  |  Количество питомцев: {_pets.Count}";

            // Кнопки редактирования доступны только для одного приюта 
            btnAdd.IsEnabled = CanEdit;
            btnRemove.IsEnabled = CanEdit;
            if (!CanEdit)
                txtInfo.Text += "  (Изменение запрещено)";
        }

        private void BtnAdd_Click(object source, RoutedEventArgs e)
        {
            if (!CanEdit) return;

            // Открываем диалог добавления
            var window = new AddPetDialog();
            if (window.NewPet != null)
            {
                bool res = _dataManager.AddPetToShelter(window.NewPet, _shelter);
                // Добавляем питомца
                if (res)
                {
                    _pets.Add(window.NewPet);
                    UpdateDisplay();       // Обновляем таблицу
                    MessageBox.Show($"Ваш питомец {window.NewPet.Name} добавлен!");
                }
                else   MessageBox.Show("Не удалось добавить Вашего питомца.");
            }
        }

        private void BtnRemove_Click(object source, RoutedEventArgs a)
        {
            if (!CanEdit) return;
            var pet = dgPets.SelectedItem as Pet;
            if (pet == null)
            {
                MessageBox.Show("Выберите питомца, которого Вы хотите удалить"); return;
            }
            var choice = MessageBox.Show($"Удалить {pet.Name}?");
            if (choice == MessageBoxResult.Yes)
            {
                _dataManager.RemovePetFromShelter(pet, _shelter);
                _pets.Remove(pet);
                UpdateDisplay();
                MessageBox.Show("Питомец удалён");
            }
        }

        private void BtnClose_Click(object source, RoutedEventArgs a)
        {
            Close();
        }

        private void UpdateDisplay()
        {
            dgPets.ItemsSource = null;
            dgPets.ItemsSource = _pets;
            txtInfo.Text = $"Приют: {_shelter.Name}  |  Количество питомцев: {_pets.Count}";
        }
    }
}
