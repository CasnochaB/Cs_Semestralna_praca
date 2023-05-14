﻿using System.Globalization;

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
                return GetBirthDate(personalData.identificationNumber);
            }
        }

        private static DateTime GetBirthDate(string identificationNumber)
        {
            int year = 2000 + Int32.Parse(identificationNumber.Substring(0, 2));
            return new DateTime(
                year < DateTime.Today.Year + 1 ? year : year-100,
                int.Parse(identificationNumber.Substring(2, 2)),
                int.Parse(identificationNumber.Substring(4, 2)));
        }

        public Person(string firstName, string lastName, string identificationNumber, string? address = null)
        {
            PersonalData data = new PersonalData(firstName, lastName, identificationNumber); 
            if (PersonRegister.Contains(data))
            {
                throw new ArgumentException("Identification number must be unique");
            }
            if (!CheckIDValidity(identificationNumber))
            {
                throw new ArgumentException("Identification number is invalid");
            }
            PersonRegister.Add(data);
            personalData = data;
            this.address = address;
        }

        public static bool CheckIDValidity(string identificationNumber)
        {
            string[] parts = identificationNumber.Split("/");
            if (parts.Length != 2) { 
                return false;
            }
            if (parts[0].Length != 6) { return false; }
            if (parts[1].Length != 4) { return false; }
            DateTime birthdate = GetBirthDate(parts[0]);
            int year = Int32.Parse(identificationNumber.Substring(0, 2));
            string dateString = (year < DateTime.Today.Year + 1 ? "20" : "19") + parts[0];
            return DateTime.TryParseExact(dateString, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }


    }
}