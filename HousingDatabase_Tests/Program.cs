// See https://aka.ms/new-console-template for more information
using Database;
using HousingGenerators;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        //testPersonGeneration("Janko", "Hrasko", "552315");
        //testPersonGeneration("Janko", "Hrasko", "551215/1220");
        //testPersonGeneration("Janko", "Hrasko", "551215/1220");
        //testPersonGeneration("Janko", "Hrasko", "22/22");
        //testPersonGeneration("Janko", "Hrasko", "551215/1221");

        HousingDatabase database = new HousingDatabase();

        Person person = new Person("Asterix", "Obelix", "000101/1111");

        for (int i = 0; i < 100; i++)
        {
            PersonGenerator.SetCountSpan(2, 8);
            var house = HousingGenerator.GenerateHouse(true);
            Console.WriteLine(house.houseNumber);
            PersonGenerator.SetCountSpan(1, 5);
            var flat = HousingGenerator.GenerateFlat(14, true, true);
            Console.WriteLine(flat.houseNumber);
            if (!database.Add(flat))
            {
                flat.houseNumber = HousingGenerator.GetUniqueHousingId(database);
                database.Add(flat);
            }
            if (!database.Add(house))
            {
                house.houseNumber = HousingGenerator.GetUniqueHousingId(database);
                database.Add(house);
            }
            house.Add(person);
        }
        //database.PrintContent();
        Console.WriteLine(database.GetNumberOfInhabitants());
        Console.WriteLine(PersonRegister.GetNumberOfPeople());
        FileInfo fileInfo = new FileInfo("C:\\Users\\brano\\OneDrive\\Dokumenty\\!CSharp\\Semestralna_praca\\C#_Semestralka\\sample4.csv");
        database.Save(fileInfo);
        database.Clear();
        database.Load(fileInfo);
    }

    public static void testPersonGeneration(string firstName, string lastName, string ID)
    {
        try
        {
            Person person = new Person(firstName, lastName, ID);
            Console.WriteLine(person.fullName + " " + ID);
        }
        catch (Exception ex)
        {
            Console.Write("ERROR " + firstName + " " + lastName + " " + ID + ":    ");
            Console.WriteLine(ex.Message);
        }
    }



}