using System.Globalization;

namespace Database
{
    public class Person
    {
        public struct PersonalData
        {
            public PersonalData(string firstName, string lastName, string identificationNumber)
            {
                this.FirstName = firstName;
                this.LastName = lastName;
                this.IdentificationNumber = identificationNumber;
            }

            public string FirstName { get; }
            public string LastName { get; }
            public string IdentificationNumber { get; }
        }

        public PersonalData personalData { get; set; }
        public int age
        {
            get
            {
                DateTime today = DateTime.Today;
                int age = today.Year - birthDate.Year;

                if (birthDate > today.AddYears(-age))
                {
                    age--;
                }

                return age;
            }
        }
        public string fullName
        {
            get
            {
                return personalData.FirstName + " " + personalData.LastName;
            }
        }

        public DateTime birthDate
        {
            get
            {
                return GetBirthDate(personalData.IdentificationNumber);
            }
        }

        private static DateTime GetBirthDate(string identificationNumber)
        {
            int year = 2000 + Int32.Parse(identificationNumber.Substring(0, 2));
            return new DateTime(
                year < DateTime.Today.Year + 1 ? year : year - 100,
                int.Parse(identificationNumber.Substring(2, 2)),
                int.Parse(identificationNumber.Substring(4, 2)));
        }

        public Person(string firstName, string lastName, string identificationNumber)
        {
            if (PersonRegister.Contains(identificationNumber))
            {
                throw new ArgumentException("Identification number must be unique");
            }
            if (!CheckIDValidity(identificationNumber))
            {
                throw new ArgumentException("Identification number is invalid");
            }
            personalData = new PersonalData(firstName, lastName, identificationNumber);
            PersonRegister.Add(this);
        }

        public static bool CheckIDValidity(string identificationNumber)
        {
            string[] parts = identificationNumber.Split("/");
            if (parts.Length != 2)
            {
                return false;
            }
            if (parts[0].Length != 6) { return false; }
            if (parts[1].Length != 4) { return false; }
            int year = Int32.Parse(identificationNumber.Substring(0, 2));
            string dateString = (year < DateTime.Today.Year + 1 ? "20" : "19") + parts[0];
            return DateTime.TryParseExact(dateString, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }


    }
}