using System.Windows;
using System.Windows.Input;
using Paws_of_Hope.EF;
using Paws_of_Hope.Windows;

namespace Paws_of_Hope
{
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
