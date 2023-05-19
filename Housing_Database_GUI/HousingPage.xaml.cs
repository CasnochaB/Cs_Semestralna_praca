using Database;
using Housing_Database_GUI.AddWindows;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Housing_Database_GUI
{
    /// <summary>
    /// Interaction logic for HousingPage.xaml
    /// </summary>
    public partial class HousingPage : UserControl
    {
        public HousingDatabase database;
        private bool ignoreHousingUnits = false;

        public HousingPage()
        {
            database = new HousingDatabase();
            InitializeComponent();
        }

        public HousingPage(HousingDatabase housingDatabase)
        {
            InitializeComponent();
            database = housingDatabase;
            HousingListReset();
        }

        private void HousingListReset()
        {
            Housings_Listbox.ItemsSource = database.GetHousings();
        }

        private void AddHousing_Button_Click(object sender, RoutedEventArgs e)
        {
            //database.Add(housing);
        }

        private void RemoveHousing_button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditHousing_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FilterHousings_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddHousingUnits_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveHousingUnits_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditHousingUnits_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FilterHousingUnits_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddPerson_Button_Click(object sender, RoutedEventArgs e)
        {
            AddPersonWindow addPersonWindow = new AddPersonWindow();
            var housingUnit = GetSelectedHousingUnit();
            var result = addPersonWindow.ShowDialog();
            if (result == true)
            {
                string identificationNumber = addPersonWindow.IdentificationNumber_TextBox.Text;
                Person person = PersonRegister.Get(identificationNumber);
                housingUnit.Add(person);
            }
        }

        private void RemovePerson_Button_Click(object sender, RoutedEventArgs e)
        {
            People_ListBox.SelectedIndex = 0;
            var person = GetSelectedPerson();
            var housingUnit = GetSelectedHousingUnit();
            housingUnit.Remove(person);
            RemovePerson_Button.IsEnabled = false;
        }

        private void EditPerson_Button_Click(object sender, RoutedEventArgs e)
        {
            var person = GetSelectedPerson();
            AddPersonWindow addPersonWindow = new AddPersonWindow(person);
            var result = addPersonWindow.ShowDialog();
            if (result == true)
            {
                Person.PersonalData data = new Person.PersonalData(addPersonWindow.FirstName_TextBox.Text,
                                                                    addPersonWindow.LastName_TextBox.Text,
                                                                    addPersonWindow.IdentificationNumber_TextBox.Text);
                person.personalData = data;
            }
            PeopleListReset();
        }

        private void PeopleListReset()
        {
            var housingUnit = GetSelectedHousingUnit();
            if (housingUnit != null)
            {
                People_ListBox.ItemsSource = GetSelectedHousingUnit().GetInhabitants();
            }
        }

        private void FilterPeople_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private HousingUnit GetSelectedHousingUnit()
        {
            var unit = (HousingUnit)HousingUnits_ListBox.SelectedItem;
            return unit;
        }

        private Person GetSelectedPerson()
        {
            var person = (Person)HousingUnits_ListBox.SelectedItem; 
            return person;
        }

        private Housing GetSelectedHousing()
        {
            var housing = (Housing)HousingUnits_ListBox.SelectedItem;
            return housing;
        }

        private void People_ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void HousingUnits_ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Housings_Listbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
