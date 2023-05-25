using Database;
using Housing_Database_GUI.AddWindows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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
        private bool ignoreHousingUnits = true;

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
            People_ListBox.UnselectAll();
            People_ListBox.ItemsSource = null;
            HousingUnits_ListBox.UnselectAll();
            HousingUnits_ListBox.ItemsSource = null;
            Housings_Listbox.UnselectAll(); 
            Housings_Listbox.ItemsSource = new List<Object>();
            Housings_Listbox.Items.Refresh();
            var data = database.GetHousings();
            if (data != null)
            {
                Housings_Listbox.ItemsSource = new ObservableCollection<Housing>(data);
            }
            else
            {
                Housings_Listbox.ItemsSource = new ObservableCollection<Housing>();
            }
            Housings_Listbox.Items.Refresh();
        }

        private void AddHousing_Button_Click(object sender, RoutedEventArgs e)
        {
            AddHousingWindow addHousingWindow = new AddHousingWindow();
            addHousingWindow.housingDatabase = database;
            var result = addHousingWindow.ShowDialog();
            if (result == true)
            {
                int houseNumber = Int32.Parse(addHousingWindow.HouseNumber_TextBox.Text);
                if (addHousingWindow.SelectHousingType_ComboBox.SelectedIndex == 0)
                {
                    House house = new House(houseNumber);
                    database.Add(house);
                } else
                {
                    int housingUnitsCount = Int32.Parse(addHousingWindow.UnitsNumber_TextBox.Text);
                    Flat flat = new Flat(houseNumber, housingUnitsCount);
                    database.Add(flat);
                }
            }
            HousingListReset();
        }

        private void RemoveHousing_button_Click(object sender, RoutedEventArgs e)
        {
            var housing = GetSelectedHousing();
            if (housing != null)
            {
                database.Remove(housing);
            }
            RemoveHousing_button.IsEnabled = false;
            HousingListReset();
        }

        private void EditHousing_Button_Click(object sender, RoutedEventArgs e)
        {
            var housing = GetSelectedHousing();
            AddHousingWindow addHousingWindow = new AddHousingWindow(housing.houseNumber, database);
            var result = addHousingWindow.ShowDialog();
            if (result == true)
            {
                database.Remove(housing);
                housing.houseNumber = Int32.Parse(addHousingWindow.HouseNumber_TextBox.Text);
                database.Add(housing);
            }
            HousingListReset();
        }

        private void FilterHousings_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddHousingUnits_Button_Click(object sender, RoutedEventArgs e)
        {
            Flat housing = (Flat)GetSelectedHousing();
            if (housing != null)
            {
                housing.Add();
                HousingUnitsListReset();
            }
        }

        private void RemoveHousingUnits_Button_Click(object sender, RoutedEventArgs e)
        {
            Flat housing = (Flat)GetSelectedHousing();
            if (housing != null)
            {
                var unit = GetSelectedHousingUnit();
                if (unit != null)
                {
                    housing.Remove(unit);
                }
            }
            HousingUnitsListReset();
        }

        private void HousingUnitsListReset()
        {
            People_ListBox.UnselectAll();
            HousingUnits_ListBox.UnselectAll(); 
            var data = GetSelectedHousing().GetHousingUnits();
            if (data != null)
            {
                HousingUnits_ListBox.ItemsSource = new ObservableCollection<HousingUnit>(data);
            }
            else
            {
                HousingUnits_ListBox.ItemsSource = new ObservableCollection<HousingUnit>();
            }
        }

        private void EditHousingUnits_Button_Click(object sender, RoutedEventArgs e)
        {
            //TODO odstranit toto tlacitko
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
            var person = GetSelectedPerson();
            if (person == null)
            {
                RemovePerson_Button.IsEnabled = false;
                return;
            }
            if (ignoreHousingUnits)
            {
                var housing = GetSelectedHousing();
                housing.Remove(person);
            }
            else
            {
                var housingUnit = GetSelectedHousingUnit();
                housingUnit.Remove(person);
            }
            RemovePerson_Button.IsEnabled = false;
            PeopleListReset();
        }

        private void EditPerson_Button_Click(object sender, RoutedEventArgs e)
        {
            //TODO odstranit

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
            if (ignoreHousingUnits)
            {
                var data = GetSelectedHousing().GetInhabitants();
                if (data != null)
                {
                    People_ListBox.ItemsSource = new ObservableCollection<Person>(data);
                }
                else
                {
                    People_ListBox.ItemsSource = new ObservableCollection<Person>();
                }
            }
            else
            {
                var housingUnit = GetSelectedHousingUnit();
                if (housingUnit != null)
                {
                    var data = housingUnit.GetInhabitants();
                    if (data != null)
                    {
                        People_ListBox.ItemsSource = new ObservableCollection<Person>(data);
                    }
                    else
                    {
                        People_ListBox.ItemsSource = new ObservableCollection<Person>();
                    }
                }
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
            var person = (Person)People_ListBox.SelectedItem; 
            return person;
        }

        private Housing GetSelectedHousing()
        {
            var housing = (Housing)Housings_Listbox.SelectedItem;
            return housing;
        }

        private void People_ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AddPerson_Button.IsEnabled = true;
            RemovePerson_Button.IsEnabled = true;
        }

        private void HousingUnits_ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ignoreHousingUnits)
            {

            }
            else
            {
                RemoveHousingUnits_Button.IsEnabled = true;
                var housingUnit = GetSelectedHousingUnit();
                if (housingUnit != null)
                {
                    if (GetSelectedHousing().GetType().Name == "House")
                    {
                        RemoveHousingUnits_Button.IsEnabled = false;
                    }
                    People_ListBox.ItemsSource = housingUnit.GetInhabitants();
                }
            }         
        }

        private void Housings_Listbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RemoveHousing_button.IsEnabled = true;
            var housing = GetSelectedHousing();
            if (housing == null) {
                return;
            }
            if (ignoreHousingUnits)
            {
                HousingUnits_ListBox.ItemsSource = null;
                AddHousingUnits_Button.IsEnabled = false;
                RemoveHousingUnits_Button.IsEnabled = false;
                FilterHousingUnits_Button.IsEnabled = false;
                People_ListBox.ItemsSource = housing.GetInhabitants();
            }
            else
            {
                AddHousingUnits_Button.IsEnabled = true;
                FilterHousingUnits_Button.IsEnabled = true;
                HousingUnits_ListBox.ItemsSource = housing.GetHousingUnits();
                People_ListBox.ItemsSource = null;
                HousingUnits_ListBox.Items.Refresh();      
            }
        }

        private void ToggleIgnoreHousingUnits_Click(object sender, RoutedEventArgs e)
        {
            ignoreHousingUnits = !ignoreHousingUnits;
            int housingIndex = Housings_Listbox.SelectedIndex;
            HousingListReset();
            Housings_Listbox.SelectedIndex = housingIndex;
        }
    }
}
