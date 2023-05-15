﻿using Database;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
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

namespace Housing_Database_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private HousingDatabase housingDatabase;

        public MainWindow()
        {
            housingDatabase = new HousingDatabase();
            MainFrame = new Frame();
            MainFrame.Source = new Uri("HousingPage.xaml", UriKind.Relative);
            MainFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            
            InitializeComponent();
        }

        private void PageSelector_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DisplaySelectedView();
        }

        private void DisplaySelectedView()
        {
            int index = PageSelector_ComboBox.SelectedIndex;
            switch (index)
            {
                case 0:
                    MainFrame.Content = new HousingPage();
                    break;
                case 1:
                    MainFrame.Content = new AdressPage();
                    break;
                case 2:
                    MainFrame.Content = new PersonRegisterPage();
                    break;
                case 3:
                    MainFrame.Content = new HousingPage();
                    break;
                default:
                    MainFrame.Content = new HousingPage();
                    break;
            }
        }

        private void Clear()
        {
            if (housingDatabase != null)
            {
                housingDatabase.Clear();
            }
            PersonRegister.Clear();
        }

        private void ExportFile_Click(object sender, RoutedEventArgs e)
        {
            var result = SaveFileDialog();
            if (result != null)
            {
                housingDatabase.Export(result);
            }
        }

        private void ImportFile_Click(object sender, RoutedEventArgs e)
        {
            var fileInfo = OpenFileDialog();
            if (fileInfo != null)
            {
                housingDatabase.Import(fileInfo);
                DisplaySelectedView();
            }
        }

        private void NewFile_Click(object sender, RoutedEventArgs e) 
        {
            if (housingDatabase.Count() == 0 && PersonRegister.count == 0)
            {
                //nothing happens
            } else
            {
                var result = MessageBox.Show("Are you sure you want to create new database?", "New housing database", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    Clear();
                    DisplaySelectedView();
                }
            }
        }

        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            var result = SaveFileDialog();
            if (result != null)
            {
                housingDatabase.Save(result);
            }
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            var fileInfo = OpenFileDialog();
            if (fileInfo != null)
            {
                housingDatabase.Load(fileInfo);
                DisplaySelectedView();
            }
        }

        private FileInfo? SaveFileDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            var result = saveFileDialog.ShowDialog();
            if (result == true)
            {
                return new FileInfo(saveFileDialog.FileName);
            }
            return null;
        }

        private FileInfo? OpenFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            var result = openFileDialog.ShowDialog();
            if (result == true)
            {
                return new FileInfo(openFileDialog.FileName);
            }
            return null;
        }

        private void ExitWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
