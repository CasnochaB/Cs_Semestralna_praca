using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{


    public class Flat : Housing,IEnumerable<HousingUnit>
    {
        private Dictionary<int,HousingUnit> housingUnits;
        private int currentHousingUnitID = 1;

        public override int numberOfInhabitants => housingUnits.SelectMany(unit => unit.Value.GetInhabitants()).Distinct().Count();

        public override int numberOfHousingUnits => housingUnits.Count;

        public Flat(int houseNumber) : base(houseNumber) {
            housingUnits = new Dictionary<int, HousingUnit>();
        }

        public override IEnumerable<HousingUnit> GetHousingUnits()
        {
            return housingUnits.Values;
        }

        public override IEnumerable<Person> GetInhabitants()
        {
            return housingUnits.SelectMany(n => n.Value.GetInhabitants());
        }

        public override void Clear()
        {
            RemoveInhabitants();
            housingUnits.Clear();
        }

        public override void RemoveInhabitants()
        {
            foreach (var item in housingUnits)
            {
                item.Value.Clear();
            }
        }

        //public bool Add(IEnumerable<HousingUnit> housingUnits)
        //{
        //    foreach (var item in housingUnits)
        //    {
        //        Add(item);
        //    }
        //    return true;
        //}

        public bool Add(HousingUnit housingUnit)
        {
            housingUnit.SetSuperiorHousing(this);
            while (housingUnits.ContainsKey(housingUnit.unitOrder))
            {
                housingUnit.unitOrder = currentHousingUnitID++;
            }
            housingUnits.Add(housingUnit.unitOrder,housingUnit);
            return true;
        }

        public bool Add()
        {
            return Add(new HousingUnit(currentHousingUnitID, this));
        }

        public override HousingUnit? GetHousingUnit(int? housingID)
        {
            if (housingID != null)
            {
                return housingUnits.Where(n => n.Key == housingID).First().Value;
            }
            return null;
        }

        public override IEnumerable<Person> Where(Predicate<Person> predicate)
        {
            return housingUnits.Values.SelectMany(n=> n.Where(predicate));
        }

        public override IEnumerable<Person> Where(Func<Person, bool> predicate)
        {
            return housingUnits.Values.SelectMany(n => n.Where(predicate));
        }

        //public override IEnumerable<HousingUnit> Where(Func<HousingUnit, bool> predicate)
        //{
        //    return housingUnits.Values.Where(predicate);
        //}

        public override IEnumerable<HousingUnit> Where(Predicate<HousingUnit> predicate)
        {
            return housingUnits.Values.Where(n=> predicate(n));
        }

        public override IEnumerator<HousingUnit> GetEnumerator()
        {
            return housingUnits.Values.GetEnumerator();
        }

        public bool Contains(int housingID)
        {
            return housingUnits.ContainsKey(housingID);
        }
    }
}
