
using Paws_of_Hope.Class;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Paws_of_Hope.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddApplicationWindow.xaml
    /// </summary>
    public partial class AddApplicationWindow : Window
    {

        EF.ExecutedApplication editApplication = new EF.ExecutedApplication();
        bool isEdit = true; // изменяем или добавляем пользователя
        string pathPhoto = null; // Для сохранения пути к изображению

        public AddApplicationWindow()
        {
            InitializeComponent();

            var gender = AppDate.GetAllGender();
            gender.Insert(0, "Выберите пол");
            cbGender.ItemsSource = gender;
            cbGender.SelectedIndex = 0;

            var size = AppDate.GetAllSize();
            size.Insert(0, "Выберите размер");
            cbSizePet.ItemsSource = size;
            cbSizePet.SelectedIndex = 0;

            var client = AppDate.GetAllClient();
            cbHoz.ItemsSource = client;

            var typePet = AppDate.GetAllTypePet();
            typePet.Insert(0, "Выберите тип питомца");
            cbTypePet.ItemsSource = typePet;
            cbTypePet.SelectedIndex = 0;
        }

        public AddApplicationWindow(EF.ExecutedApplication application)
        {
            InitializeComponent();

            //edit combobox
            cbGender.ItemsSource = AppDate.context.Gender.ToList();
            cbGender.DisplayMemberPath = "NameGender";

            //edit TItle and content button
            tbTitle.Text = "Изменить данные заявки";
            btnAddApplication.Content = "Изменить";

            //Get value
            editApplication = application;
     
            cbGender.SelectedIndex = editApplication.Pet.IDGender - 1;
            cbTypePet.SelectedIndex = editApplication.Pet.IDTypePet - 1;
            cbSizePet.SelectedIndex = (int)(editApplication.Pet.IDSizePet - 1);
            cbHoz.SelectedIndex = editApplication.Application.Client.IDClient - 1;
            cbPet.SelectedIndex = editApplication.Pet.IDPet - 1;
            txtAgePet.Text = Convert.ToString(editApplication.Pet.Age);
            txtPetWish.Text = editApplication.Application.Wishes;

            isEdit = true;
        }

        private void txtPetWish_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox instance = (TextBox)sender;
            if (instance.Text == instance.Tag.ToString())
                instance.Text = "";
        }

        private void txtPetWish_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox instance = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(instance.Text))
                instance.Text = instance.Tag.ToString();
        }


        private void txtAgePet_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox instance = (TextBox)sender;
            if (instance.Text == instance.Tag.ToString())
                instance.Text = "";
        }

        private void txtAgePet_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox instance = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(instance.Text))
                instance.Text = instance.Tag.ToString();
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void btnBackApplication_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAddApplication_Click(object sender, RoutedEventArgs e)
        {
            //Проверка на ошибки в БД
            if (isEdit)
            {
                try
                {
                    //изменение читателя
                    editApplication.Pet.IDTypePet = cbTypePet.SelectedIndex + 1;
                    editApplication.Pet.IDSizePet = cbTypePet.SelectedIndex + 1;
                    editApplication.Pet.Age = Convert.ToInt32(txtAgePet.Text);
                    editApplication.Application.Wishes = txtPetWish.Text;
                    editApplication.Pet.IDGender = cbGender.SelectedIndex + 1;
                    editApplication.Pet.IDPet = cbPet.SelectedIndex + 1;
                    editApplication.Application.Client.IDClient = cbHoz.SelectedIndex + 1;

                    AppDate.context.SaveChanges();
                    MessageBox.Show("Данные о заявке успешно изменены", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
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
                        //Добавление нового читателя
                        EF.ExecutedApplication application = new EF.ExecutedApplication();
                        editApplication.Pet.IDTypePet = cbTypePet.SelectedIndex + 1;
                        editApplication.Pet.IDSizePet = cbTypePet.SelectedIndex + 1;
                        editApplication.Pet.Age = Convert.ToInt32(txtAgePet.Text);
                        editApplication.Pet.IDGender = cbGender.SelectedIndex + 1;
                        editApplication.Pet.IDPet = cbPet.SelectedIndex + 1;
                        editApplication.Application.Client.IDClient = cbHoz.SelectedIndex + 1;
                        editApplication.Application.Wishes = txtPetWish.Text;

                        AppDate.context.ExecutedApplication.Add(application);
                        AppDate.context.SaveChanges();
                        MessageBox.Show("Данные о заявке успешно добавлены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            this.Close();
        }
    }
}
