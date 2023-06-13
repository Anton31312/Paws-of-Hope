using Paws_of_Hope.Class;
using Paws_of_Hope.EF;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
namespace Paws_of_Hope.Windows
{
    /// <summary>
    /// Логика взаимодействия для ApplicationWindow.xaml
    /// </summary>
    public partial class ApplicationWindow : Window
    {

        private int TotalPet = 0;
        List<ExecutedApplication> appList = new List<ExecutedApplication>();

        public ApplicationWindow()
        {
            InitializeComponent();

            var status = AppDate.GetAllStatus();
            status.Insert(0, "По умолчанию");
            cbStatusClient.ItemsSource = status;
            cbStatusClient.SelectedIndex = 0;

            Filter();
        }

        private void Filter()
        {
            if (listApplicationClient is null)
                return;

            appList = AppDate.context.ExecutedApplication.ToList();
            if (tbSearch.Text != $"ФИО клиента")
            {
                appList = appList.Where(i => i.Application.Client.LastName.ToLower().Contains(tbSearch.Text.ToLower()) ||
                i.Application.Client.FirstName.ToLower().Contains(tbSearch.Text.ToLower()) ||
                i.Application.Client.Patronymic.ToLower().Contains(tbSearch.Text.ToLower())).ToList();
            }
            TotalPet = AppDate.GetAllApplication().Count;

            if (cbStatusClient.SelectedIndex != 0)
            {
                appList = appList.Where(i => i.Application.Client.IDStatusClient == cbStatusClient.SelectedIndex).ToList();
            }

            listApplicationClient.ItemsSource = appList;
            txtCountApp.Text = $"{appList.Count} из {TotalPet}";
        }

        

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddApplicationWindow addApplicationWindow = new AddApplicationWindow();
            this.Opacity = 0.2;
            addApplicationWindow.ShowDialog();
            this.Opacity = 1;
            Filter();
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

        private void tbSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox instance = (TextBox)sender;
            if (instance.Text == instance.Tag.ToString())
            {
                instance.Text = "";
                instance.Foreground = Brushes.Black;
            }
        }

        private void tbSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox instance = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(instance.Text))
            {
                instance.Text = instance.Tag.ToString();
                instance.Foreground = Brushes.LightGray;
            }
        }

        private void listApplicationClient_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var editAppication = new EF.ExecutedApplication();

            if (listApplicationClient.SelectedItem is EF.ExecutedApplication)
            {
                editAppication = listApplicationClient.SelectedItem as EF.ExecutedApplication;
            }
            AddApplicationWindow addApplicationWindow = new AddApplicationWindow(editAppication);
            this.Opacity = 0.2;
            addApplicationWindow.ShowDialog();
            this.Opacity = 1;
            Filter();
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void cbStatusClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }
    }
}
