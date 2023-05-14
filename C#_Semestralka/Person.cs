namespace Database
{
    public class Person
    {
        public struct PersonalData
        {
            public PersonalData(string firstName, string lastName, string identificationNumber)
            {
                this.firstName = firstName;
                this.lastName = lastName;
                this.identificationNumber = identificationNumber;
            }

            public string firstName { get; }
            public string lastName { get; }
            public string identificationNumber { get; }
        }

        public PersonalData personalData { get; }
        public int age { get {  return new DateTime(DateTime.Today.Ticks - birthDate.Ticks).Year; } }
        public string fullName { get {
                return personalData.firstName + " " + personalData.lastName;
            }
        }
        public string? address { get; set;}


        public DateTime birthDate { get 
            {
                int year = Int32.Parse(personalData.identificationNumber.Substring(0, 2));
                return new DateTime(
                    year < DateTime.Today.Year+1 ? 2000+year : 1900+year,
                    int.Parse(personalData.identificationNumber.Substring(2, 2)),
                    int.Parse(personalData.identificationNumber.Substring(4, 2)));   
            }
        }

        public Person(string firstName, string lastName, string identificationNumber, string? address = null)
        {
            PersonalData data = new PersonalData(firstName,lastName,identificationNumber);
            if (PersonRegister.Contains(data))
            {
                throw new Exception("Personal data is not unique");
            } else
            {
                PersonRegister.Add(data);
                personalData = data;
            }
            this.address = address;
        }




    }
}