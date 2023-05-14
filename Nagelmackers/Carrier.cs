using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nagelmackers
{
    internal class Carrier
    {
        private int Red;
        private int Green;
        private int Blue;
        private string Name;
        private int Trigger;
        private double PricePerUnit;
        private double TimePerUnit;

        /// <summary>
        /// Constructor for Carrier
        /// </summary>
        /// <param name="Red">RGB value for red</param>
        /// <param name="Green">RGB value for green</param>
        /// <param name="Blue">RGB value for blue</param>
        /// <param name="Name">Name of the Carrier</param>
        /// <param name="Trigger">Time the carrier need beetwen departing again (in munutes)</param>
        /// <param name="PricePerUnit"></param>
        /// <param name="TimePerUnit"></param>
        public Carrier(int Red, int Green, int Blue, string Name, int Trigger, double PricePerUnit, double TimePerUnit)
        {
            this.Red = Red;
            this.Green = Green;
            this.Blue = Blue;
            this.Name = Name;
            this.Trigger = Trigger;
            this.PricePerUnit = PricePerUnit;
            this.TimePerUnit = TimePerUnit;
        }

        //Getter Setter Methods
        public int red { get => Red; set => Red = value; }
        public int green { get => Green; set => Green = value; }
        public int blue { get => Blue; set => Blue = value; }
        public string name { get => Name; set => Name = value; }
        public int trigger { get => Trigger; set => Trigger = value; }
        public double pricePerUnit { get => PricePerUnit; set => PricePerUnit = value; }
        public double timePerUnit { get => TimePerUnit; set => TimePerUnit = value; }
    }
}