﻿using Database;

namespace HousingGenerators
{
    public static class HousingUnitGenerator
    {
        public static HousingUnit GenerateHousingUnit(Housing? superior, bool generateInhabitants = false)
        {
            HousingUnit housingUnit = new HousingUnit(superior);
            if (generateInhabitants)
            {
                housingUnit.Add(PersonGenerator.GeneratePeople());
            }
            return housingUnit;
        }

        public static HousingUnit GenerateHousingUnit(bool generateInhabitants = false)
        {
            return GenerateHousingUnit(null, generateInhabitants);
        }

        public static IEnumerable<HousingUnit> GenerateHousingUnits(int count, bool generateInhabitants = false)
        {
            for (int i = 0; i < count; i++)
            {
                yield return GenerateHousingUnit(generateInhabitants);
            }
        }

    }
}
