using Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HousingGenerators
{
    public static class PersonGenerator
    {
        public static DateOnly birthDate {  get; set; }
        public static int uniqueID = 0;

        public static int GetNextUniqueID()
        {
            uniqueID++;
            return uniqueID;
        }

        public static string randomFirstName { 
            get 
            {
                //TODO
                return "Meno";  
            } 
        }
        public static string randomLastName
        {
            get
            {
                //TODO
                return "Priezvisko";
            }
        }

        public static DateTime randomBirthDate
        {
            //TODO
            get
            {
                return DateTime.Today;                
            }
        }

        public static Person GeneratePerson(string firstName,string lastName)
        {
            DateTime date = randomBirthDate;
            string IDnumber = (date.Year%100).ToString("D2") + date.Month.ToString("D2")  + date.Day.ToString("D2") + "/" + GetNextUniqueID().ToString("D4");
            Console.WriteLine(IDnumber);
            return new Person(firstName, lastName,IDnumber);
        }

        public static Person GeneratePerson()
        {
            return GeneratePerson(randomFirstName,randomLastName);
        }

        public static IEnumerable<Person> GeneratePeople(int count) {
            for (int i = 0; i < count; i++)
            {
                yield return GeneratePerson();
            }
        }

    }
}
