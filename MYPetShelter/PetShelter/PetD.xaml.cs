using Model;
using Model.Core;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace PetShelter
{
    public partial class AddPetDialog : Window
    {
        public Pet NewPet { get; private set; }
        public AddPetDialog()
        {
            InitializeComponent();
        }
        // Обработчик кнопки "Добавить"
        private void BtnOk_Click(object source, RoutedEventArgs a)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Кличка вашего питомца: ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);  return;
            }

            string tAge = txtAge.Text;
            bool isAge = int.TryParse(tAge, out int age);
            if (!isAge || age<0)
            {
                MessageBox.Show("Возраст вашего питомца: ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            string tWeight = txtWeight.Text;
            bool isWeight = int.TryParse(tAge, out int weight);
            if (!isWeight || weight <= 0)
            {
                MessageBox.Show("Вес вашего питомца: ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string name = txtName.Text;
            string marks = txtSpecialMarks.Text;
            bool hasClfb = chkClaustrophobia.IsChecked == true;

            string type = (cboxPetType.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (type == "Cat")
                NewPet = new Cat(name, age, weight, marks, hasClaustro);
            else if (type == "Dog")
                NewPet = new Dog(name, age, weight, marks, hasClaustro);
            else if (type == "Rabbit")
                NewPet = new Rabbit(name, age, weight, marks, hasClaustro);
            else if (type == "Fox")
                NewPet = new Fox(name, age, weight, marks, hasClaustro);
            else if (type == "Raccoon")
                NewPet = new Raccoon(name, age, weight, marks, hasClaustro);
            else
                NewPet = new Parrot(name, age, weight, marks, hasClaustro);

            DialogResult = true;
            Close();
        }

        // Обработчик кнопки "Отмена"
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
