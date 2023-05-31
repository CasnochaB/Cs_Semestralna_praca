using Database;
using Housing_Database_GUI.HousingPageWindows;
using System;
using System.Linq;

namespace Housing_Database_GUI.Managers
{
    internal class HousingPageHousingUnitsManager
    {
        private readonly HousingPage housingPage;

        public HousingPageHousingUnitsManager(HousingPage housingPage)
        {
            this.housingPage = housingPage;
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
                housingPage.PeopleListReset();
            }
        }

        public void AddHousingUnit()
        {
            var housing = housingPage.GetSelectedHousing();
            if (housing == null)
            {
                return;
            }
            if (housingPage.GetSelectedHousing().GetType().Name == "Flat")
            {
                ((Flat)housing).Add();
                HousingUnitsListReset();
            }
        }

        public void RemoveHousingUnit()
        {
            var housing = housingPage.GetSelectedHousing();
            if (housing is null)
            {
                return;
            }
            if (housing.housingType == "Flat")
            {
                var flat = housing as Flat;
                if (flat != null)
                {
                    var unit = housingPage.GetSelectedHousingUnit();
                    if (unit != null)
                    {
                        flat.Remove(unit);
                        housingPage.HousingUnits_ListBox.Items.Remove(unit);
                        housingPage.PeopleListReset();
                    }
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

        internal void OpenFilterWindow()
        {
            FilterHousingUnitsWindow window = new FilterHousingUnitsWindow();
            var result = window.ShowDialog();
            if (result == true)
            {
                int min = Int32.Parse(window.MinNumber_TextBox.Text);
                int max = Int32.Parse(window.MaxNumber_TextBox.Text);
                if (min == 0 && max == 0)
                {
                    ResetFilter();
                }
                else
                {
                    ApplyFilter(n => n.numberOfInhabitants >= min && n.numberOfInhabitants <= max);
                }
                housingPage.PeopleListReset();
            }
        }

        public void ApplyFilter(Predicate<HousingUnit> predicate)
        {
            housingPage.HousingUnits_ListBox.Items.Filter = housingUnit => predicate((HousingUnit)housingUnit);
        }

        public void ResetFilter()
        {
            Predicate<HousingUnit> numberOfInhabitantsPredicate = housingUnit => true;
            ApplyFilter(numberOfInhabitantsPredicate);
        }
    }
}
