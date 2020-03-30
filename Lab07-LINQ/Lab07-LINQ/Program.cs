using System;
using System.IO;
using System.Linq;
using Lab07_LINQ.Classes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace Lab07_LINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            QueryData();
        }

        static RootObject ReadData(string path)
        {
            string jsonData = File.ReadAllText(path);
            RootObject deserializedRoot = JsonConvert.DeserializeObject<RootObject>(jsonData);
            return deserializedRoot;
        }

        static void QueryData()
        {
            RootObject hoodCollection = ReadData("../../../data.json");

            QueryAll(hoodCollection);

            Console.WriteLine("Press enter to see only named neighbourhoods.");
            Console.ReadLine();
            QueryNamed(hoodCollection);

            Console.WriteLine("Press enter to see all neighbourhoods without duplicates.");
            Console.ReadLine();
            QueryNoDupes(hoodCollection);
        }

        static void QueryAll(RootObject collection)
        {
            Console.Clear();
            Console.WriteLine("These are all the new york neighborhoods in my database: \n(press Enter)");
            Console.ReadLine();
            var query = from item in collection.features
                        select item.properties.neighborhood;

            int count = 0;
            foreach (string hood in query)
            {
                count++;
                Console.WriteLine(hood);
            }
            Console.WriteLine($"Total Neighborhoods: {count}");
        }

        static void QueryNamed(RootObject collection)
        {
            Console.Clear();
            Console.WriteLine("These are all the named new york neighborhoods: \n(press Enter)");
            Console.ReadLine();
            var query = from item in collection.features
                        where item.properties.neighborhood != ""
                        select item.properties.neighborhood;

            int count = 0;
            foreach (string hood in query)
            {
                count++;
                Console.WriteLine(hood);
            }
            Console.WriteLine($"Total Neighborhoods: {count}");
        }

        static void QueryNoDupes(RootObject collection)
        {
            Console.Clear();
            Console.WriteLine("These are all the new york neighborhoods, without duplicates: \n(press Enter)");
            Console.ReadLine();

            var query = from item in collection.features
                        where item.properties.neighborhood != ""
                        select item.properties.neighborhood;

            int count = 0;
            foreach (string hood in query.Distinct())
            {
                count++;
                Console.WriteLine(hood);
            }
            Console.WriteLine($"Total Neighborhoods: {count}");
        }
    }
}
