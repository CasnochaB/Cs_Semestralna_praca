using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Database.Person;

namespace Database
{
    public static class PersonRegister
    {
        private static HashSet<PersonalData> people = new HashSet<PersonalData>();

        public static void Add(PersonalData person)
        {
            people.Add(person);
        }
        public static void Remove(PersonalData person)
        {
            people.Remove(person);
        }

        public static bool Contains(PersonalData person)
        {
            return people.Contains(person);
        }
    }
}
