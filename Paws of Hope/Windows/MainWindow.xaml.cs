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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Paws_of_Hope.Class;
using Paws_of_Hope.EF;
using Paws_of_Hope.Windows;

namespace Paws_of_Hope
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            txtNameTutor.Text = CurrentUser.FullName;
        }

        private void txtExit_MouseUp(object sender, MouseButtonEventArgs e)
        {
            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
            this.Close();
        }

        private void txtCatWin_MouseUp(object sender, MouseButtonEventArgs e)
        {
            CatWindow catWindow = new CatWindow();
            catWindow.Show();
            this.Close();
        }

        private void txtClientWin_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ClientWindow clientWindow = new ClientWindow();
            clientWindow.Show();
            this.Close();
        }

        private void txtAppWin_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ApplicationWindow applicationWindow = new ApplicationWindow();
            applicationWindow.Show();
            this.Close();
        }

        private void txtDogWin_MouseUp(object sender, MouseButtonEventArgs e)
        {
            DogWindow dogWindow = new DogWindow();
            dogWindow.Show();
            this.Close();
        }
    }
}
