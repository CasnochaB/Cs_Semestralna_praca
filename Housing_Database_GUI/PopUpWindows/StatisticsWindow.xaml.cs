using Database;
using System.Linq;
using System.Windows;

namespace Housing_Database_GUI.PopUpWindows
{
    /// <summary>
    /// Interaction logic for StatisticsWindow.xaml
    /// </summary>
    public partial class StatisticsWindow : Window
    {
        private const string FloatFormat = "0.000";
        private readonly HousingDatabase database;
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
            int unitCount = database.Select(n => n.numberOfHousingUnits).Sum();
            int accommodationCount = database.GetNumberOfInstances();
            double averageHouseInhabitants = database.Where(n => n.housingType == "House").Select(m => m.numberOfInhabitants).DefaultIfEmpty(0).Average();
            double averageFlatInhabitants = database.Where(n => n.housingType == "Flat").Select(m => m.numberOfInhabitants).DefaultIfEmpty(0).Average();
            //double averageUnitInhabitants = database.GetHousings().SelectMany(n => n.GetHousingUnits()).Select(m => m.numberOfInhabitants).DefaultIfEmpty(0).Average();
            double averageUnitInhabitants = database.Where((HousingUnit housingUnit) => true).Select(housingUnit => housingUnit.numberOfInhabitants).Average();
            double ageAverage = database.GetHousings().SelectMany(n => n.GetInhabitants()).Select(m => m.age).DefaultIfEmpty(0).Average();
            int peopleWithoutAccommodationCount = PersonRegister.count - database.GetNumberOfInhabitants();

            PeopleCount_Label.Content += peopleCount.ToString();
            HousingCount_Label.Content += housingCount.ToString();
            HousingUnitCount_Label.Content += unitCount.ToString();
            AccommodationCount_Label.Content += accommodationCount.ToString();
            HouseAverage_Label.Content += averageHouseInhabitants.ToString(FloatFormat);
            FlatAverage_Label.Content += averageFlatInhabitants.ToString(FloatFormat);
            UnitAverage_Label.Content += averageUnitInhabitants.ToString(FloatFormat);
            AgeAverage_Label.Content += ageAverage.ToString(FloatFormat);
            AgeAverage_Label.Content += ageAverage.ToString();
            PeopleWithoutAccommodation_Label.Content += peopleWithoutAccommodationCount.ToString();

        }
    }
}
