namespace Database
{


    public class Flat : Housing, IEnumerable<HousingUnit>
    {
        private Dictionary<int, HousingUnit> housingUnits;

        public override int numberOfInhabitants => housingUnits.SelectMany(unit => unit.Value.GetInhabitants()).Distinct().Count();

        public override int numberOfHousingUnits => housingUnits.Count;

        public Flat(int houseNumber, int housingUnitsCount = 0) : base(houseNumber)
        {
            housingUnits = new Dictionary<int, HousingUnit>();
            for (int i = 0; i < housingUnitsCount; i++)
            {
                Add();
            }
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
            ClearInhabitants();
            housingUnits.Clear();
        }

        public override void ClearInhabitants()
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
                housingUnit.unitOrder = GetNewUnitID();
            }
            housingUnits.Add(housingUnit.unitOrder, housingUnit);
            return true;
        }

        private int GetNewUnitID()
        {
            int i = 1;
            while (housingUnits.ContainsKey(i))
            {
                i++;
            }
            return i;
        }

        public bool Add()
        {
            return Add(new HousingUnit(GetNewUnitID(), this));
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
            return housingUnits.Values.SelectMany(n => n.Where(predicate));
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
            return housingUnits.Values.Where(n => predicate(n));
        }

        public override IEnumerator<HousingUnit> GetEnumerator()
        {
            return housingUnits.Values.GetEnumerator();
        }

        public void Remove(HousingUnit housingUnit)
        {
            housingUnits.Remove(housingUnit.unitOrder);
        }

        public bool Contains(int housingID)
        {
            return housingUnits.ContainsKey(housingID);
        }
    }
}
