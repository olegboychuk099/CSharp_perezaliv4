using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using Csharp_laba4.Model;

namespace Csharp_laba4.Tools

{
    internal static class UsersCreator
    {
        public static List<Person> Users { get; }

        static UsersCreator()
        {
            var filepath = Path.Combine(GetAndCreateDataPath(), Person.filename);
            if (File.Exists(filepath))
            {
                try
                {
                    Users = SerializeHelper.Deserialize<List<Person>>(filepath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to get user list from file.{Environment.NewLine}{ex.Message}");
                    throw;
                }
            }
            else
            {
                Users = new List<Person>();
                Random rnd = new Random();
                for (int i = 0; i < 50; ++i)
                    Users.Add(new Person($"{(char)(rnd.Next(65, 90))}{(char)(rnd.Next(97, 122))}{(char)(rnd.Next(97, 122))}", $"{(char)(rnd.Next(65, 90))}{(char)(rnd.Next(97, 122))}{(char)(rnd.Next(97, 122))}", $"user{i}@gmail.com", new DateTime(rnd.Next(DateTime.Today.Year - 134, DateTime.Today.Year - 5), rnd.Next(1, 13), rnd.Next(1, 25))));
            
            }
        }

        internal static Person CreateUser(string firstName, string lastName, string email, DateTime date)
        {
            Person newPerson = new Person(firstName, lastName, email, date);
            Users.Add(newPerson);
            return newPerson;
        }

        internal static void SaveData()
        {
            SerializeHelper.Serialize(Users, Path.Combine(GetAndCreateDataPath(), Person.filename));
        }

        private static string GetAndCreateDataPath()
        {
            string dir = PersonControler.WorkingDirectory;
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            return dir;
        }

    }
}