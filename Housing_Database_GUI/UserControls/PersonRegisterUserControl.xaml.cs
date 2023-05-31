using Database;
using Housing_Database_GUI.AddWindows;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Housing_Database_GUI
{
    /// <summary>
    /// Interaction logic for PersonRegisterPage.xaml
    /// </summary>
    public partial class PersonRegisterPage : UserControl
    {
        private readonly HousingDatabase database;
        readonly FilterManager filterManager;
        public PersonRegisterPage() : this(new HousingDatabase())
        {
        }

        public PersonRegisterPage(Database.HousingDatabase housingDatabase)
        {
            InitializeComponent();
            database = housingDatabase;
            filterManager = new FilterManager();
            PeopleListReset();
        }

        private void PeopleListReset()
        {
            PersonRegister_ListBox.Items.Clear();
            var data = PersonRegister.GetAll();
            foreach (var person in data)
            {
                PersonRegister_ListBox.Items.Add(person);
            }
            PeopleCountLabelReset();
        }

        private void PeopleCountLabelReset()
        {
            Count_Label.Content = PersonRegister_ListBox.Items.Count + "/" + database.GetNumberOfInhabitants();
        }

        private void AddNewPerson_Button_Click(object sender, RoutedEventArgs e)
        {
            AddPersonWindow addPersonWindow = new AddPersonWindow();
            var result = addPersonWindow.ShowDialog();
            if (result == true)
            {
                string identificationNumber = addPersonWindow.IdentificationNumber_TextBox.Text;
                Person person = PersonRegister.Get(identificationNumber);
                if (!PersonRegister_ListBox.Items.Contains(person)) {
                    PersonRegister_ListBox.Items.Add(person);
                }
                PeopleCountLabelReset();
            }
        }

        private void RemovePerson_Button_Click(object sender, RoutedEventArgs e)
        {
            var person = GetSelectedPerson();
            if (person != null)
            {
                PersonRegister.Remove(person);
                PersonRegister_ListBox.Items.Remove(person);
                PeopleCountLabelReset();
            }
        }

        private Person GetSelectedPerson()
        {
            return (Person)PersonRegister_ListBox.SelectedItem;
        }

        private void FilterSelection_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Filter_TextBox is not null)
            {
                Filter_TextBox.Text = "";
            }
        }

        private void Filter_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var text = Filter_TextBox.Text;
            switch (FilterSelection_ComboBox.SelectedIndex)
            {
                case 0: PersonRegister_ListBox.Items.Filter = item => { return filterManager.FirstNameFilterPredicate((Person)item, text); }; break;
                case 1: PersonRegister_ListBox.Items.Filter = item => { return filterManager.LastNameFilterPredicate((Person)item, text); }; break;
                case 2: PersonRegister_ListBox.Items.Filter = item => { return filterManager.PersonIDFilterPredicate((Person)item, text); }; break;
            }
            Count_Label.Content = PersonRegister_ListBox.Items.Count + "/" + PersonRegister.count;
        }

        private void ResetFilter_Button_Click(object sender, RoutedEventArgs e)
        {
            Filter_TextBox.Text = "";
        }
    }

}
