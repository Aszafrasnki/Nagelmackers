using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nagelmackers
{
    internal class StationList
    {
        private List<Station> ListOfStations = new List<Station>();

        /// <summary>
        /// Constructor for StationList
        /// </summary>
        /// <param name="NumberOfStations"></param>
        /// <param name="StationName"></param>
        /// <param name="CordX">Coordinates of the station (x)</param>
        /// <param name="CordY">Coordinates of the station (y)</param>
        /// <param name="WestEast">wheter the station is a west or east variant (-1 west | 0 main | 1 east)</param>
        public StationList(int NumberOfStations, string StationName, int CordX, int CordY, int WestEast)
        {
            for (int i = 0; i < NumberOfStations; i++)
            {
                ListOfStations.Add(new Station(StationName, CordX, CordY, WestEast));
            }
        }
    }
}
