﻿using Database;

namespace HousingGenerators
{

    public static class HousingGenerator
    {

        public static int uniqueHouseNumber = 1;

        public static House GenerateHouse(bool generateInhabitants = false)
        {
            House house = new House(GetUniqueHousingId());
            if (generateInhabitants)
            {
                foreach (var person in PersonGenerator.GeneratePeople())
                {
                    house.Add(person);
                }
            }
            return house;
        }

        public static Flat GenerateFlat(int numberOfHousingUnits = 6, bool generateHousingUnits = false, bool generateInhabitants = false)
        {
            Flat flat = new Flat(GetUniqueHousingId());
            if (generateHousingUnits)
            {
                foreach (var item in HousingUnitGenerator.GenerateHousingUnits(numberOfHousingUnits, generateInhabitants))
                {
                    flat.Add(item);
                }
            }
            return flat;
        }

        public static IEnumerable<House> GenerateHouses(int count, bool generateInhabitants = false)
        {
            for (int i = 0; i < count; i++)
            {
                yield return GenerateHouse(generateInhabitants);
                uniqueHouseNumber++;
            }
        }

        public static IEnumerable<Flat> GenerateFlats(int count, int numberOfHousings = 6, bool generateHousing = false, bool generateInhabitants = false)
        {
            for (int i = 0; i < count; i++)
            {
                yield return GenerateFlat(numberOfHousings, generateHousing, generateInhabitants);
                uniqueHouseNumber++;
            }
        }
        private static int GetUniqueHousingId()
        {
            return uniqueHouseNumber++;
        }
        public static int GetUniqueHousingId(HousingDatabase housingDatabase)
        {
            while (housingDatabase.Contains(uniqueHouseNumber))
            {
                uniqueHouseNumber++;
            }
            return uniqueHouseNumber;
        }
    }
}