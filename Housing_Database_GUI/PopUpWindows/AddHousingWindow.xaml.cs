using Database;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Housing_Database_GUI.AddWindows
{
    /// <summary>
    /// Interaction logic for AddHousingWindow.xaml
    /// </summary>
    public partial class AddHousingWindow : Window
    {

        private readonly Regex numberOnlyRegex = new Regex("[^0-9]+");
        public HousingDatabase housingDatabase;
        private int houseNumber = -1;

        public AddHousingWindow()
        {
            housingDatabase = new HousingDatabase();
            InitializeComponent();
        }

        public AddHousingWindow(int houseNumber, HousingDatabase database)
        {
            InitializeComponent();
            housingDatabase = database;
            this.houseNumber = houseNumber;
            HouseNumber_TextBox.Text = houseNumber.ToString();
        }

        private void SelectHousingType_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UnitsNumber_TextBox is not null)
            {
                if (SelectHousingType_ComboBox.SelectedIndex == 0)
                {
                    UnitsNumber_TextBox.IsEnabled = false;
                }
                else
                {
                    UnitsNumber_TextBox.IsEnabled = true;
                }
            }
        }

        private void UnitsNumber_TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = numberOnlyRegex.IsMatch(UnitsNumber_TextBox.Text);
        }

        private void NewHousingOK_Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void UnitsNumber_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NewHousingOK_Button is not null)
            {
                if (UnitsNumber_TextBox.Text.Length > 0)
                {
                    NewHousingOK_Button.IsEnabled = true;
                }
                else
                {
                    NewHousingOK_Button.IsEnabled = false;
                }
            }
        }

        private void HouseNumber_TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string newText = HouseNumber_TextBox.Text + e.Text;
            if (numberOnlyRegex.IsMatch(newText))
            {
                e.Handled = true;
            }
        }

        private void HouseNumber_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NewHousingOK_Button is not null)
            {
                if (HouseNumber_TextBox.Text.Length > 0)
                {
                    int newHouseNumber = Int32.Parse(HouseNumber_TextBox.Text);
                    if (housingDatabase.Contains(newHouseNumber))
                    {
                        if (newHouseNumber == houseNumber)
                        {
                            NewHousingOK_Button.IsEnabled = true;
                            CorrectionCheck_Label.Content = "";
                            return;
                        }
                        NewHousingOK_Button.IsEnabled = false;
                        CorrectionCheck_Label.Content = "Číslo domu už existuje";
                    }
                    else
                    {
                        NewHousingOK_Button.IsEnabled = true;
                    }
                }
                else
                {
                    CorrectionCheck_Label.Content = "Nie je zadané číslo domu";
                }
            }
        }
    }
}
