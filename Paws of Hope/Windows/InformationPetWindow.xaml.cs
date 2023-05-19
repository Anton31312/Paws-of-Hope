﻿using Paws_of_Hope.Class;
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
    /// Логика взаимодействия для InformationPetWindow.xaml
    /// </summary>
    public partial class InformationPetWindow : Window
    {
        EF.Pet editPet = new EF.Pet();


        public InformationPetWindow(EF.Pet pet)
        {
            InitializeComponent();

            editPet = pet;
            //add combobox
            cbAnimalShelter.ItemsSource = AppDate.Context.AnimalShelter.ToList();
            cbAnimalShelter.DisplayMemberPath = "NameAnimalShelter";

            cbGender.ItemsSource = AppDate.Context.Gender.ToList();
            cbGender.DisplayMemberPath = "NameGender";

            cbPetHoz.ItemsSource = AppDate.Context.Client.ToList();
            cbPetHoz.DisplayMemberPath = "LastName";

            cbTypePet.ItemsSource = AppDate.Context.TypePet.ToList();
            cbTypePet.DisplayMemberPath = "NameTypePet";

            cbTutor.ItemsSource = AppDate.Context.Tutor.ToList();
            cbTutor.DisplayMemberPath = "LastName";

            cbSizePet.ItemsSource = AppDate.Context.SizePet.ToList();
            cbSizePet.DisplayMemberPath = "NameSizePet";

            //get value
            txtPetName.Text = editPet.NamePet;
            cbGender.SelectedIndex = editPet.IDGender - 1;
            cbPetHoz.SelectedIndex = (int)(editPet.IDClient - 1);
            cbSizePet.SelectedIndex = (int)(editPet.IDSizePet - 1);
            cbTypePet.SelectedIndex = editPet.IDTypePet - 1;
        }

        private void btnEditPet_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBackPet_Click(object sender, RoutedEventArgs e)
        {
         
        }

        private void btnCheckPhoto_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}