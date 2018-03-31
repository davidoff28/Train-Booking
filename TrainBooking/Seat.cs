using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainBooking
{
    class Seat
    {
        public string TravelClass { get; set; }
        public string SeatClass { get; set; }
        public string SeatIndex { get; set; }
        public string Passenger { get; set; }
        public string DepartStation { get; set; }
        public string DepartTime { get; set; }

        public Seat() { }

        public void Print()
        {
            //Console.WriteLine("{0}\t{1}{2}\t{3}\t{5}\t{6}", TravelClass, SeatClass, SeatIndex, Passenger, DepartStation, DepartTime);
            Console.WriteLine(TravelClass + "\t " + SeatClass + "" + SeatIndex + "\t " + Passenger + "\t" + DepartStation + "\t" + DepartTime);
        }
    }
}
