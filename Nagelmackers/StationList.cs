using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using Nagelmackers;

namespace Nagelmackers
{
    internal class StationList
    {
        public class StationClassMap : ClassMap<Station>
        {
            public StationClassMap()
            {
                Map(m => m.StationName).Name("StationName");
                Map(m => m.CordX).Name("CordX");
                Map(m => m.CordY).Name("CordY");
                Map(m => m.WestEast).Name("WestEast");
            }
        }

        private List<Station> ListOfStations = new List<Station>();

        /// <summary>
        /// Constructor for StationList
        /// </summary>
        /// <param name="manual">if set to true you can add entries to the list manualy | if set to false (default) it will create a list based on the Lista_Miast.csv file</param>
        /// <param name="StationName"></param>
        /// <param name="CordX">Coordinates of the station (x)</param>
        /// <param name="CordY">Coordinates of the station (y)</param>
        /// <param name="WestEast">wheter the station is a west or east variant (-1 west | 0 main | 1 east)</param>
        public StationList(bool manual = false, string StationName = "", int CordX = 0, int CordY = 0, int WestEast = 0)
        {
            if (manual)
            {
                ListOfStations.Add(new Station(StationName, CordX, CordY, WestEast));
            }
            else
            {
                using (var reader = new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Lista_Miast.csv"), Encoding.UTF8))
                {
                    var CSVconfig = new CsvConfiguration(CultureInfo.CurrentCulture) {
                        Delimiter = ";",
                        Encoding = Encoding.UTF8
                    };
                using (var csvReader = new CsvReader(reader, CSVconfig))
                    {
                        csvReader.Context.RegisterClassMap<StationClassMap>();
                        ListOfStations = csvReader.GetRecords<Station>().ToList();
                    }
                }
            }
        }

        /// <summary>
        /// Adds a statuion to the list
        /// </summary>
        /// <param name="StationName"></param>
        /// <param name="CordX"></param>
        /// <param name="CordY"></param>
        /// <param name="WestEast"></param>
        public void AddStation(string StationName, int CordX, int CordY, int WestEast)
        {
            ListOfStations.Add(new Station(StationName, CordX, CordY, WestEast));
        }
        
        /// <summary>
        /// Prints out the ListOfStations to a external file in bin/Debug/ListOfStations_Reader_Output.txt
        /// </summary>
        public void ListOfStations_Reader()
        {
            FileStream logi = new FileStream("LostOfStations_Reader_Output.txt", FileMode.Create);
            TextWriter textwriter = new StreamWriter(logi, Encoding.UTF8);
            Console.SetOut(textwriter);
            for (int i = 0; i < ListOfStations.Count; i++)
            {
                Console.WriteLine(i.ToString() + ": ListOfStations -> " + ListOfStations[i].ToString());
                Console.WriteLine(i.ToString() + ": StationName -> " + ListOfStations[i].StationName);
                Console.WriteLine(i.ToString() + ": CordX -> " + ListOfStations[i].CordX.ToString());
                Console.WriteLine(i.ToString() + ": CordY -> " + ListOfStations[i].CordY.ToString());
                Console.WriteLine(i.ToString() + ": WestEast -> " + ListOfStations[i].WestEast.ToString());
            }
            textwriter.Flush();
            textwriter.Close();
            textwriter.Dispose();
        }
        /// <summary>
        /// Returns the name of the station in the list at specified index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetStationNameAtIndex(int index)
        {
            if (index - 1 > ListOfStations.Count)
            {
                return "Error Index out of bounds (ListOfStations)";
            }
            return ListOfStations[index].StationName;
        }

        public int GetIndexFromMachingString(string search)
        {
            for (int i = 0; i < ListOfStations.Count; i++)
            {
                if (ListOfStations[i].StationName == search)
                {
                    return i;
                }
            }
            return 0;
        }

        public int GetCordXAtIndex(int index)
        {
            return ListOfStations[index].CordX;
        }

        public int GetCordYAtIndex(int index)
        {
            return ListOfStations[index].CordY;
        }

        public int GetWestEastAtIndex(int index)
        {
            return ListOfStations[index].WestEast;
        }

        public int getSize()
        {
            return ListOfStations.Count;
        }
    }
}
