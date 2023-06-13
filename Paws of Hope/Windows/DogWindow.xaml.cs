using Paws_of_Hope.Class;
using Paws_of_Hope.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Paws_of_Hope.Windows
{
    public partial class DogWindow : Window
    {
        private int TotalPet = 0;

        List<string> listAge = new List<string>()
        {
            "По умолчанию",
            "Молодой",
            "Взрослый",
            "Старый"
        };

        public DogWindow()
        {
            InitializeComponent();
            

            var animalShelter = AppDate.GetAllAnimalShelter();
            animalShelter.Insert(0, "Все приюты");
            cbShelter.ItemsSource = animalShelter;
            cbShelter.SelectedIndex = 0;

            var gender = AppDate.GetAllGender();
            gender.Insert(0, "По умолчанию");
            cbGender.ItemsSource = gender;
            cbGender.SelectedIndex = 0;

            cbAge.ItemsSource = listAge;
            cbAge.SelectedIndex = 0;

            var size = AppDate.GetAllSize();
            size.Insert(0, "По умолчанию");
            cbSize.ItemsSource = size;
            cbSize.SelectedIndex = 0;

            Filter();
        }

        private void Filter()
        {
            if (listDog is null)
                return;

            List<VW_PetTutor> petList = new List<VW_PetTutor>();

            petList = AppDate.context.VW_PetTutor.Where(i => i.TypePetID == 1).ToList();

            // Поиск 
            if (tbSearch.Text != "Введите имя")
            {
                petList = petList.Where(i => i.NamePet.ToLower().Contains(tbSearch.Text.ToLower())).ToList();
            }
            TotalPet = AppDate.GetAllPet().Count;

            // Сортировка
            switch (cbAge.SelectedIndex)
            {
                case 1:
                    petList = petList.OrderBy(i => i.IDPet).ToList();
                    break;
                case 2:
                    petList = petList.OrderBy(i => i.AgePet <= 3).ToList();
                    break;
                case 3:
                    petList = petList.OrderBy(i => i.AgePet >= 4 & i.AgePet <= 8).ToList();
                    break;
                case 4:
                    petList = petList.OrderBy(i => i.AgePet >= 9).ToList();
                    break;
                default:
                    petList = petList.OrderBy(i => i.IDPet).ToList();
                    break;
            }

            if (cbGender.SelectedIndex != 0)
            {
                petList = petList.Where(i => i.IDGender == cbGender.SelectedIndex & i.TypePetID == 1).ToList();
            }

            if (cbShelter.SelectedIndex != 0)
            {
                petList = petList.Where(i => i.AnimalShelteFullName == cbShelter.Text & i.TypePetID == 1).ToList();
            }

            if (cbSize.SelectedIndex != 0)
            {
                petList = petList.Where(i => i.IDSizePet == cbSize.SelectedIndex & i.TypePetID == 1).ToList();
            }

            listDog.ItemsSource = petList;

            txtCountProd.Text = $"{petList.Count} из {TotalPet}";
        }


        private void txtExit_MouseUp(object sender, MouseButtonEventArgs e)
        {
            CurrentUser.FullName = "";
            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
            this.Close();
        }

        private void txtBackMainWin_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void listDog_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var editDog = new VW_PetTutor();

            if (listDog.SelectedItem is VW_PetTutor)
            {
                editDog = listDog.SelectedItem as VW_PetTutor;
            }
            InformationPetWindow informationPetWindow = new InformationPetWindow(editDog);
            this.Opacity = 0.2;
            informationPetWindow.ShowDialog();
            this.Opacity = 1;
            Filter();
        }

        private void listDog_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (listDog.SelectedItem is VW_PetTutor)
                {
                    try
                    {
                        var item = listDog.SelectedItem as VW_PetTutor;
                        var resultClick = MessageBox.Show("Вы уверены?", "Подтвердите удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (resultClick == MessageBoxResult.Yes)
                        {
                            AppDate.context.VW_PetTutor.Remove(item);
                            AppDate.context.SaveChanges();
                            MessageBox.Show("Питомец успешно удален!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                            Filter();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }

            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (listDog.SelectedItem is VW_PetTutor)
            {
                try
                {
                    var item = listDog.SelectedItem as VW_PetTutor;
                    var resultClick = MessageBox.Show("Вы уверены?", "Подтвердите удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (resultClick == MessageBoxResult.Yes)
                    {
                        AppDate.context.VW_PetTutor.Remove(item);
                        AppDate.context.SaveChanges();
                        MessageBox.Show("Питомец успешно удален!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        Filter();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddPetWindow addPetWindow = new AddPetWindow();
            this.Opacity = 0.2;
            addPetWindow.ShowDialog();
            this.Opacity = 1;
            Filter();
        }

        private void tbSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox instance = (TextBox)sender;
            if (instance.Text == instance.Tag.ToString())
            {
                instance.Text = "";
                instance.Foreground = Brushes.Black;
            }
        }

        private void tbSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox instance = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(instance.Text))
            {
                instance.Text = instance.Tag.ToString();
                instance.Foreground = Brushes.LightGray;
            }
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void cbShelter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void cbGender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void cbAge_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void cbSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Filter();
        }
    }
}
