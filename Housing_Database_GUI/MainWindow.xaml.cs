using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            MainFrame = new Frame();
            MainFrame.Source = new Uri("HousingPage.xaml", UriKind.Relative);
            MainFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            
            InitializeComponent();
        }

        private void PageSelector_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = PageSelector_ComboBox.SelectedIndex;
            Debug.WriteLine(index);
            switch (index)
            {
                case 0:
                    MainFrame.Source = new Uri("HousingPage.xaml", UriKind.Relative);
                    break;
                case 1:
                    MainFrame.Source = new Uri("AdressPage.xaml", UriKind.Relative);
                    break;
                case 2:
                    MainFrame.Source = new Uri("PersonRegisterPage.xaml", UriKind.Relative);
                    break;
                case 3:
                    MainFrame.Source = new Uri("AdressPage.xaml", UriKind.Relative);
                    break;
                default:
                    MainFrame.Source = new Uri("HousingPage.xaml", UriKind.Relative);
                    break;
            }
        }
    }
}
