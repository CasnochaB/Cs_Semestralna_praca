﻿using System;
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
        private static Dictionary<string,PersonalData> people = new Dictionary<string,PersonalData>();
        public static int count {
            get 
            { 
                return people.Count; 
            } 
        }
        public static void Add(PersonalData personalData)
        {
            people.Add(personalData.IdentificationNumber,personalData);
        }
        public static void Remove(PersonalData person)
        {
            people.Remove(person.IdentificationNumber);
        }

        public static bool Contains(PersonalData person)
        {
            return people.ContainsKey(person.IdentificationNumber);
        }

        public static bool Contains(string personID) { 
            return people.ContainsKey(personID);
        }

        public static void Clear() { 
            people.Clear();
        }

        public static int GetNumberOfPeople() {
            return people.Count;
        }
    }
}
