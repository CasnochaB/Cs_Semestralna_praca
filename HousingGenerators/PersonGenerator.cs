using Database;

namespace HousingGenerators
{
    public static class PersonGenerator
    {

        private static readonly string[] maleFirstNames = { "Michal", "Tomáš", "Lukáš", "Ján", "Peter", "Martin", "Marek", "Adam", "Andrej", "Matúš" };
        private static readonly string[] femaleFirstNames = { "Lucia", "Monika", "Zuzana", "Veronika", "Ivana", "Andrea", "Lenka", "Petra", "Jana", "Karolína" };
        private static readonly string[] maleLastNames = { "Novák", "Horváth", "Kováč", "Varga", "Tóth", "Nagy", "Szabó", "Farkaš", "Baláž", "Molnár" };
        private static readonly string[] femaleLastNames = { "Nováková", "Horváthová", "Kováčová", "Vargová", "Tóthová", "Nagyová", "Szabóová", "Farkašová", "Balážová", "Molnárová" };

        private static readonly Random random = new Random();
        private static int uniqueID = 0;
        private static int lowerCountSpan = 4;
        private static int upperCountSpan = 8;
        public static string GetUniqueID()
        {
            DateTime date = GetRandomBirthDate();
            string firstPart = (date.Year % 100).ToString("D2") + date.Month.ToString("D2") + date.Day.ToString("D2");
            uniqueID = random.Next(0, 10000);
            string secondPart = uniqueID.ToString("D4");
            string id = firstPart + "/" + secondPart;
            while (PersonRegister.Contains(id))
            {
                uniqueID++;
                if (uniqueID > 9999)
                {
                    uniqueID = 0;
                }
                secondPart = uniqueID.ToString("D4");
                id = firstPart + "/" + secondPart;
            }
            return id;
        }
        public static string GetRandomFirstName(bool isMale)
        {
            if (isMale)
            {
                return maleFirstNames[random.Next(10)];
            }
            else
            {
                return femaleFirstNames[random.Next(10)];
            }
        }
        public static string GetRandomLastName(bool isMale)
        {
            if (isMale)
            {
                return maleLastNames[random.Next(10)];
            }
            else
            {
                return femaleLastNames[random.Next(10)];
            }

        }

        public static DateTime GetRandomBirthDate()
        {
            int year = random.Next(DateTime.Today.Year - 99, DateTime.Today.Year + 1);
            int month = random.Next(1, 13);
            int daysInMonth = DateTime.DaysInMonth(year, month);
            int day = random.Next(1, daysInMonth + 1);
            DateTime randomDate = new DateTime(year, month, day);
            return randomDate;
        }

        public static Person GeneratePerson(string firstName, string lastName)
        {
            string temp = GetUniqueID();
            Person person;
            person = new Person(firstName, lastName, temp);
            return person;
        }

        public static Person GeneratePerson()
        {
            bool isMale = RandomTrueFalse();
            return GeneratePerson(GetRandomFirstName(isMale), GetRandomLastName(isMale));
        }

        private static bool RandomTrueFalse()
        {
            return random.Next(2) == 0;
        }

        public static IEnumerable<Person> GeneratePeople(int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return GeneratePerson();
            }
        }

        public static IEnumerable<Person> GeneratePeople()
        {
            int count = random.Next(lowerCountSpan, upperCountSpan + 1);
            return GeneratePeople(count);
        }

        public static void SetCountSpan(int lower, int upper)
        {
            if (lower <= upper)
            {
                lowerCountSpan = lower;
                upperCountSpan = upper;
            }
        }
    }
}
