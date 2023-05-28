using Database;
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
using System.Windows.Shapes;

namespace Housing_Database_GUI.PopUpWindows
{
    /// <summary>
    /// Interaction logic for StatisticsWindow.xaml
    /// </summary>
    public partial class StatisticsWindow : Window
    {
        HousingDatabase database;
        public StatisticsWindow(HousingDatabase database)
        {
            InitializeComponent();
            this.database = database;
            ShowStatistics();
        }

        private void ShowStatistics()
        {
            int peopleCount = database.GetNumberOfInhabitants();
            int housingCount = database.Count();
            int unitCount = database.GetHousings().Select(n => n.numberOfHousingUnits).Sum();
            int accommodationCount = database.GetNumberOfInstances();
            double averageHouseInhabitants = database.Where(n => n.housingType == "House").Select(m => m.numberOfInhabitants).DefaultIfEmpty(0).Average();
            double averageFlatInhabitants = database.Where(n => n.housingType == "Flat").Select(m => m.numberOfInhabitants).DefaultIfEmpty(0).Average();
            double averageUnitInhabitants = database.GetHousings().SelectMany(n => n.GetHousingUnits()).Select(m => m.numberOfInhabitants).DefaultIfEmpty(0).Average();
            double ageAverage = database.GetHousings().SelectMany(n => n.GetInhabitants()).Select(m => m.age).DefaultIfEmpty(0).Average();
            int peopleWithoutAccommodationCount = PersonRegister.count - database.GetNumberOfInhabitants();

            PeopleCount_Label.Content += peopleCount.ToString();
            HousingCount_Label.Content += housingCount.ToString();
            HousingUnitCount_Label.Content += unitCount.ToString();
            AccommodationCount_Label.Content += accommodationCount.ToString();
            HouseAverage_Label.Content += averageHouseInhabitants.ToString();
            FlatAverage_Label.Content += averageFlatInhabitants.ToString();
            UnitAverage_Label.Content += averageUnitInhabitants.ToString();
            AgeAverage_Label.Content += ageAverage.ToString();
            PeopleWithoutAccommodation_Label.Content += peopleWithoutAccommodationCount.ToString();

        }
    }
}
