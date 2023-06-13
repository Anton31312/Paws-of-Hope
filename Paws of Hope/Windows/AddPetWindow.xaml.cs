using Microsoft.Win32;
using Paws_of_Hope.Class;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Paws_of_Hope.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddPetWindow.xaml
    /// </summary>
    public partial class AddPetWindow : Window
    {
        EF.VW_PetTutor editPet = new EF.VW_PetTutor();
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
            {
                instance.Text = "";
                instance.Foreground = Brushes.Black;
            }
        }

        private void txtPetName_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox instance = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(instance.Text))
            {
                instance.Text = instance.Tag.ToString();
                instance.Foreground = Brushes.LightGray;
            }
        }

        private void txtPetAge_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox instance = (TextBox)sender;
            if (instance.Text == instance.Tag.ToString())
            {
                instance.Text = "";
                instance.Foreground = Brushes.Black;
            }
        }

        private void txtPetAge_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox instance = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(instance.Text))
            {
                instance.Text = instance.Tag.ToString();
                instance.Foreground = Brushes.LightGray;
            }
        }

        private void txtPetDisas_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox instance = (TextBox)sender;
            if (instance.Text == instance.Tag.ToString())
            {
                instance.Text = "";
                instance.Foreground = Brushes.Black;
            }
        }

        private void txtPetDisas_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox instance = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(instance.Text))
            {
                instance.Text = instance.Tag.ToString();
                instance.Foreground = Brushes.LightGray;
            }
        }

        private void txtPetHistory_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox instance = (TextBox)sender;
            if (instance.Text == instance.Tag.ToString())
            {
                instance.Text = "";
                instance.Foreground = Brushes.Black;
            }
        }

        private void txtPetHistory_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox instance = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(instance.Text))
            {
                instance.Text = instance.Tag.ToString();
                instance.Foreground = Brushes.LightGray;
            }
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
                    editPet.DiseasesPet = txtPetDisas.Text;
                    editPet.HistoryPet = txtPetHistory.Text;
                    editPet.AgePet = Convert.ToInt32(txtPetAge.Text);
                    editPet.TypePetID = cbTypePet.SelectedIndex + 1;
                    editPet.IDSizePet = cbSizePet.SelectedIndex + 1;
                    editPet.ClientID = cbPetHoz.SelectedIndex + 1;
                    editPet.IDGender = cbGender.SelectedIndex + 1;

                    if (pathPhoto != null)
                    {
                       // editPet.Photo = File.ReadAllBytes(pathPhoto);
                    }

                    AppDate.context.SaveChanges();
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
                        EF.VW_PetTutor pet = new EF.VW_PetTutor();
                        pet.NamePet = txtPetName.Text;
                        pet.DiseasesPet = txtPetDisas.Text;
                        pet.HistoryPet = txtPetHistory.Text;
                        pet.AgePet = Convert.ToInt32(txtPetAge.Text);
                        pet.TypePetID = cbTypePet.SelectedIndex + 1;
                        pet.IDSizePet = cbSizePet.SelectedIndex + 1;
                        pet.ClientID = cbPetHoz.SelectedIndex + 1;
                        pet.IDGender = cbGender.SelectedIndex + 1;

                        if (pathPhoto != null)
                        {
                            pet.Photo = File.ReadAllText(pathPhoto);
                        }


                        AppDate.context.VW_PetTutor.Add(pet);
                        AppDate.context.SaveChanges();
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
