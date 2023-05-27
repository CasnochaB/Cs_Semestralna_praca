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
    internal class HousingPagePeopleManager
    {
        private HousingDatabase database;
        private HousingPage housingPage;

        public HousingPagePeopleManager(HousingDatabase housingDatabase, HousingPage housingPage)
        {
            database = housingDatabase;
            this.housingPage = housingPage;
        }

        public void PeopleListReset()
        {
            if (housingPage.ignoreHousingUnits)
            {
                var data = housingPage.GetSelectedHousing().GetInhabitants();
                if (data != null)
                {
                    housingPage.People_ListBox.ItemsSource = new ObservableCollection<Person>(data);
                }
                else
                {
                    housingPage.People_ListBox.ItemsSource = new ObservableCollection<Person>();
                }
            }
            else
            {
                var housingUnit = housingPage.GetSelectedHousingUnit();
                if (housingUnit != null)
                {
                    var data = housingUnit.GetInhabitants();
                    if (data != null)
                    {
                        housingPage.People_ListBox.ItemsSource = new ObservableCollection<Person>(data);
                    }
                }
                else
                {
                    housingPage.People_ListBox.ItemsSource = new ObservableCollection<Person>();
                }
            }
            housingPage.DisplayPeopleCount();
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
            }
            else
            {
                var housingUnit = housingPage.GetSelectedHousingUnit();
                housingUnit.Remove(person);
            }
            housingPage.RemovePerson_Button.IsEnabled = false;
            PeopleListReset();
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

    }
}
