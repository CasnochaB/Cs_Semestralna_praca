// See https://aka.ms/new-console-template for more information
using Database;
using HousingGenerators;

Console.WriteLine("Hello, World!");


Person person = new Person("Jano", "Ano", "000022/3", "WWWW");
Console.WriteLine(person.GetType().FullName);
Console.WriteLine(person.GetType().Name);
Flat flat = new Flat(25, 10);
Console.WriteLine(flat.GetType().Name);
Console.WriteLine(flat.GetType().FullName);
PersonGenerator.GeneratePerson();
Console.WriteLine();
