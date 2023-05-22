using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Paws_of_Hope.Class;
using Paws_of_Hope.EF;

namespace Paws_of_Hope.Windows
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        bool isConnected = false;
        ServiceAuth.ServiceAuthClient client;
        int ID;

        public AuthWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            client = new ServiceAuth.ServiceAuthClient();
        }

        void ConnectUser() 
        {
            if (!isConnected)
            {
                ID = client.Connect(txtLogin.Text, pbPassword.Password);
                isConnected = true;
            }
        }

        void DisconectUser()
        {
            if (isConnected)
            {
                client.Disconnect(ID);
                isConnected = false;
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            ConnectUser();
            var userAuth = AppDate.Context.Tutor.ToList().Where(i => i.IDTutor == ID).FirstOrDefault();
            if (userAuth != null)
            {
                CurrentUser.FullName = string.Join(" ", new string[4] { "Наставник:", userAuth.LastName, userAuth.FirstName ,userAuth.Patronymic});
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                //this.Close();
            }
            else
            {
                MessageBox.Show("Пользователь с такими данными не найден!");
            }
        }

        private void txtLogin_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox instance = (TextBox)sender;
            if (instance.Text == instance.Tag.ToString())
                instance.Text = "";
        }

        private void txtLogin_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox instance = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(instance.Text))
                instance.Text = instance.Tag.ToString();
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            DisconectUser();
        }
    }
}
