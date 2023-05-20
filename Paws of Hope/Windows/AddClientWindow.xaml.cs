using Microsoft.Win32;
using Paws_of_Hope.Class;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Paws_of_Hope.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddClientWindow.xaml
    /// </summary>
    public partial class AddClientWindow : Window
    {
        EF.Client editClient = new EF.Client();
        string pathPhoto = null; // Для сохранения пути к изображению
        bool isEdit = true;

        public AddClientWindow()
        {
            InitializeComponent();

            var gender = AppDate.GetAllGender();
            gender.Insert(0, "Выберите пол");
            cbGender.ItemsSource = gender;

            var status = AppDate.GetAllStatus();
            status.Insert(0, "Выберите статус");
            cbStatusClient.ItemsSource = status;

            bool isEdit = false;
        }

        public AddClientWindow(EF.Client client)
        {
            InitializeComponent();

            // add image
            //if (client.Photo != null)
            //{
            //    using (MemoryStream stream = new MemoryStream(client))
            //    {
            //        BitmapImage bitmapImage = new BitmapImage();
            //        bitmapImage.BeginInit();
            //        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            //        bitmapImage.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
            //        bitmapImage.StreamSource = stream;
            //        bitmapImage.EndInit();
            //        MainPhoto.Source = bitmapImage;

            //    }
            //}

            //edit combobox
            cbGender.ItemsSource = AppDate.Context.Gender.ToList();
            cbGender.DisplayMemberPath = "NameGender";

            //edit TItle and content button
            tbTitle.Text = "Изменить данные клиента";
            btnAddClient.Content = "Изменить";

            //Get value
            editClient = client;

            txtLastName.Text = editClient.LastName;
            txtFirstName.Text = editClient.FirstName;
            txtPatronymic.Text = editClient.Patronymic;
            txtPhone.Text = editClient.Phone;
            txtEmail.Text = editClient.Email;
            cbGender.SelectedIndex = editClient.IDGender - 1;
            cbStatusClient.SelectedIndex = editClient.IDStatusClient - 1;

            isEdit = true;
        }

        private void txtLastName_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox instance = (TextBox)sender;
            if (instance.Text == instance.Tag.ToString())
                instance.Text = "";
        }

        private void txtLastName_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox instance = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(instance.Text))
                instance.Text = instance.Tag.ToString();
        }

        private void txtFirstName_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox instance = (TextBox)sender;
            if (instance.Text == instance.Tag.ToString())
                instance.Text = "";
        }

        private void txtFirstName_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox instance = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(instance.Text))
                instance.Text = instance.Tag.ToString();
        }

        private void txtPatronymic_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox instance = (TextBox)sender;
            if (instance.Text == instance.Tag.ToString())
                instance.Text = "";
        }

        private void txtPatronymic_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox instance = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(instance.Text))
                instance.Text = instance.Tag.ToString();
        }

        private void txtPhone_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox instance = (TextBox)sender;
            if (instance.Text == instance.Tag.ToString())
                instance.Text = "";
        }

        private void txtPhone_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox instance = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(instance.Text))
                instance.Text = instance.Tag.ToString();
        }

        private void txtEmail_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox instance = (TextBox)sender;
            if (instance.Text == instance.Tag.ToString())
                instance.Text = "";
        }

        private void txtEmail_LostFocus(object sender, RoutedEventArgs e)
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

        private void btnAddClient_Click(object sender, RoutedEventArgs e)
        {
            //Валидация
            #region
            //Проверка на пустоту
            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Поле Фамилия не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("Поле Имя не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPatronymic.Text))
            {
                MessageBox.Show("Поле Отчество не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Поле Телефон не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //Проверка на количество символов

            if (txtLastName.Text.Length > 100)
            {
                MessageBox.Show("В поле Фамилия недопустимое количество символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtFirstName.Text.Length > 100)
            {
                MessageBox.Show("В поле Имя недопустимое количество символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtPatronymic.Text.Length > 100)
            {
                MessageBox.Show("В поле Отчество недопустимое количество символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            #endregion

            //Проверка на ошибки в БД
            if (isEdit)
            {
                try
                {
                    //изменение читателя
                    editClient.LastName = txtLastName.Text;
                    editClient.FirstName = txtFirstName.Text;
                    editClient.Patronymic = txtPatronymic.Text;
                    editClient.IDStatusClient = cbStatusClient.SelectedIndex + 1;
                    editClient.IDGender = cbGender.SelectedIndex + 1;

                    if (pathPhoto != null)
                    {
                        // editPet.Photo = File.ReadAllBytes(pathPhoto);
                    }

                    AppDate.Context.SaveChanges();
                    MessageBox.Show("Данные клиента успешно изменены", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
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
                        //Добавление нового клиента
                        EF.Client client = new EF.Client();

                        client.LastName = txtLastName.Text;
                        client.FirstName = txtFirstName.Text;
                        client.Patronymic = txtPatronymic.Text;
                        client.IDStatusClient = cbStatusClient.SelectedIndex + 1;
                        client.IDGender = cbGender.SelectedIndex + 1;

                        if (pathPhoto != null)
                        {
                            //pet.PhotoPath = File.ReadAllBytes(pathPhoto);
                        }


                        AppDate.Context.Client.Add(client);
                        AppDate.Context.SaveChanges();
                        MessageBox.Show("Клиент успешно добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void btnBackClient_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void txtPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox txtPhone)
            {
                txtPhone.Text = new string(txtPhone.Text.Where(ch => ch == '+' || ch == '-' || (ch >= '0' && ch <= '9') || ch == '(' || ch == ')').ToArray());
                txtPhone.SelectionStart = e.Changes.First().Offset + 1;
                txtPhone.SelectionLength = 0;
            }
        }
    }
}
