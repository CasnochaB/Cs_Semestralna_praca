using Database;

namespace HousingGenerators
{
 
    public static class HousingGenerator
    {

        public static int uniqueHouseNumber { 
            get { return uniqueHouseNumber++; }
            set { uniqueHouseNumber = value; }
        } 

        public static House GenerateHouse(bool generateInhabitants = false)
        {
            House house = new House(uniqueHouseNumber);
            if (generateInhabitants)
            {
                //TODO
            }
            return house;
        }

        public static Flat GenerateFlat(int numberOfHousingUnits = 6, bool generateHousingUnits = false, bool generateInhabitants = false)
        {
            Flat flat = new Flat(uniqueHouseNumber,numberOfHousingUnits);
            if (generateHousingUnits)
            {
                //TODO
                foreach (var item in HousingUnitGenerator.GenerateHousingUnits(numberOfHousingUnits, generateInhabitants))
                {
                    flat.Add(item);
                } 
            }
            return flat;
        }

        public static IEnumerable<House> GenerateHouses(int count,bool generateInhabitants = false)
        {
            for (int i = 0; i < count; i++)
            {
                yield return GenerateHouse(generateInhabitants);
            }
        }

        public static IEnumerable<Flat> GenerateFlats(int count, int numberOfHousings = 6,bool generateHousing = false,bool generateInhabitants = false)
        {
            for (int i = 0; i < count; i++)
            {
                yield return GenerateFlat(numberOfHousings, generateHousing, generateInhabitants);
            }
        }

        public static void SetUniqueHouseId(Housing housing,HousingDatabase housingDatabase)
        {
            while (housingDatabase.Contains(uniqueHouseNumber)) {}
            housing.houseNumber = uniqueHouseNumber;
        }
    }
}