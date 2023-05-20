using Microsoft.Win32;
using Paws_of_Hope.Class;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Paws_of_Hope.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddPetWindow.xaml
    /// </summary>
    public partial class AddPetWindow : Window
    {
        EF.Pet editPet = new EF.Pet();
        bool isEdit = true; // изменяем или добавляем пользователя
        string pathPhoto = null; // Для сохранения пути к изображению

        public AddPetWindow()
        {
            InitializeComponent();

            var gender = AppDate.GetAllGender();
            gender.Insert(0, "Выберите пол");
            cbGender.ItemsSource = gender;

            var size = AppDate.GetAllSize();
            size.Insert(0, "Выберите размер");
            cbSizePet.ItemsSource = size;

            var tutor = AppDate.GetAllTutor();
            cbTutor.ItemsSource = tutor;

            var client = AppDate.GetAllClient();
            cbPetHoz.ItemsSource = client;

            var animalShelter = AppDate.GetAllAnimalShelter();
            animalShelter.Insert(0, "Выберите приют");
            cbAnimalShelter.ItemsSource = animalShelter;

            var typePet = AppDate.GetAllTypePet();
            typePet.Insert(0, "Выберите тип питомца");
            cbTypePet.ItemsSource = typePet;

            isEdit = false;
        }

        private void txtPetName_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox instance = (TextBox)sender;
            if (instance.Text == instance.Tag.ToString())
                instance.Text = "";
        }

        private void txtPetName_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox instance = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(instance.Text))
                instance.Text = instance.Tag.ToString();
        }

        private void txtPetAge_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox instance = (TextBox)sender;
            if (instance.Text == instance.Tag.ToString())
                instance.Text = "";
        }

        private void txtPetAge_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox instance = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(instance.Text))
                instance.Text = instance.Tag.ToString();
        }

        private void txtPetDisas_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox instance = (TextBox)sender;
            if (instance.Text == instance.Tag.ToString())
                instance.Text = "";
        }

        private void txtPetDisas_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox instance = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(instance.Text))
                instance.Text = instance.Tag.ToString();
        }

        private void txtPetHistory_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox instance = (TextBox)sender;
            if (instance.Text == instance.Tag.ToString())
                instance.Text = "";
        }

        private void txtPetHistory_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox instance = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(instance.Text))
                instance.Text = instance.Tag.ToString();
        }

        private void btnAddPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                MainPhoto.Source = new BitmapImage(new Uri(openFileDialog.FileName));

                pathPhoto = openFileDialog.FileName;
            }
        }

        private void btnBackPet_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAddPet_Click(object sender, RoutedEventArgs e)
        {
            //Валидация
            #region
            //Проверка на пустоту
            if (string.IsNullOrWhiteSpace(txtPetName.Text))
            {
                MessageBox.Show("Поле Кличка не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPetAge.Text))
            {
                MessageBox.Show("Поле Возраст не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            //Проверка на количество символов

            if (txtPetName.Text.Length > 100)
            {
                MessageBox.Show("В поле Кличка недопустимое количество символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            #endregion

            //Проверка на ошибки в БД
            if (isEdit)
            {
                try
                {
                    //изменение читателя
                    editPet.NamePet = txtPetName.Text;
                    editPet.Diseases = txtPetDisas.Text;
                    editPet.History = txtPetHistory.Text;
                    editPet.Age = Convert.ToInt32(txtPetAge.Text);
                    editPet.IDTypePet = cbTypePet.SelectedIndex + 1;
                    editPet.IDSizePet = cbSizePet.SelectedIndex + 1;
                    editPet.IDClient = cbPetHoz.SelectedIndex + 1;
                    editPet.IDGender = cbGender.SelectedIndex + 1;

                    if (pathPhoto != null)
                    {
                       // editPet.Photo = File.ReadAllBytes(pathPhoto);
                    }

                    AppDate.Context.SaveChanges();
                    MessageBox.Show("Данные питомца успешно изменены", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            else
            {
                try
                {
                    var resultClick = MessageBox.Show("Вы уверены?", "Подтвердите добавление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (resultClick == MessageBoxResult.Yes)
                    {
                        //Добавление нового питомца
                        EF.Pet pet = new EF.Pet();
                        pet.NamePet = txtPetName.Text;
                        pet.Diseases = txtPetDisas.Text;
                        pet.History = txtPetHistory.Text;
                        pet.Age = Convert.ToInt32(txtPetAge.Text);
                        pet.IDTypePet = cbTypePet.SelectedIndex + 1;
                        pet.IDSizePet = cbSizePet.SelectedIndex + 1;
                        pet.IDClient = cbPetHoz.SelectedIndex + 1;
                        pet.IDGender = cbGender.SelectedIndex + 1;

                        if (pathPhoto != null)
                        {
                            //pet.PhotoPath = File.ReadAllBytes(pathPhoto);
                        }


                        AppDate.Context.Pet.Add(pet);
                        AppDate.Context.SaveChanges();
                        MessageBox.Show("Питомец успешно добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
