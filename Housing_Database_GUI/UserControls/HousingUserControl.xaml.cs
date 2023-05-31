using Database;
using Housing_Database_GUI.Managers;
using System;
using System.Linq;
using System.Text.RegularExpressions;
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
        public bool ignoreHousingUnits = false;
        public Object? lastSelectedObject = null;
        private readonly HousingPageHousingManager housingManager;
        private readonly HousingPageHousingUnitsManager housingUnitsManager;
        private readonly HousingPagePeopleManager peopleManager;

        public HousingPage() : this(new HousingDatabase())
        {
        }

        public HousingPage(HousingDatabase housingDatabase)
        {
            InitializeComponent();
            database = housingDatabase;
            housingManager = new HousingPageHousingManager(database, this);
            housingUnitsManager = new HousingPageHousingUnitsManager(this);
            peopleManager = new HousingPagePeopleManager(this);
            HousingListReset();
        }

        private void HousingListReset()
        {
            housingManager.HousingListReset();
        }

        private void AddHousing_Button_Click(object sender, RoutedEventArgs e)
        {
            housingManager.AddHousing();
        }

        private void RemoveHousing_button_Click(object sender, RoutedEventArgs e)
        {
            housingManager.RemoveHousing();
        }

        private void EditHousing_Button_Click(object sender, RoutedEventArgs e)
        {
            housingManager.EditHousing();
        }
        private void AddHousingUnits_Button_Click(object sender, RoutedEventArgs e)
        {
            housingUnitsManager.AddHousingUnit();
        }

        private void RemoveHousingUnits_Button_Click(object sender, RoutedEventArgs e)
        {
            housingUnitsManager.RemoveHousingUnit();
        }

        private void FilterHousingUnits_Button_Click(object sender, RoutedEventArgs e)
        {
            housingUnitsManager.OpenFilterWindow();
        }

        private void AddPerson_Button_Click(object sender, RoutedEventArgs e)
        {
            peopleManager.AddPerson();
        }

        private void RemovePerson_Button_Click(object sender, RoutedEventArgs e)
        {
            peopleManager.RemovePerson();
        }

        public void DisplayPeopleCount()
        {
            Count_Label.Content = People_ListBox.Items.Count + "/" + database.GetNumberOfInstances();
        }

        private void FilterPeople_Button_Click(object sender, RoutedEventArgs e)
        {
            peopleManager.OpenFilterWindow();
        }

        public HousingUnit GetSelectedHousingUnit()
        {
            return (HousingUnit)HousingUnits_ListBox.SelectedItem;
        }

        public Person GetSelectedPerson()
        {
            var person = (Person)People_ListBox.SelectedItem;
            return person;
        }

        public Housing GetSelectedHousing()
        {
            var housing = (Housing)Housings_Listbox.SelectedItem;
            return housing;
        }

        private void People_ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            peopleManager.SelectionChanged();
        }

        private void HousingUnits_ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            housingUnitsManager.SelectionChanged();
        }

        private void Housings_ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            housingManager.SelectionChanged();
        }

        private void ToggleIgnoreHousingUnits_Click(object sender, RoutedEventArgs e)
        {
            ignoreHousingUnits = !ignoreHousingUnits;
            int housingIndex = Housings_Listbox.SelectedIndex;
            HousingUnitsListReset();
            HousingListReset();
            Housings_Listbox.SelectedIndex = housingIndex;
        }

        private void AddToExport_Button_Click(object sender, RoutedEventArgs e)
        {
            if (lastSelectedObject != null)
            {
                switch (lastSelectedObject.GetType().Name)
                {
                    case "Person":
                        Person person = (Person)lastSelectedObject;
                        AddPersonToExport(person);
                        break;
                    case "HousingUnit":
                        database.AddToExport((HousingUnit)lastSelectedObject); break;
                    default:
                        database.AddToExport((Housing)lastSelectedObject); break;
                }
            }
        }

        private void AddPersonToExport(Person person)
        {
            if (ignoreHousingUnits)
            {
                if (GetSelectedHousing() != null)
                {
                    string[] address = GetSelectedHousing().Where(n => n.Contains(person)).Select(m => m.unitIdentifier).ToArray();
                    foreach (var item in address)
                    {
                        database.AddToExport(person, item);
                    }
                }
            }
            else
            {
                if (GetSelectedHousingUnit() != null)
                {
                    string address = GetSelectedHousingUnit().unitIdentifier;
                    database.AddToExport(person, address);
                }
            }
        }

        internal void PeopleListReset()
        {
            peopleManager.PeopleListReset();
        }

        internal void HousingUnitsListReset()
        {
            housingUnitsManager.HousingUnitsListReset();
        }

        private void HousingIDFilter_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            housingManager.Filter();
        }

        private void HousingIDFilter_TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            string newText = HousingIDFilter_TextBox.Text + e.Text;
            Regex regex = new Regex("[^0-9]+");
            if (regex.IsMatch(newText))
            {
                e.Handled = true;
            }
        }

        private void InhabitantsFilter_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            peopleManager.ApplyFilter();
        }

        private void ResetFilters_Button_Click(object sender, RoutedEventArgs e)
        {
            HousingIDFilter_TextBox.Text = "";
            InhabitantsFilter_TextBox.Text = "";
            peopleManager.ResetFilter();
            housingUnitsManager.ResetFilter();
        }
    }
}
