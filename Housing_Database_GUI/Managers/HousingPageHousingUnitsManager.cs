using Database;
using Housing_Database_GUI.HousingPageWindows;
using System;

namespace Housing_Database_GUI.Managers
{
    internal class HousingPageHousingUnitsManager
    {
        private HousingPage housingPage;
        Predicate<HousingUnit> numberOfInhabitantsPredicate;

        public HousingPageHousingUnitsManager(HousingPage housingPage)
        {
            this.housingPage = housingPage;
            numberOfInhabitantsPredicate = housingUnit => true;
        }

        public void HousingUnitsListReset()
        {
            housingPage.HousingUnits_ListBox.Items.Clear();
            if (housingPage.ignoreHousingUnits)
            {
                return;
            }
            var housing = housingPage.GetSelectedHousing();
            if (housing == null)
            {
                return;
            }
            var data = housingPage.GetSelectedHousing().GetHousingUnits();
            if (data != null)
            {
                foreach (var item in data)
                {
                    housingPage.HousingUnits_ListBox.Items.Add(item);
                }
                housingPage.HousingUnits_ListBox.Items.Filter = housingUnit => numberOfInhabitantsPredicate((HousingUnit)housingUnit);
                housingPage.PeopleListReset();
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
                    housingPage.HousingUnits_ListBox.Items.Add(housing);
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
                    housingPage.HousingUnits_ListBox.Items.Remove(unit);
                    housingPage.PeopleListReset();
                }
            }
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
                    housingPage.PeopleListReset();
                }
            }
            housingPage.DisplayPeopleCount();
        }

        internal void ApplyFilter()
        {
            FilterHousingUnitsWindow window = new FilterHousingUnitsWindow();
            var result = window.ShowDialog();
            if (result == true)
            {
                int min = Int32.Parse(window.MinNumber_TextBox.Text);
                int max = Int32.Parse(window.MaxNumber_TextBox.Text);
                if (min == 0 && max == 0)
                {
                    numberOfInhabitantsPredicate = housingUnit => true;
                }
                else
                {
                    numberOfInhabitantsPredicate = new Predicate<HousingUnit>(n => n.numberOfInhabitants >= min && n.numberOfInhabitants <= max);
                    housingPage.HousingUnits_ListBox.Items.Filter = housingUnit => numberOfInhabitantsPredicate((HousingUnit)housingUnit);
                }
                housingPage.PeopleListReset();
            }
        }
    }
}
