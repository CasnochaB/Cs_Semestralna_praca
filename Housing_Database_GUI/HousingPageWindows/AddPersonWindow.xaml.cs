using Database;
using System.Windows;
using System.Windows.Controls;

namespace Housing_Database_GUI.AddWindows
{
    /// <summary>
    /// Interaction logic for AddPersonWindow.xaml
    /// </summary>
    public partial class AddPersonWindow : Window
    {
        public AddPersonWindow()
        {
            InitializeComponent();
        }

        public AddPersonWindow(Person person)
        {
            InitializeComponent();
            FirstName_TextBox.Text = person.personalData.FirstName;
            LastName_TextBox.Text = person.personalData.LastName;
            IdentificationNumber_TextBox.Text = person.personalData.IdentificationNumber;
        }

        private void NewPersonOK_Button_Click(object sender, RoutedEventArgs e)
        {
            if (DoCorrection())
            {
                DialogResult = true;
            }
        }

        private bool DoCorrection()
        {
            CorrectionLabel.Content = "";
            string firstName = FirstName_TextBox.Text;
            string lastName = LastName_TextBox.Text;
            string identificationNumber = IdentificationNumber_TextBox.Text;


            if (PersonRegister.Contains(identificationNumber) && firstName.Length == 0 && lastName.Length == 0)
            {
                return true;
            }

            if (PersonRegister.Contains(identificationNumber))
            {
                var data = PersonRegister.Get(identificationNumber);
                if (data.personalData.FirstName.Equals(firstName) && data.personalData.LastName.Equals(lastName))
                {
                    return true;
                }
                CorrectionLabel.Content = "Osoba s týmto rodným číslo už existuje";
                return false;
            }

            if (firstName.Length > 1 && lastName.Length > 1)
            {
                try
                {
                    Person person = new Person(firstName, lastName, identificationNumber);
                }
                catch { }
                return true;
            }
            CorrectionLabel.Content = "Zadajte meno a priezvisko pre osobu";
            return false;
        }

        private void FirstName_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void LastName_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void IdentificationNumber_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            string identificationNumber = IdentificationNumber_TextBox.Text;
            if (identificationNumber.Length == 0)
            {
                CorrectionLabel.Content = "";
                return;
            }

            if (!Person.CheckIDValidity(identificationNumber))
            {
                CorrectionLabel.Content = "Rodné číslo nemá správny formát";
            }
            else
            {
                CorrectionLabel.Content = "";
                NewPersonOK_Button.IsEnabled = true;
            }
        }
    }
}
