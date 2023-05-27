namespace Database
{
    public static class PersonRegister
    {
        private static Dictionary<string, Person> people = new Dictionary<string, Person>();
        public static int count
        {
            get
            {
                return people.Count;
            }
        }
        public static void Add(Person person)
        {
            people.Add(person.personalData.IdentificationNumber, person);
        }
        public static void Remove(Person person)
        {
            people.Remove(person.personalData.IdentificationNumber);
        }

        public static bool Contains(Person person)
        {
            return people.ContainsKey(person.personalData.IdentificationNumber);
        }

        public static Person Get(string identificationNumber) { return people[identificationNumber]; }

        public static bool Contains(string personID)
        {
            return people.ContainsKey(personID);
        }

        public static void Clear()
        {
            people.Clear();
        }

        public static int GetNumberOfPeople()
        {
            return people.Count;
        }

        public static IEnumerable<Person> GetAll()
        {
            return people.Values;
        }
    }
}
