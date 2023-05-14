using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Nagelmackers
{
    internal class ResultsList
    {
        private List<ResultTab> Results = new List<ResultTab>();
        private string departure_station_name;
        private int departure_cord_x;
        private int departure_cord_y;
        private int departure_west_east;
        private string destination_station_name;
        private int destination_cord_x;
        private int destination_cord_y;
        private int destination_west_east;
        private List<Carrier> carrier_list;

        int[] CarrierTrigger = new int[3] { 0, 0, 0}; //todo: write a method that determines whether a carrier's trigger has to be concidered and make the size of this array dependant on the nubmer of carriers in the CarrierList
        bool[] excludeCases = new bool[3] { false, false, false };

        /// <summary>
        /// Constructor for ResultsList
        /// </summary>
        /// <param name="DepartureStationName"></param>
        /// <param name="DepartureCordX">Expecting CordX value from StationList corresponding to a station = DepartureStationName</param>
        /// <param name="DepartureCordY">Expecting CordY value from StationList corresponding to a station = DepartureStationName</param>
        /// <param name="DepartureWestEast">Expecting WestEast value from StationList corresponding to a station = DepartureStationName</param>
        /// <param name="DestinationStationName"></param>
        /// <param name="DestinationCordX">Expecting CordX value from StationList corresponding to a station = DestinationStationName</param>
        /// <param name="DestinationCordY">Expecting CordY value from StationList corresponding to a station = DestinationStationName</param>
        /// <param name="DestinationWestEast">Expecting WestEast value from StationList corresponding to a station = DestinationStationName</param>
        /// <param name="NumerOfTabs">Number of tabs for this Result List (Recommended Value > 25) </param>
        /// <param name="DepartureTime">Time of Departure for this set of Results (in minutes)</param>
        /// <param name="CarrierList"></param>
        public ResultsList(string DepartureStationName, int DepartureCordX, int DepartureCordY, int DepartureWestEast,
                           string DestinationStationName, int DestinationCordX, int DestinationCordY, int DestinationWestEast,
                           int NumerOfTabs, int DepartureTime, List<Carrier> CarrierList)
        {
            this.departure_station_name = DepartureStationName;
            this.departure_cord_x = DepartureCordX;
            this.departure_cord_y = DepartureCordY;
            this.departure_west_east = DepartureWestEast;
            this.destination_station_name = DestinationStationName;
            this.destination_cord_x = DestinationCordX;
            this.destination_cord_y = DestinationCordY;
            this.destination_west_east = DestinationWestEast;
            this.carrier_list = CarrierList;
            InicializeTriggers();
            for (int i = 0; i < NumerOfTabs; i++)
            {
                Results.Add(new ResultTab(DepartureTime, 0, 0, 0, 0));
            }
        }

        /// <summary>
        /// Adds a new ResultTab to the ResultsList
        /// </summary>
        /// <param name="DepartureTime"></param>
        /// <param name="ArrivalTime"></param>
        /// <param name="IndexOfCarrier"></param>
        /// <param name="TravelTime"></param>
        /// <param name="TicketPrice"></param>
        public void AddResultTab(int DepartureTime, int ArrivalTime, int IndexOfCarrier, int TravelTime, float TicketPrice)
        {
            Results.Add(new ResultTab(DepartureTime, ArrivalTime, IndexOfCarrier, TravelTime, TicketPrice));
        }

        public void Scramble()
        {
            Random rnd = new Random();
            Results[0].DepartureTime += rnd.Next(20); //Searched departure time +~20min
            Results[0].IndexOfCarrier = RandomizeCarrier();
            for (int i = 1; i < Results.Count; i++)
            {
                Results[i].IndexOfCarrier = RandomizeCarrier();
                Results[i].DepartureTime = RandomizeDepartureTime();
                Results[i].TicketPrice = CalculateTicketPrice(carrier_list[Results[i].IndexOfCarrier].pricePerUnit);
                Results[i].TravelTime = CalulateTravelTime(carrier_list[Results[i].IndexOfCarrier].timePerUnit);
                Results[i].ArrivalTime = CalculateArrivalTime(Results[i].DepartureTime, Results[i].TravelTime);
            }
        }
        public int RandomizeDepartureTime()
        {
            Random rnd = new Random();
            bool trigger = true;
            int time = 0;
            while (trigger)
            {
                time += 30;
                time += rnd.Next(9);
                if (rnd.Next(100) > 50)
                { 
                    trigger = false;
                }
            }
            SubstractTriggers(time);
            return time;
        }
        public double CalculateDistance()
        {
            return Math.Sqrt(Math.Pow((departure_cord_x - destination_cord_x), 2) + Math.Pow((departure_cord_y - destination_cord_y), 2));
        }
        public double CalculateTicketPrice(double PricePerUnit)
        {
            return CalculateDistance() * PricePerUnit;
        }
        public int CalulateTravelTime(double TimePerUnit)
        {
            return Convert.ToInt32(Math.Round(CalculateDistance() * TimePerUnit));
        }
        public int CalculateArrivalTime(int Dtime, int Ttime)
        {
            return Dtime + Ttime;
        }
        //todo: write calculate methods for ticket price and travel time
        /// <summary>
        /// Generates a random IndexOfCarrier(pkp 30% pr 35% kd 25% ic 10%) and sets it value to Results at specified index
        /// </summary>
        public int RandomizeCarrier()
        {
            Random rnd = new Random();
            switch (rnd.Next(100))
            {
                case int n when (n < ExcludeAjustment()[0]): //pkp
                    SetPKPTrigger();
                    return 0;
                case int n when (n < ExcludeAjustment()[1]): //pr
                    return 1;
                case int n when (n < ExcludeAjustment()[2]): //kd
                    SetKDTrigger();
                    return 2;
                case int n when (n < 100):                   //id
                    SetICTrigger();
                    return 3;
            }
            return 0;
        }
        /// <summary>
        /// returns an array of numbers to be used in calculculating chances for Carrier index
        /// </summary>
        /// <returns>[0] chance for pkp | [1] chance for pr | [2] chance for kd </returns>
        public int[] ExcludeAjustment()
        {
            //0 = PKP chance | 1 = KD chance | 2 = IC chance
            int[] adjustment = new int[3];
            if (excludeCases[0] == true)
            {
                adjustment[0] = 0;
                adjustment[1] = adjustment[0] + 45;
                adjustment[2] = adjustment[1] + 35;
                return adjustment;
            }
            if (excludeCases[1] == true)
            {
                adjustment[0] = 35;
                adjustment[1] = adjustment[0] + 45;
                adjustment[2] = adjustment[1] + 0;
                return adjustment;
            }
            if (excludeCases[2] == true)
            {
                adjustment[0] = 35;
                adjustment[1] = adjustment[0] + 40;
                adjustment[2] = adjustment[1] + 25;
                return adjustment;
            }
            if (excludeCases[0] == true && excludeCases[1] == true)
            {
                adjustment[0] = 0;
                adjustment[1] = adjustment[0] + 80;
                adjustment[2] = adjustment[1] + 0;
                return adjustment;
            }
            if (excludeCases[0] == true && excludeCases[2] == true)
            {
                adjustment[0] = 0;
                adjustment[1] = adjustment[0] + 65;
                adjustment[2] = adjustment[1] + 35;
                return adjustment;
            }
            if (excludeCases[1] == true && excludeCases[2] == true)
            {
                adjustment[0] = 50;
                adjustment[1] = adjustment[0] + 50;
                adjustment[2] = adjustment[1] + 0;
                return adjustment;
            }
            else
            {
                adjustment[0] = 30;
                adjustment[1] = adjustment[0] + 35;
                adjustment[2] = adjustment[1] + 25;
                return adjustment;
            }
        }
        /// <summary>
        /// Checks if Trigger values for each carrier (except PR) is above 0 and if it is it sets the exclude case for that carrier
        /// </summary>
        public void SetExclusionCase()
        {
            if (CarrierTrigger[0] > 0)
            {
                excludeCases[0] = true;
            }
            if (CarrierTrigger[1] > 0)
            {
                excludeCases[1] = true;
            }
            if (CarrierTrigger[2] > 0)
            {
                excludeCases[2] = true;
            } 
        }

        /// <summary>
        /// Sets trigger values from CarrierList to the coresponding CarrierTrigger array
        /// </summary>
        public void InicializeTriggers() //todo: see the todo of int[] CarrierTrigger
        {
            SetPKPTrigger();
            SetKDTrigger();
            SetICTrigger();
        }
        /// <summary>
        /// Substracts given value from all CarrierTriggers
        /// </summary>
        /// <param name="substractor"></param>
        public void SubstractTriggers(int substractor)
        {
            for(int i = 0; i > CarrierTrigger.Count() - 1; i ++)
            {
                CarrierTrigger[i] -= substractor;
            }
        }

        //Trigger Setter Methods
        public void SetPKPTrigger()
        {
            CarrierTrigger[0] = carrier_list[0].trigger; //60
        }
        public void SetKDTrigger()
        {
            CarrierTrigger[1] = carrier_list[1].trigger; //90
        }
        public void SetICTrigger()
        {
            CarrierTrigger[2] = carrier_list[2].trigger; //360
        }
    }
}
//szukany czas +~20min losowanie między przewoźnikiem (pkp 30% pr 35% kd 25% ic 10%)
//oblicznie odległości * standardowy czas +-10 min
//co 30 min 50% na szanse ze dodanie | triggery przewoźników aka losowanie i sprawdzenie czy mozna -> róznica między teraz - ostatnie urzycie
//wcześniejszy czas => historia aka już odjechało?
//#FF013765 pkp     | +1H    | 2.25zł | 3.18min | 
//#FFCC0000 pr      | +0.5H  | 2.15zł | 6min    |
//#FFBFB700 ic      | +6H    | 4.11zł | 1.63min |
//#FF14001B kd      | +1.5H  | 2zł    | 5.25min |
