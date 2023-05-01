using Paws_of_Hope.Class;
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
    /// Логика взаимодействия для DogWindow.xaml
    /// </summary>
    public partial class DogWindow : Window
    {
        public DogWindow()
        {
            InitializeComponent();
        }

        private void txtExit_MouseUp(object sender, MouseButtonEventArgs e)
        {
            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
            this.Close();
        }

        private void txtBackMainWin_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void listDog_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void listDog_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (listDog.SelectedItem is EF.Pet)
                {
                    try
                    {
                        var item = listDog.SelectedItem as EF.Pet;
                        var resultClick = MessageBox.Show("Вы уверены?", "Подтвердите удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (resultClick == MessageBoxResult.Yes)
                        {
                            AppDate.Context.Pet.Remove(item);
                            AppDate.Context.SaveChanges();
                            MessageBox.Show("Пользователь успешно удален!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                            //Filter();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }

            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (listDog.SelectedItem is EF.Pet)
            {
                try
                {
                    var item = listDog.SelectedItem as EF.Pet;
                    var resultClick = MessageBox.Show("Вы уверены?", "Подтвердите удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (resultClick == MessageBoxResult.Yes)
                    {
                        AppDate.Context.Pet.Remove(item);
                        AppDate.Context.SaveChanges();
                        MessageBox.Show("Пользователь успешно удален!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        //Filter();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void tbSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox instance = (TextBox)sender;
            if (instance.Text == instance.Tag.ToString())
                instance.Text = "";
        }

        private void tbSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox instance = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(instance.Text))
                instance.Text = instance.Tag.ToString();
        }
    }
}
