using Database;
using Housing_Database_GUI.AddWindows;
using System.Collections.Generic;

namespace Housing_Database_GUI.Managers
{
    internal class HousingPagePeopleManager
    {
        private HousingPage housingPage;
        FilterManager filterManager;

        public HousingPagePeopleManager(HousingPage housingPage)
        {
            this.housingPage = housingPage;
            filterManager = new FilterManager();
        }

        public void PeopleListReset()
        {
            housingPage.People_ListBox.Items.Clear();

            if (housingPage.ignoreHousingUnits)
            {
                var housing = housingPage.GetSelectedHousing();
                if (housing == null)
                {
                    return;
                }
                var data = housingPage.GetSelectedHousing().GetInhabitants();
                AddPeopleToListBox(data);
            }
            else
            {
                var housingUnit = housingPage.GetSelectedHousingUnit();
                if (housingUnit != null)
                {
                    var data = housingUnit.GetInhabitants();
                    AddPeopleToListBox(data);
                }
            }
            housingPage.DisplayPeopleCount();
        }

        private void AddPeopleToListBox(IEnumerable<Person> data)
        {
            if (data != null)
            {
                foreach (var item in data)
                {
                    housingPage.People_ListBox.Items.Add(item);
                }
            }
        }

        public void AddPerson()
        {
            AddPersonWindow addPersonWindow = new AddPersonWindow();
            var housingUnit = housingPage.GetSelectedHousingUnit();
            var result = addPersonWindow.ShowDialog();
            if (result == true)
            {
                string identificationNumber = addPersonWindow.IdentificationNumber_TextBox.Text;
                Person person = PersonRegister.Get(identificationNumber);
                housingUnit.Add(person);
                housingPage.People_ListBox.Items.Add(person);
                housingPage.DisplayPeopleCount();
            }
        }

        public void RemovePerson()
        {
            var person = housingPage.GetSelectedPerson();
            if (person == null)
            {
                housingPage.RemovePerson_Button.IsEnabled = false;
                return;
            }
            if (housingPage.ignoreHousingUnits)
            {
                var housing = housingPage.GetSelectedHousing();
                housing.Remove(person);
                housingPage.People_ListBox.Items.Remove(person);
            }
            else
            {
                var housingUnit = housingPage.GetSelectedHousingUnit();
                housingUnit.Remove(person);
                housingPage.People_ListBox.Items.Remove(person);
            }
            housingPage.RemovePerson_Button.IsEnabled = false;
        }

        public void SelectionChanged()
        {
            housingPage.AddPerson_Button.IsEnabled = true;
            housingPage.RemovePerson_Button.IsEnabled = true;
            Person person = housingPage.GetSelectedPerson();
            if (person != null)
            {
                housingPage.lastSelectedObject = person;
            }
        }

        public void Filter()
        {
            housingPage.People_ListBox.Items.Filter = item => filterManager.FullNameFilterPredicate((Person)item, housingPage.InhabitantsFilter_TextBox.Text);
        }

    }
}
