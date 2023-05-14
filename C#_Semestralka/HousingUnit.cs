using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Database
{
    public class HousingUnit: IEnumerable<Person>
    {
        private Dictionary<string,Person> inhabitants;
        public int numberOfInhabitants { get {  return inhabitants.Count; } }
        public int unitOrder;
        public string unitIdentifier { get {
                return superiorHousing == null ? "Unknown" : superiorHousing.houseNumber.ToString()  + (unitOrder == 0 ? "" : "/" + unitOrder.ToString());    
            }
        }

        private Housing? superiorHousing = null;

        public Housing? GetSuperiorHousing()
        {
            return superiorHousing;
        }

        public void SetSuperiorHousing(Housing superiorHousing)
        {
            this.superiorHousing = superiorHousing;          
        }

        public HousingUnit(int unitIdentifier, Housing? superiorResidence = null)
        {
            inhabitants = new Dictionary<string,Person>();
            this.unitOrder = unitIdentifier;
            this.superiorHousing = superiorResidence;
        }

        public HousingUnit(Housing? superiorHousing) : this(1, superiorHousing) { }
        

        public void Add(Person person)
        {
            if (person != null) {
                inhabitants.Add(person.personalData.identificationNumber,person);
            }
        }
        public void Add(string firstName,string lastName, string identificationNumber)
        {
            Add(new Person(firstName, lastName, identificationNumber));
        }

        public void Add(IEnumerable<Person> people)
        {
            foreach (var person in people)
            {
                Add(person);
            }; 
        }


        public bool Remove(Person person)
        {
            return inhabitants.Remove(person.personalData.identificationNumber);
        }

        public bool Remove(string identificationNumber)
        {
            return inhabitants.Remove(identificationNumber);
        }

        public bool Remove(IEnumerable<Person> peopleToRemove) {
            if (inhabitants.Values.All(n=> peopleToRemove.Contains(n)))
            {
                foreach (var person in peopleToRemove)
                {
                    inhabitants.Remove(person.personalData.identificationNumber);
                }
                return true;
            }
            return false;
        }

        //public bool removeInhabitants(int lowSpan,int upSpan) {
            
        //    if( lowSpan < 0 || upSpan < lowSpan || upSpan < numberOfInhabitants)
        //    {
        //        return false;
        //    }
        //    int range = upSpan - lowSpan;
        //    inhabitants.RemoveRange(lowSpan, range);
        //    return true;
        //}

        public bool Remove(Func<Person,bool> match)
        {
            var newItems = inhabitants.Values.Where(match);
            foreach (var item in newItems)
            {
                Remove(item);
            }
            return true;
        }

        public IEnumerable<Person> GetInhabitants()
        {
            return inhabitants.Values;
        }

        public IEnumerator<Person> GetEnumerator()
        {
            return inhabitants.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Clear()
        {
            inhabitants.Clear();
        }

        public IEnumerable<Person> Where(Predicate<Person> match)
        {
            return inhabitants.Values.Where((person) => match(person));
        }

        //public IEnumerable<Person> Where(Func<Person,bool> match)
        //{
        //    return inhabitants.Values.Where(match);
        //}
    }
}
