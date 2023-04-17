using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nagelmackers
{
    internal class ResultsList
    {
        private List<ResultTab> Results = new List<ResultTab>();
        private string departure_station_name;
        private string destination_station_name;

        public ResultsList(string departure_station_name, string destination_station_name, int NumerOfTabs,int DepartureTime, int ArrivalTime, int IndexOfCarrier, int TravelTime, float TicketPrice)
        {
            this.departure_station_name = departure_station_name;
            this.destination_station_name = destination_station_name;
            for (int i = 0; i < NumerOfTabs; i++)
            {
                Results.Add(new ResultTab(DepartureTime, ArrivalTime, IndexOfCarrier, TravelTime, TicketPrice));
            }
        }
    }
}
