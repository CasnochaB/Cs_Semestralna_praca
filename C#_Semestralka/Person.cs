namespace Database
{
    public class Person
    {
        public string firstName { get; }
        public string lastName { get; }
        public string identificationNumber { get; }
        public int age { get {  return new DateTime(DateTime.Today.Ticks - birthDate.Ticks).Year; } }
        public string fullName { get {
                return firstName + " " + lastName;
            }
        }
        public string? address { get; set;}


        public DateTime birthDate { get 
            {
                int year = Int32.Parse(identificationNumber.Substring(0, 2));
                return new DateTime(
                    year < DateTime.Today.Year+1 ? 2000+year : 1900+year,
                    int.Parse(identificationNumber.Substring(2, 2)),
                    int.Parse(identificationNumber.Substring(4, 2)));   
            }
        }

        public Person(string firstName, string lastName, string identificationNumber, string? address = null)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.identificationNumber = identificationNumber;
            this.address = address;
        }




    }
}