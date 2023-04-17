using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using Nagelmackers;

namespace Nagelmackers
{
    internal class Station
    {
        private string station_name;
        private int cord_x;
        private int cord_y;
        private int west_east;

        public Station() { }

        /// <summary>
        /// Constructor for Station
        /// </summary>
        /// <param name="station_name"></param>
        /// <param name="cord_x">Coordinates of the station (x)</param>
        /// <param name="cord_y">Coordinates of the station (y)</param>
        /// <param name="west_east">wheter the station is a west or east variant (-1 west | 0 main | 1 east)</param>
        public Station(string station_name, int cord_x, int cord_y, int west_east)
        {
            this.station_name = station_name;
            this.cord_x = cord_x;
            this.cord_y = cord_y;
            this.west_east = west_east;
        }
        //Getter Setter Methods
        [Name("StationName")]
        public string StationName { get => station_name; set => station_name = value; }
        [Name("CordX")]
        public int CordX { get => cord_x; set => cord_x = value; }
        [Name("CordY")]
        public int CordY { get => cord_y; set => cord_y = value; }
        [Name("WestEast")]
        public int WestEast { get => west_east; set => west_east = value; }
    }
}
