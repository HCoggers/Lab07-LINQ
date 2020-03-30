using System;
using System.IO;
using System.Linq;
using Lab08_LINQ.Classes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace Lab08_LINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            QueryData();
        }

        /// <summary>
        /// Reads a json file and deserializes it to a RootObject
        /// </summary>
        /// <param name="path">path of json file</param>
        /// <returns>deserialized Json data as a RootObject</returns>
        static RootObject ReadData(string path)
        {
            string jsonData = File.ReadAllText(path);
            RootObject deserializedRoot = JsonConvert.DeserializeObject<RootObject>(jsonData);
            return deserializedRoot;
        }

        /// <summary>
        /// Calls various linq queries on a dataset of new york neighborhoods.
        /// </summary>
        static void QueryData()
        {
            RootObject hoodCollection = ReadData("../../../data.json");

            QueryAll(hoodCollection);

            Console.WriteLine("Press enter to see only named neighbourhoods.");
            Console.ReadLine();
            QueryNamed(hoodCollection);

            Console.WriteLine("Press enter to see all UNIQUE, named methods.");
            Console.ReadLine();
            QueryNoDupes(hoodCollection);
        }

        /// <summary>
        /// Writes all neighborhoods in database to console.
        /// </summary>
        /// <param name="collection">collection of neighborhood features</param>
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

        /// <summary>
        /// Writes all named neighbourhoods to Console. filters empty names out.
        /// </summary>
        /// <param name="collection">Collection of neighborhood features</param>
        static void QueryNamed(RootObject collection)
        {
            Console.Clear();
            Console.WriteLine("These are all the named new york neighborhoods: \n(press Enter)");
            Console.ReadLine();

            // DEAD CODE: 
            /*var query = from item in collection.features
                        where item.properties.neighborhood != ""
                        select item.properties.neighborhood;*/

            // REWRITE USING METHOD SYNTAX
            var query = collection.features
                .Where(f => f.properties.neighborhood != "")
                .Select(f => f.properties.neighborhood);

            int count = 0;
            foreach (string hood in query)
            {
                count++;
                Console.WriteLine(hood);
            }
            Console.WriteLine($"Total Neighborhoods: {count}");
        }

        /// <summary>
        /// Writes all named neighborhoods to Console, with no duplicates.
        /// </summary>
        /// <param name="collection">Collection of neighbourhood features</param>
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
