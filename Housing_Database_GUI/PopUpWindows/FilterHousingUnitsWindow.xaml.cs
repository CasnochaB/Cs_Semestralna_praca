using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Housing_Database_GUI.HousingPageWindows
{
    /// <summary>
    /// Interaction logic for FilterHousingUnitsWindow.xaml
    /// </summary>
    public partial class FilterHousingUnitsWindow : Window
    {
        protected static int minNumber = 0;
        protected static int maxNumber = 0;
        protected Regex numberOnlyRegex = new Regex("[^0-9]+");

        public FilterHousingUnitsWindow()
        {
            InitializeComponent();
            MaxNumber_TextBox.Text = maxNumber.ToString();
            MinNumber_TextBox.Text = minNumber.ToString();
        }

        private void MinNumber_TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string newText = MinNumber_TextBox.Text + e.Text;
            Match(e, newText);
        }

        private void Match(TextCompositionEventArgs e, string newText)
        {
            if (numberOnlyRegex.IsMatch(newText))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        private void MaxNumber_TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string newText = MaxNumber_TextBox.Text + e.Text;
            Match(e, newText);
        }

        private void FilterOk_Button_Click(object sender, RoutedEventArgs e)
        {
            if (MaxNumber_TextBox.Text.Length == 0)
            {
                MaxNumber_TextBox.Text = "0";
            }
            if (MinNumber_TextBox.Text.Length == 0)
            {
                MinNumber_TextBox.Text = "0";
            }
            if (Int32.Parse(MinNumber_TextBox.Text) > Int32.Parse(MaxNumber_TextBox.Text))
            {
                MinNumber_TextBox.Text = MaxNumber_TextBox.Text;
            }
            minNumber = Int32.Parse(MinNumber_TextBox.Text);
            maxNumber = Int32.Parse(MaxNumber_TextBox.Text);
            DialogResult = true;
        }

        private void Reset_Button_Click(object sender, RoutedEventArgs e)
        {
            minNumber = 0;
            maxNumber = 0;
            MaxNumber_TextBox.Text = maxNumber.ToString();
            MinNumber_TextBox.Text = minNumber.ToString();
        }
    }
}
