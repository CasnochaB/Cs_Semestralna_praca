using System.Collections;
using System.Text;

namespace Database
{
    public class HousingDatabase : IEnumerable<Housing>
    {
        public Dictionary<(string personID, string address), Person> exportHousings = new Dictionary<(string, string), Person>();
        private readonly Dictionary<int, Housing> housings;

        public HousingDatabase()
        {
            housings = new Dictionary<int, Housing>();
        }

        public void PrintContent()
        {
            foreach (var housing in housings)
            {
                Console.WriteLine(housing.Value.houseNumber);
                foreach (var housingUnit in housing.Value)
                {
                    Console.WriteLine(" " + housingUnit.unitIdentifier);
                    foreach (var person in housingUnit)
                    {
                        Console.WriteLine("    " + person.fullName);
                    }
                }
            }
        }

        public bool Add(Housing housing)
        {
            if (Contains(housing.houseNumber))
            {
                return false;
            }
            housings.Add(housing.houseNumber, housing);
            return true;
        }
        public bool Remove(Housing housing)
        {
            return housings.Remove(housing.houseNumber);
        }

        public IEnumerable<Housing> GetHousings()
        {
            return housings.Values;
        }

        public int GetNumberOfInhabitants()
        {
            return housings.SelectMany(n => n.Value.GetInhabitants()).Distinct().Count();
        }

        public int GetNumberOfInstances()
        {
            return housings.SelectMany(n => n.Value.GetHousingUnits().SelectMany(m => m.GetInhabitants())).Count();
        }

        public void Save(FileInfo fileInfo)
        {
            StreamWriter streamWriter = new StreamWriter(fileInfo.FullName, false, Encoding.UTF8);
            foreach (var housing in housings)
            {
                streamWriter.Flush();
                foreach (var housingUnit in housing.Value)
                {
                    foreach (var person in housingUnit)
                    {

                        WritePersonToFile(streamWriter, housingUnit.unitIdentifier, person);
                    }
                }
            }
            streamWriter.Close();
        }

        private static void WritePersonToFile(StreamWriter streamWriter, string adress, Person person)
        {
            streamWriter.WriteLine(person.personalData.FirstName + ";" +
                                                           person.personalData.LastName + ";" +
                                                           person.personalData.IdentificationNumber + ";" +
                                                           adress);
        }

        public void Load(FileInfo fileInfo)
        {
            housings.Clear();
            PersonRegister.Clear();
            Import(fileInfo);
        }

        public int Import(FileInfo fileInfo)
        {
            StreamReader streamReader;
            if (File.Exists(fileInfo.FullName))
            {
                streamReader = new StreamReader(File.OpenRead(fileInfo.FullName));
                int collisions = 0;
                while (!streamReader.EndOfStream)
                {
                    var line = streamReader.ReadLine();
                    if (line != null)
                    {
                        string[] strings = line.Split(";");
                        string firstName = strings[0];
                        string lastName = strings[1];
                        string id = strings[2];
                        string adress = strings[3];
                        string[] adressParts = adress.Split("/");
                        string houseNumberString = adressParts[0];
                        int houseNumberInteger = Int32.Parse(houseNumberString);
                        Person person;
                        try
                        {
                            person = new Person(firstName, lastName, id);
                        }
                        catch
                        {
                            collisions++;
                            person = PersonRegister.Get(id);
                        }
                        if (adressParts.Length == 2)
                        {
                            string housingNumber = adressParts[1];
                            int housingNumberInteger = Int32.Parse(housingNumber);
                            Flat flat = new Flat(houseNumberInteger);
                            if (!housings.ContainsKey(houseNumberInteger))
                            {
                                housings.Add(flat.houseNumber, flat);
                            }
                            HousingUnit housingUnit = new HousingUnit(housingNumberInteger);
                            flat = (Flat)housings[houseNumberInteger];
                            flat.Add(housingUnit);
                            housingUnit.Add(person);
                        }
                        else
                        {
                            House house = new House(houseNumberInteger);
                            if (!housings.ContainsKey(houseNumberInteger))
                            {
                                housings.Add(houseNumberInteger, house);
                            }
                            housings[houseNumberInteger].Add(person);
                        }

                    }
                }
                streamReader.Close();
                return collisions;
            }
            return 0;
        }

        public void Export(FileInfo fileInfo)
        {
            StreamWriter streamWriter = new StreamWriter(File.OpenWrite(fileInfo.FullName), Encoding.UTF8);
            foreach (var person in exportHousings)
            {
                WritePersonToFile(streamWriter, person.Key.address, person.Value);
            }
            streamWriter.Close();
            ClearExport();
        }

        public void AddToExport(Housing housing)
        {
            foreach (var housingUnit in housing)
            {
                AddToExport(housingUnit);
            }
        }

        public void AddToExport(Person person, string address)
        {
            if (!exportHousings.ContainsKey((person.personalData.IdentificationNumber, address)))
            {
                exportHousings.Add((person.personalData.IdentificationNumber, address), person);
            }
        }

        public void AddToExport(HousingUnit housingUnit)
        {
            foreach (var person in housingUnit)
            {
                AddToExport(person, housingUnit.unitIdentifier);
            }
        }

        public void AddToExport(IEnumerable<Housing> housings)
        {
            foreach (var item in housings)
            {
                AddToExport(item);
            }
        }

        public void ClearExport()
        {
            exportHousings.Clear();
        }

        public void AddToExport(IEnumerable<HousingUnit> housingUnits)
        {
            foreach (var item in housingUnits)
            {
                AddToExport(item);
            }
        }

        public void Clear()
        {
            housings.Clear();
        }

        public bool Contains(int houseId)
        {
            return housings.ContainsKey(houseId);
        }

        public IEnumerable<Housing> Where(Predicate<Housing> predicate)
        {
            return housings.Values.Where(housing => predicate(housing));
        }

        public IEnumerable<HousingUnit> Where(Predicate<HousingUnit> predicate)
        {
            return housings.Values.SelectMany(n => n.Where(predicate));
        }

        public IEnumerable<Person> Where(Predicate<Person> predicate)
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
