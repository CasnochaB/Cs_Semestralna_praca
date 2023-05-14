using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public abstract class Housing : IEnumerable<HousingUnit>
    {
        public int houseNumber { get; set; }
        public abstract int numberOfInhabitants { get; }
        public abstract int numberOfHousingUnits { get; }


        protected Housing(int number) { houseNumber = number; }
        public abstract IEnumerable<HousingUnit> GetHousingUnits();
        public abstract IEnumerable<Person> GetInhabitants();

        public abstract void Clear();
        public abstract void RemoveInhabitants();
        public void SetHouseNumber(int houseNumber) { this.houseNumber = houseNumber;}

        public abstract HousingUnit? GetHousingUnit(int? housingID = null);
        public bool Add(IEnumerable<Person> people, int housingID = 1)
        {
            foreach (var item in people)
            {
                if (Add(item, housingID) == false) {  return false; }
            }
            return true;
        }

        public bool Add(Person person, int housingID = 1)
        {
            var housing = GetHousingUnit(housingID);
            if (housing == null) 
            {
                return false; 
            }
            housing.Add(person);
            return true;
        }

        public abstract IEnumerable<Person> Where(Predicate<Person> predicate);
        public abstract IEnumerable<Person> Where(Func<Person, bool> predicate);
        //public abstract IEnumerable<HousingUnit> Where(Func<HousingUnit, bool> predicate);
        public abstract IEnumerable<HousingUnit> Where(Predicate<HousingUnit> predicate);
        public abstract IEnumerator<HousingUnit> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
