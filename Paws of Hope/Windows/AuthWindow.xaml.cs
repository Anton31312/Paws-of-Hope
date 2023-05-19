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
using Paws_of_Hope.Class;
using Paws_of_Hope.EF;

namespace Paws_of_Hope.Windows
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
       

        public AuthWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var userAuth = AppDate.Context.Tutor.ToList().
                Where(i => i.Login == txtLogin.Text && i.Password == pbPassword.Password).
                FirstOrDefault();
            if (userAuth != null)
            {
                CurrentUser.FullName = string.Join(" ", new string[4] { "Наставник:", userAuth.LastName, userAuth.FirstName ,userAuth.Patronymic});
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Пользователь с такими данными не найден!");
            }
        }
    }
}
