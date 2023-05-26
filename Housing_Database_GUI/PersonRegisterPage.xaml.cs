using Database;
using Housing_Database_GUI.AddWindows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Housing_Database_GUI
{
    /// <summary>
    /// Interaction logic for PersonRegisterPage.xaml
    /// </summary>
    public partial class PersonRegisterPage : UserControl
    {
        private HousingDatabase database;
        public PersonRegisterPage()
        {
            InitializeComponent();
            SetWidth();
            database = new HousingDatabase();
            PeopleListReset();
        }
        public PersonRegisterPage(Database.HousingDatabase housingDatabase)
        {
            InitializeComponent();
            SetWidth();
            database = housingDatabase;
            PeopleListReset();
        }

        private void SetWidth()
        {
            var workingWidth = PersonRegister_ListBox.Width; //- SystemParameters.VerticalScrollBarWidth;
            var col1 = 0.50;
            var col2 = 0.20;
            var col3 = 0.15;
            Register_GridView.Columns[0].Width = col1 * workingWidth;
            Register_GridView.Columns[1].Width = col2 * workingWidth;
            Register_GridView.Columns[2].Width = col3 * workingWidth;
        }

        private void PeopleListReset()
        {
            PersonRegister_ListBox.ItemsSource = PersonRegister.GetAll();
        }

        private void AddNewPerson_Button_Click(object sender, RoutedEventArgs e)
        {
            AddPersonWindow addPersonWindow = new AddPersonWindow();
            var result = addPersonWindow.ShowDialog();
            if (result == true)
            {
                string identificationNumber = addPersonWindow.IdentificationNumber_TextBox.Text;
                Person person = PersonRegister.Get(identificationNumber);
            }
        }

        private void RemovePerson_Button_Click(object sender, RoutedEventArgs e)
        {
            var person = GetSelectedPerson();
            if (person != null)
            {
                PersonRegister.Remove(person);
                PeopleListReset();
            }
        }

        private Person GetSelectedPerson()
        {
            return (Person)PersonRegister_ListBox.SelectedItem;
        }
    }
}
