using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Paws_of_Hope.Class;
using Paws_of_Hope.EF;

namespace Paws_of_Hope.Windows
{
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

        void ConnectUser(int IDuser, string login, string password) 
        {
            if (!isConnected)
            {
                ID = client.Connect(IDuser, login, password);
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
            
            var userAuth = AppDate.context.Tutor.ToList().FirstOrDefault(i => i.Login == txtLogin.Text & i.Password == pbPassword.Password);
            if (userAuth != null)
            {
                ConnectUser(userAuth.IDTutor, userAuth.Login, userAuth.Password);
                CurrentUser.FullName = string.Join(" ", new string[4] { "Наставник:", userAuth.LastName, userAuth.FirstName ,userAuth.Patronymic});
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
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
            {
                instance.Text = "";
                instance.Foreground = Brushes.Black;
            }
                
        }

        private void txtLogin_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox instance = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(instance.Text))
            {
                instance.Text = instance.Tag.ToString();
                instance.Foreground = Brushes.LightGray;
            }    
                
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            DisconectUser();
        }
    }
}
