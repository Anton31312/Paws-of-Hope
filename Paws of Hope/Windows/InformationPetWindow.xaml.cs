using Paws_of_Hope.Class;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Paws_of_Hope.Windows
{
    /// <summary>
    /// Логика взаимодействия для InformationPetWindow.xaml
    /// </summary>
    public partial class InformationPetWindow : Window
    {
        EF.Pet editPet = new EF.Pet();


        public InformationPetWindow(EF.Pet pet)
        {
            InitializeComponent();

            editPet = pet;
            //add combobox
            cbAnimalShelter.ItemsSource = AppDate.Context.AnimalShelter.ToList();
            cbAnimalShelter.DisplayMemberPath = "NameAnimalShelter";

            cbGender.ItemsSource = AppDate.Context.Gender.ToList();
            cbGender.DisplayMemberPath = "NameGender";

            cbPetHoz.ItemsSource = AppDate.Context.Client.ToList();
            cbPetHoz.DisplayMemberPath = "LastName";

            cbTypePet.ItemsSource = AppDate.Context.TypePet.ToList();
            cbTypePet.DisplayMemberPath = "NameTypePet";

            cbTutor.ItemsSource = AppDate.Context.Tutor.ToList();
            cbTutor.DisplayMemberPath = "LastName";

            cbSizePet.ItemsSource = AppDate.Context.SizePet.ToList();
            cbSizePet.DisplayMemberPath = "NameSizePet";

            //get value
            txtPetName.Text = editPet.NamePet;
            cbGender.SelectedIndex = editPet.IDGender - 1;
            //cbPetHoz.SelectedIndex = (int)(editPet.IDClient - 1);
            if (pet.IDTypePet == 1)
            {
                cbSizePet.SelectedIndex = (int)(editPet.IDSizePet - 1);
            }
            cbTypePet.SelectedIndex = editPet.IDTypePet - 1;
        }

        private void btnEditPet_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBackPet_Click(object sender, RoutedEventArgs e)
        {
            if (editPet.IDTypePet != 2)
            {
                CatWindow catWindow = new CatWindow();
                catWindow.Show();
                this.Close();
            }
            else if (editPet.IDTypePet == 1)
            {
                DogWindow dogWindow = new DogWindow();
                dogWindow.Show();
                this.Close();
            }
        }

        private void btnCheckPhoto_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
