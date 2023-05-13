using Database;

namespace HousingGenerators
{
    public static class HousingUnitGenerator
    {
        public static int uniqueHousingUnitID { 
            get 
            {
                return ++uniqueHousingUnitID;
            } 
            set 
            { 
                uniqueHousingUnitID = value;
            }
        }

        public static HousingUnit GenerateHousingUnit(Housing? superior,bool generateInhabitants = false)
        {
            HousingUnit housingUnit = new HousingUnit(uniqueHousingUnitID,superior);
            if (generateInhabitants)
            {
                //TODO
            }
            return housingUnit;
        }

        public static HousingUnit GenerateHousingUnit(bool generateInhabitants = false)
        {
            return GenerateHousingUnit(null,generateInhabitants);
        }

        public static IEnumerable<HousingUnit> GenerateHousingUnits(int count, bool generateInhabitants = false)
        {
            for (int i = 0; i < count; i++)
            {
                yield return GenerateHousingUnit(generateInhabitants);
            }
        }

        public static void SetUniqueHousingUnitID(HousingUnit housingUnit,Flat flat)
        {
            while (flat.Contains(uniqueHousingUnitID)) { }
            housingUnit.unitIdentifier = uniqueHousingUnitID;
        }
    }
}
