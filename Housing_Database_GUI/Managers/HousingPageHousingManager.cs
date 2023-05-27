using Database;
using Housing_Database_GUI.AddWindows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Housing_Database_GUI.Managers
{
    internal class HousingPageHousingManager
    {
        private HousingDatabase database;
        private HousingPage housingPage;
        public HousingPageHousingManager(HousingDatabase housingDatabase,HousingPage housingPage)
        {
            database = housingDatabase;
            this.housingPage = housingPage;
        }

        public void HousingListReset()
        {
            housingPage.People_ListBox.UnselectAll();
            housingPage.People_ListBox.ItemsSource = null;
            housingPage.HousingUnits_ListBox.UnselectAll();
            housingPage.HousingUnits_ListBox.ItemsSource = null;
            housingPage.Housings_Listbox.UnselectAll();
            housingPage.Housings_Listbox.Items.Refresh();
            var data = database.GetHousings();
            if (data != null)
            {
                housingPage.Housings_Listbox.ItemsSource = new ObservableCollection<Housing>(data);
            }
            else
            {
                housingPage.Housings_Listbox.ItemsSource = new ObservableCollection<Housing>();
            }
            housingPage.Housings_Listbox.Items.Refresh();
            housingPage.DisplayPeopleCount();
        }

        public void AddHousing()
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
                }
                else
                {
                    int housingUnitsCount = Int32.Parse(addHousingWindow.UnitsNumber_TextBox.Text);
                    Flat flat = new Flat(houseNumber, housingUnitsCount);
                    database.Add(flat);
                }
            }
            HousingListReset();
        }

        public void RemoveHousing()
        {
            var housing = housingPage.GetSelectedHousing();
            if (housing != null)
            {
                database.Remove(housing);
            }
            housingPage.RemoveHousing_button.IsEnabled = false;
            HousingListReset();
        }

        public void EditHousing()
        {
            var housing = housingPage.GetSelectedHousing();
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

        public void SelectionChanged()
        {
            housingPage.RemoveHousing_button.IsEnabled = true;
            var housing = housingPage.GetSelectedHousing();
            if (housing == null)
            {
                return;
            }
            housingPage.lastSelectedObject = housing;
            if (housingPage.ignoreHousingUnits)
            {
                housingPage.HousingUnits_ListBox.ItemsSource = null;
                housingPage.AddHousingUnits_Button.IsEnabled = false;
                housingPage.RemoveHousingUnits_Button.IsEnabled = false;
                housingPage.FilterHousingUnits_Button.IsEnabled = false;
                housingPage.People_ListBox.ItemsSource = housing.GetInhabitants();
            }
            else
            {
                housingPage.AddHousingUnits_Button.IsEnabled = true;
                housingPage.FilterHousingUnits_Button.IsEnabled = true;
                housingPage.HousingUnits_ListBox.ItemsSource = housing.GetHousingUnits();
                housingPage.People_ListBox.ItemsSource = null;
                housingPage.HousingUnits_ListBox.Items.Refresh();
            }
            housingPage.DisplayPeopleCount();
        }
    }
}
