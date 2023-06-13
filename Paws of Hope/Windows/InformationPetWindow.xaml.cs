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
        EF.VW_PetTutor editPet = new EF.VW_PetTutor();


        public InformationPetWindow(EF.VW_PetTutor pet)
        {
            InitializeComponent();

            editPet = pet;
            //add combobox
            cbAnimalShelter.ItemsSource = AppDate.context.AnimalShelter.ToList();
            cbAnimalShelter.DisplayMemberPath = "NameAnimalShelter";

            cbGender.ItemsSource = AppDate.context.Gender.ToList();
            cbGender.DisplayMemberPath = "NameGender";

            cbPetHoz.ItemsSource = AppDate.context.Client.ToList();
            cbPetHoz.DisplayMemberPath = "LastName";

            cbTypePet.ItemsSource = AppDate.context.TypePet.ToList();
            cbTypePet.DisplayMemberPath = "NameTypePet";

            cbTutor.ItemsSource = AppDate.context.Tutor.ToList();
            cbTutor.DisplayMemberPath = "LastName";

            cbSizePet.ItemsSource = AppDate.context.SizePet.ToList();
            cbSizePet.DisplayMemberPath = "NameSizePet";

            //get value
            txtPetName.Text = editPet.NamePet;
            cbGender.SelectedIndex = editPet.IDGender - 1;
            cbPetHoz.SelectedIndex = (int)(editPet.ClientID - 1);
            if (pet.TypePetID == 1)
            {
                cbSizePet.SelectedIndex = (int)(editPet.IDSizePet - 1);
            }
            cbTypePet.SelectedIndex = editPet.TypePetID - 1;
        }

        private void btnEditPet_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBackPet_Click(object sender, RoutedEventArgs e)
        {
            if (editPet.TypePetID == 2)
            {
                CatWindow catWindow = new CatWindow();
                catWindow.Show();
                this.Close();
            }
            else if (editPet.TypePetID == 1)
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
