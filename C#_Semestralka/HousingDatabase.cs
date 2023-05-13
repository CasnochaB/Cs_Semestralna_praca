using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class HousingDatabase : IEnumerable<Housing>
    {
        private static int House_Number = 0;
        private List<Person> exportHousings = new List<Person>();
        
        public static int GetHouseNumber()
        {
            return House_Number++;
        }
        private Dictionary<int,Housing> housings;

        public HousingDatabase() {
            housings = new Dictionary<int, Housing>();
        }

        public void Add(Housing housing)
        {
            housings.Add(housing.houseNumber,housing);
        }
        public bool Remove(Housing housing)
        {
            return housings.Remove(housing.houseNumber);
        }

        public IEnumerable<Housing> GetHousings() {
            return housings.Values;
        }

        public void Save(FileInfo fileInfo)
        {
            StreamWriter streamWriter = new StreamWriter(fileInfo.FullName);
           
        }
        
        public void Load(FileInfo fileInfo)
        {
            housings.Clear();
            Import(fileInfo);
        }

        public void Import(FileInfo fileInfo)
        {

        }

        public void Export(FileInfo fileInfo)
        {

        }

        public void AddToExport(Housing housing)
        {
            foreach (var person in housing.GetInhabitants())
            {
                AddToExport(person);
            }
        }
        
        public void AddToExport(Person person)
        {
            exportHousings.Add(person);
        }
        
        public void AddToExport(HousingUnit housingUnit)
        {
            foreach (var person in housingUnit.GetInhabitants())
            {
                AddToExport(person);
            }
        }

        public void AddToExport(IEnumerable objects)
        {
            foreach (var item in objects)
            {
                switch (item.GetType().FullName) {
                    case "Database.House":
                    case "Database.Flat":
                        AddToExport((Housing)item); break;
                    case "Database.Person":
                        AddToExport((Person)item); break;
                    case "Database.HousingUnit":
                        AddToExport((HousingUnit)item); break;
                    default: break;
                }
            }
        }

        public bool Contains(int houseId)
        {
            return housings.ContainsKey(houseId);
        }

        public IEnumerable<Housing> Where(Predicate<Housing> predicate) 
        {
            return housings.Values.Where(housing => predicate(housing));
        }

        public IEnumerable<HousingUnit> Where(Func<HousingUnit, bool> predicate)
        {
            return housings.Values.SelectMany(n=> n.Where(predicate));  
        }

        public IEnumerable<Person> Where(Func<Person, bool> predicate)
        {
            return housings.Values.SelectMany(n => n.Where(predicate));
        }

        public IEnumerator<Housing> GetEnumerator()
        {
            return housings.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
