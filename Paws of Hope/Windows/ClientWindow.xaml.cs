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
    /// Логика взаимодействия для ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        public ClientWindow()
        {
            InitializeComponent();
        }

        private void listClient_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void txtBackMainWin_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void txtExit_MouseUp(object sender, MouseButtonEventArgs e)
        {
            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
            this.Close();
        }

        private void listClient_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (listClient.SelectedItem is EF.Client)
                {
                    try
                    {
                        var item = listClient.SelectedItem as EF.Client;
                        var resultClick = MessageBox.Show("Вы уверены?", "Подтвердите удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (resultClick == MessageBoxResult.Yes)
                        {
                            AppDate.Context.Client.Remove(item);
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

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (listClient.SelectedItem is EF.Client)
            {
                try
                {
                    var item = listClient.SelectedItem as EF.Client;
                    var resultClick = MessageBox.Show("Вы уверены?", "Подтвердите удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (resultClick == MessageBoxResult.Yes)
                    {
                        AppDate.Context.Client.Remove(item);
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
