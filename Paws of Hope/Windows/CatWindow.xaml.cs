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
    /// Логика взаимодействия для CatWindow.xaml
    /// </summary>
    public partial class CatWindow : Window
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

        public CatWindow()
        {
            InitializeComponent();

            var animalShelter = AppDate.GetAllAnimalShelter();
            animalShelter.Insert(0, "Все приюты");
            cbShelter.ItemsSource = animalShelter;

            var gender = AppDate.GetAllGender();
            gender.Insert(0, "По умолчанию");
            cbGender.ItemsSource = gender;

            cbAge.ItemsSource = listAge;
            cbAge.SelectedIndex = 0;

            Filter();
        }

        private void Filter()
        {
            if (listCat is null)
                return;

            petList = AppDate.Context.VW_PetTutor.Where(i => i.TypePetID == 2).ToList();
            petList = petList.Where(i => i.NamePet.ToLower().Contains(tbSearch.Text.ToLower()) ||
                            i.AnimalShelteFullName.ToLower().Contains(tbSearch.Text.ToLower())).ToList();
            TotalPet = AppDate.GetAllPet().Count;

            switch (cbAge.SelectedIndex)
            {
                case 0:
                    petList = petList.OrderBy(i => i.NamePet).ToList();
                    break;
                case 1:
                    petList = petList.OrderBy(i => Convert.ToInt32(i.AgePet) <= 3).ToList();
                    break;
                case 2:
                    petList = petList.OrderBy(i => Convert.ToInt32(i.AgePet) >= 4 & Convert.ToInt32(i.AgePet) <= 8).ToList();
                    break;
                case 3:
                    petList = petList.OrderBy(i => Convert.ToInt32(i.AgePet) >= 9).ToList();
                    break;
                default:
                    petList = petList.OrderBy(i => i.NamePet).ToList();
                    break;
            }

            listCat.ItemsSource = petList;

            UpdateItemAmountText();
        }

        private void UpdateItemAmountText()
        {
            txtCountProd.Text = $"{petList.Count} из {TotalPet}";
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddPetWindow addPetWindow = new AddPetWindow();
            this.Opacity = 0.2;
            addPetWindow.ShowDialog();
            this.Opacity = 1;
            Filter();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (listCat.SelectedItem is EF.Pet)
            {
                try
                {
                    var item = listCat.SelectedItem as EF.Pet;
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

        private void listCat_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var editCat = new EF.Pet();

            if (listCat.SelectedItem is EF.Pet)
            {
                editCat = listCat.SelectedItem as EF.Pet;
            }
            InformationPetWindow informationPetWindow = new InformationPetWindow(editCat);
            this.Opacity = 0.2;
            informationPetWindow.ShowDialog();
            this.Opacity = 1;
            Filter();
        }

        private void txtBackMainWin_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void txtExit_MouseUp(object sender, MouseButtonEventArgs e)
        {
            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
            this.Close();
        }

        private void listCat_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (listCat.SelectedItem is EF.Pet)
                {
                    try
                    {
                        var item = listCat.SelectedItem as EF.Pet;
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
    }
}
