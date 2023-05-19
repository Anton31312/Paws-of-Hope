using Paws_of_Hope.Class;
using Paws_of_Hope.EF;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для DogWindow.xaml
    /// </summary>
    public partial class DogWindow : Window
    {

        List<Pet> petList = new List<Pet>();
        List<string> listAnimalShleter = new List<string>()
        {
            "По умолчанию",
            "По возрастанию",
            "По убыванию"
        };

        List<string> listGender = new List<string>()
        {
            "По умолчанию",
            "Мужской",
            "Женский"
        };

        List<string> listAge = new List<string>()
        {
            "По умолчанию",
            "Молодой",
            "Взрослый",
            "Старый"
        };

        List<string> listSize = new List<string>()
        {
            "По умолчанию",
            "Маленький",
            "Средний",
            "Большой"
        };

        public DogWindow()
        {
            InitializeComponent();

            cbShelter.ItemsSource = listAnimalShleter;
            cbShelter.SelectedIndex = 0;

            cbGender.ItemsSource = listGender;
            cbGender.SelectedIndex = 0;

            cbAge.ItemsSource = listAge;
            cbAge.SelectedIndex = 0;

            cbSize.ItemsSource = listSize;
            cbSize.SelectedIndex = 0;

            Filter();
        }

        private void Filter()
        {
            petList = AppDate.Context.Pet.Where(i => i.IsActive == true & i.IDTypePet == 1).ToList();
            petList = petList.Where(i => i.NamePet.ToLower().Contains(tbSearch.Text.ToLower())).ToList();

            switch (cbShelter.SelectedIndex)
            {
                case 0:
                    petList = petList.OrderBy(i => i.IDPet).ToList();
                    break;
                case 1:
                    petList = petList.OrderBy(i => i.AnimalShelter).ToList();
                    break;
                case 2:
                    petList = petList.OrderByDescending(i => i.AnimalShelter).ToList();
                    break;
                default:
                    petList = petList.OrderBy(i => i.IDPet).ToList();
                    break;
            }

            switch (cbGender.SelectedIndex)
            {
                case 0:
                    petList = petList.OrderBy(i => i.IDPet).ToList();
                    break;
                case 1:
                    petList = petList.OrderBy(i => i.IDGender == 1).ToList();
                    break;
                case 2:
                    petList = petList.OrderBy(i => i.IDGender == 2).ToList();
                    break;
                default:
                    petList = petList.OrderBy(i => i.IDPet).ToList();
                    break;
            }


            switch (cbAge.SelectedIndex)
            {
                case 0:
                    petList = petList.OrderBy(i => i.IDPet).ToList();
                    break;
                case 1:
                    petList = petList.OrderBy(i => Convert.ToInt32(i.Age) <= 3).ToList();
                    break;
                case 2:
                    petList = petList.OrderBy(i => Convert.ToInt32(i.Age) >= 4 & Convert.ToInt32(i.Age) <= 8).ToList();
                    break;
                case 3:
                    petList = petList.OrderBy(i => Convert.ToInt32(i.Age) >= 9).ToList();
                    break;
                default:
                    petList = petList.OrderBy(i => i.IDPet).ToList();
                    break;
            }

            switch (cbSize.SelectedIndex)
            {
                case 0:
                    petList = petList.OrderBy(i => i.IDPet).ToList();
                    break;
                case 1:
                    petList = petList.OrderBy(i => i.IDSizePet = 1).ToList();
                    break;
                case 2:
                    petList = petList.OrderBy(i => i.IDSizePet = 2).ToList();
                    break;
                case 3:
                    petList = petList.OrderBy(i => i.IDSizePet = 3).ToList();
                    break;
                default:
                    petList = petList.OrderBy(i => i.IDPet).ToList();
                    break;
            }

            listDog.ItemsSource = petList;
        }


        private void txtExit_MouseUp(object sender, MouseButtonEventArgs e)
        {
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
            var editDog = new EF.Pet();

            if (listDog.SelectedItem is EF.Pet)
            {
                editDog = listDog.SelectedItem as EF.Pet;
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
                if (listDog.SelectedItem is EF.Pet)
                {
                    try
                    {
                        var item = listDog.SelectedItem as EF.Pet;
                        var resultClick = MessageBox.Show("Вы уверены?", "Подтвердите удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (resultClick == MessageBoxResult.Yes)
                        {
                            AppDate.Context.Pet.Remove(item);
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
            if (listDog.SelectedItem is EF.Pet)
            {
                try
                {
                    var item = listDog.SelectedItem as EF.Pet;
                    var resultClick = MessageBox.Show("Вы уверены?", "Подтвердите удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (resultClick == MessageBoxResult.Yes)
                    {
                        AppDate.Context.Pet.Remove(item);
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
                instance.Text = "";
        }

        private void tbSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox instance = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(instance.Text))
                instance.Text = instance.Tag.ToString();
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
