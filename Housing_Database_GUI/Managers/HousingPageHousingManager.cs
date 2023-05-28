using Database;
using Housing_Database_GUI.AddWindows;
using System;

namespace Housing_Database_GUI.Managers
{
    internal class HousingPageHousingManager
    {
        private HousingDatabase database;
        private HousingPage housingPage;
        public HousingPageHousingManager(HousingDatabase housingDatabase, HousingPage housingPage)
        {
            database = housingDatabase;
            this.housingPage = housingPage;
        }

        public void HousingListReset()
        {
            housingPage.Housings_Listbox.Items.Clear();
            var data = database.GetHousings();
            if (data != null)
            {
                foreach (var item in data)
                {
                    housingPage.Housings_Listbox.Items.Add(item);
                }
            }
            Filter();
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
                    housingPage.Housings_Listbox.Items.Add(house);
                }
                else
                {
                    int housingUnitsCount = Int32.Parse(addHousingWindow.UnitsNumber_TextBox.Text);
                    Flat flat = new Flat(houseNumber, housingUnitsCount);
                    database.Add(flat);
                    housingPage.Housings_Listbox.Items.Add(flat);
                }
            }
        }

        public void RemoveHousing()
        {
            var housing = housingPage.GetSelectedHousing();
            if (housing != null)
            {
                database.Remove(housing);
                housingPage.Housings_Listbox.Items.Remove(housing);
                housingPage.HousingUnitsListReset();
                housingPage.PeopleListReset();
            }
            housingPage.RemoveHousing_button.IsEnabled = false;
        }

        public void EditHousing()
        {
            var housing = housingPage.GetSelectedHousing();
            if (housing == null)
            {
                return;
            }
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
                housingPage.AddHousingUnits_Button.IsEnabled = false;
                housingPage.RemoveHousingUnits_Button.IsEnabled = false;
                housingPage.FilterHousingUnits_Button.IsEnabled = false;
                housingPage.PeopleListReset();
            }
            else
            {
                housingPage.AddHousingUnits_Button.IsEnabled = true;
                housingPage.FilterHousingUnits_Button.IsEnabled = true;
                housingPage.HousingUnits_ListBox.Items.Refresh();
                housingPage.HousingUnitsListReset();
            }
            housingPage.DisplayPeopleCount();
        }

        internal void Filter()
        {
            if (housingPage.HousingIDFilter_TextBox.Text.Length == 0)
            {
                housingPage.Housings_Listbox.Items.Filter = null;
                housingPage.HousingUnitsListReset();
                housingPage.PeopleListReset();
                return;
            }
            housingPage.Housings_Listbox.Items.Filter = HouseNumberFilterPredicate;
            housingPage.HousingUnitsListReset();
            housingPage.PeopleListReset();
        }

        private bool HouseNumberFilterPredicate(Object housing)
        {
            return ((Housing)housing).houseNumber.ToString().StartsWith(housingPage.HousingIDFilter_TextBox.Text);
        }
    }
}
