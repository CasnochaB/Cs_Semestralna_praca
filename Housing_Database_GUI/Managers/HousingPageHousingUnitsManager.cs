using Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Housing_Database_GUI.Managers
{
    internal class HousingPageHousingUnitsManager
    {

        private HousingDatabase database;
        private HousingPage housingPage;

        public HousingPageHousingUnitsManager(HousingDatabase housingDatabase, HousingPage housingPage)
        {
            database = housingDatabase;
            this.housingPage = housingPage;
        }

        public void HousingUnitsListReset()
        {
            housingPage.People_ListBox.UnselectAll();
            housingPage.HousingUnits_ListBox.UnselectAll();
            var data = housingPage.GetSelectedHousing().GetHousingUnits();
            if (data != null)
            {
                housingPage.HousingUnits_ListBox.ItemsSource = new ObservableCollection<HousingUnit>(data);
            }
            else
            {
                housingPage.HousingUnits_ListBox.ItemsSource = new ObservableCollection<HousingUnit>();
            }
        }

        public void AddHousingUnit()
        {
            if (housingPage.GetSelectedHousing().GetType().Name == "Flat")
            {
                Flat housing = (Flat)housingPage.GetSelectedHousing();
                if (housing != null)
                {
                    housing.Add();
                    HousingUnitsListReset();
                }
            }
        }

        public void RemoveHousingUnit()
        {
            Flat housing = (Flat)housingPage.GetSelectedHousing();
            if (housing != null)
            {
                var unit = housingPage.GetSelectedHousingUnit();
                if (unit != null)
                {
                    housing.Remove(unit);
                }
            }
            HousingUnitsListReset();
        }

        public void SelectionChanged()
        {
            if (housingPage.ignoreHousingUnits)
            {

            }
            else
            {
                housingPage.RemoveHousingUnits_Button.IsEnabled = true;
                var housingUnit = housingPage.GetSelectedHousingUnit();
                if (housingUnit != null)
                {
                    housingPage.lastSelectedObject = housingUnit;
                    if (housingPage.GetSelectedHousing().GetType().Name == "House")
                    {
                        housingPage.RemoveHousingUnits_Button.IsEnabled = false;
                    }
                    housingPage.People_ListBox.ItemsSource = housingUnit.GetInhabitants();
                }
            }
            housingPage.DisplayPeopleCount();
        }
    }
}
