﻿using Paws_of_Hope.Class;
using Paws_of_Hope.EF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Paws_of_Hope.Windows
{
    public partial class ClientWindow : Window
    {
        private int TotalPet = 0;
        

        public ClientWindow()
        {
            InitializeComponent();
            listClient.ItemsSource = AppDate.context.Client.ToList();
            var status = AppDate.GetAllStatus();
            status.Insert(0, "По умолчанию");
            cbStatusClient.ItemsSource = status;
            cbStatusClient.SelectedIndex = 0;

            var gender = AppDate.GetAllGender();
            gender.Insert(0, "По умолчанию");
            cbGender.ItemsSource = gender;
            cbGender.SelectedIndex = 0;

            Filter();

        }

        private void Filter()
        {
            if (listClient is null)
                return;

            List<Client> clientList = new List<Client>();
            clientList = AppDate.context.Client.ToList();
            if (tbSearch.Text != "Введите ФИО")
            {
                clientList = clientList.Where(i => i.LastName.ToLower().Contains(tbSearch.Text.ToLower()) ||
                i.FirstName.ToLower().Contains(tbSearch.Text.ToLower())).ToList();
            }

            TotalPet = AppDate.GetAllClient().Count;

            listClient.ItemsSource = clientList;

            txtCountClient.Text = $"{clientList.Count} из {TotalPet}";
        }


        private void listClient_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var editClient= new Client();

            if (listClient.SelectedItem is Client)
            {
                editClient = listClient.SelectedItem as Client;
            }
            AddClientWindow addClientWindow = new AddClientWindow(editClient);
            this.Opacity = 0.2;
            addClientWindow.ShowDialog();
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

        private void listClient_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (listClient.SelectedItem is Client)
                {
                    try
                    {
                        var item = listClient.SelectedItem as Client;
                        var resultClick = MessageBox.Show("Вы уверены?", "Подтвердите удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (resultClick == MessageBoxResult.Yes)
                        {
                            AppDate.context.Client.Remove(item);
                            AppDate.context.SaveChanges();
                            MessageBox.Show("Клиент успешно удален!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                            Filter();
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
            AddClientWindow addClientWindow = new AddClientWindow();
            this.Opacity = 0.2;
            addClientWindow.ShowDialog();
            this.Opacity = 1;
            Filter();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (listClient.SelectedItem is Client)
            {
                try
                {
                    var item = listClient.SelectedItem as Client;
                    var resultClick = MessageBox.Show("Вы уверены?", "Подтвердите удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (resultClick == MessageBoxResult.Yes)
                    {
                        AppDate.context.Client.Remove(item);
                        AppDate.context.SaveChanges();
                        MessageBox.Show("Клиент успешно удален!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        Filter();
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

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void cbStatusClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void cbGender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }
    }
}
