using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{


    public class House : Housing
    {
        HousingUnit housingUnit;

        public House(int houseNumber) : base(houseNumber)
        {
            housingUnit = new HousingUnit(0);
            housingUnit.SetSuperiorHousing(this);
        }

        public override int numberOfInhabitants => housingUnit.numberOfInhabitants;

        public override int numberOfHousingUnits => 1;


        public override void Clear()
        {
            housingUnit.Clear();
        }

        public override IEnumerable<HousingUnit> GetHousingUnits()
        {
            return new HousingUnit[] { housingUnit };
        }

        public override IEnumerable<Person> GetInhabitants()
        {
            return housingUnit.GetInhabitants();
        }

        public override void ClearInhabitants()
        {
            housingUnit.Clear();
        }

        public void Add(Person person)
        {
            housingUnit.Add(person);
        }

        public override HousingUnit? GetHousingUnit(int? housingID = null)
        {
            return housingUnit;
        }

        public override IEnumerable<Person> Where(Predicate<Person> predicate)
        {
            return housingUnit.Where(predicate);
        }

        public override IEnumerable<Person> Where(Func<Person, bool> predicate)
        {
            return housingUnit.Where(predicate);
        }

        //public override IEnumerable<HousingUnit> Where(Func<HousingUnit, bool> predicate)
        //{
        //    return predicate(housingUnit) ? (IEnumerable<HousingUnit>)housingUnit : Enumerable.Empty<HousingUnit>();
        //}

        public override IEnumerable<HousingUnit> Where(Predicate<HousingUnit> predicate)
        {
            if (predicate(housingUnit))
            {
                yield return housingUnit;
            }
        }

        public override IEnumerator<HousingUnit> GetEnumerator()
        {
            yield return housingUnit;
        }
    }
}
