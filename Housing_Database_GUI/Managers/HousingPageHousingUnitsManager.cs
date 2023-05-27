using Database;

namespace Housing_Database_GUI.Managers
{
    internal class HousingPageHousingUnitsManager
    {
        private HousingPage housingPage;

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
    }
}
