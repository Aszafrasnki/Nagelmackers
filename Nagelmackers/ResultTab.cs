using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nagelmackers
{
    internal class ResultTab
    {
        private int departure_time;
        private int arrival_time;
        private int index_of_carrier;
        private int travel_time;
        private float ticket_price;

        /// <summary>
        /// Constructor for Result Tab
        /// </summary>
        /// <param name="departure_time">Time of departure from the starting station</param>
        /// <param name="arrival_time">Time of arrival at destination station</param>
        /// <param name="index_of_carrier">index of the carrier</param>
        /// <param name="travel_time">time to travel from departure station to destination station</param>
        /// <param name="ticket_price">the price of the ticket</param>
        public ResultTab(int departure_time, int arrival_time, int index_of_carrier, int travel_time, float ticket_price)
        {
            this.departure_time = departure_time;
            this.arrival_time = arrival_time;
            this.index_of_carrier = index_of_carrier;
            this.travel_time = travel_time;
            this.ticket_price = ticket_price;
        }
        //getter and setter methods
        public int Departuretime { get => departure_time; set => departure_time = value; }
        public int Arrivaltime { get => arrival_time; set => arrival_time = value; }
        public int IndexOfCarrier { get => index_of_carrier; set => index_of_carrier = value; }
        public int Traveltime { get => travel_time; set => travel_time = value; }
        public float Ticketprice { get => ticket_price; set => ticket_price = value; }
    }
}
