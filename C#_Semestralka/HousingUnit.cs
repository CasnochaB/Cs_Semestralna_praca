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
        public int unitIdentifier { get; set; }
        string nameOfUnit { get {
                return superiorHousing == null ? "Unknown" : superiorHousing.houseNumber.ToString()  + "/" + unitIdentifier.ToString();    
            }
        }
        public Housing? superiorHousing 
        { 
            get 
            { 
                return superiorHousing; 
            }
            set 
            {
                superiorHousing = value;
                foreach (var valuePair in inhabitants)
                {
                    valuePair.Value.address = nameOfUnit;
                }
            }
        }

        public HousingUnit(int unitIdentifier, Housing? superiorResidence = null)
        {
            inhabitants = new Dictionary<string,Person>();
            this.unitIdentifier = unitIdentifier;
            this.superiorHousing = superiorResidence;
        }

        public void Add(Person person)
        {
            if (person != null) {
                inhabitants.Add(person.personalData.identificationNumber,person);
            }
        }
        public void Add(string firstName,string lastName, string identificationNumber)
        {
            Add(new Person(firstName, lastName, identificationNumber,nameOfUnit));
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
            if (inhabitants.Select(n => n.Key).Contains(identificationNumber))
            {
                inhabitants.Remove(identificationNumber);
                return true;
            }
            return false;
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
            return ((IEnumerable<Person>)inhabitants).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)inhabitants).GetEnumerator();
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
