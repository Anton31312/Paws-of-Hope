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
    /// Логика взаимодействия для AddApplicationWindow.xaml
    /// </summary>
    public partial class AddApplicationWindow : Window
    {
        public AddApplicationWindow()
        {
            InitializeComponent();
        }

        public AddApplicationWindow(EF.ExecutedApplication application)
        {
            InitializeComponent();
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
            ApplicationWindow applicationWindow = new ApplicationWindow();
            applicationWindow.Show();
            this.Close();
        }

        private void btnAddApplication_Click(object sender, RoutedEventArgs e)
        {
            ApplicationWindow applicationWindow = new ApplicationWindow();
            applicationWindow.Show();
            this.Close();
        }
    }
}
