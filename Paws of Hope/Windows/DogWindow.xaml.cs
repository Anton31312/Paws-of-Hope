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

        List<VW_PetTutor> petList = new List<VW_PetTutor>();

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
            listDog.ItemsSource = AppDate.Context.VW_PetTutor.ToList();

            var animalShelter = AppDate.GetAllAnimalShelter();
            animalShelter.Insert(0, "Все приюты");
            cbShelter.ItemsSource = animalShelter;
            cbShelter.SelectedIndex = 0;

            var gender = AppDate.GetAllGender();
            gender.Insert(0, "По умолчанию");
            cbGender.ItemsSource = gender;
            cbGender.SelectedIndex = 0;

            cbAge.ItemsSource = listAge;
            cbAge.SelectedIndex = 1;

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

            petList = AppDate.Context.VW_PetTutor.Where(i => i.TypePetID == 1).ToList();
            petList = petList.Where(i => i.NamePet.ToLower().Contains(tbSearch.Text.ToLower())).ToList();
            TotalPet = AppDate.GetAllPet().Count;

            switch (cbAge.SelectedIndex)
            {
                case 1:
                    petList = petList.OrderBy(i => i.IDPet).ToList();
                    break;
                case 2:
                    petList = petList.OrderBy(i => Convert.ToInt32(i.AgePet) <= 3).ToList();
                    break;
                case 3:
                    petList = petList.OrderBy(i => Convert.ToInt32(i.AgePet) >= 4 & Convert.ToInt32(i.AgePet) <= 8).ToList();
                    break;
                case 4:
                    petList = petList.OrderBy(i => Convert.ToInt32(i.AgePet) >= 9).ToList();
                    break;
                default:
                    petList = petList.OrderBy(i => i.IDPet).ToList();
                    break;
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
            var editDog = new EF.VW_PetTutor();

            if (listDog.SelectedItem is EF.VW_PetTutor)
            {
                editDog = listDog.SelectedItem as EF.VW_PetTutor;
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
                if (listDog.SelectedItem is EF.VW_PetTutor)
                {
                    try
                    {
                        var item = listDog.SelectedItem as EF.VW_PetTutor;
                        var resultClick = MessageBox.Show("Вы уверены?", "Подтвердите удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (resultClick == MessageBoxResult.Yes)
                        {
                            AppDate.Context.VW_PetTutor.Remove(item);
                            AppDate.Context.SaveChanges();
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
            if (listDog.SelectedItem is EF.VW_PetTutor)
            {
                try
                {
                    var item = listDog.SelectedItem as EF.VW_PetTutor;
                    var resultClick = MessageBox.Show("Вы уверены?", "Подтвердите удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (resultClick == MessageBoxResult.Yes)
                    {
                        AppDate.Context.VW_PetTutor.Remove(item);
                        AppDate.Context.SaveChanges();
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
    }
}
