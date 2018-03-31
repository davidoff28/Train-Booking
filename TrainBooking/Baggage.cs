using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainBooking
{
    class Baggage
    {
        //  Setting up Baggage as a Singleton as there only needs to be one instance of the class
        private static Baggage instance;

        //  If there is no Baggage Class this will instaniate one
        public static Baggage Instance
        {
            get
            {
                if (instance == null)
                {
                    //  Instaniate Baggage with capacity of 40
                    instance = new Baggage(40);
                }
                return instance;
            }
        }

        private int capacity;
        public int Capacity
        {
            get
            {
                return capacity;
            }
            private set
            {
                capacity = value;
            }
        }

        public Baggage(int capacity)
        {
            //  The Baggage class' capacity is the number that is entered into the constructor
            //  when it is instaniated
            this.capacity = capacity;
        }

        public int CalcBagCapacity(int bagAmount)
        {
            if (capacity > 0 || capacity >= 1)
            {
                capacity = capacity - bagAmount;
                Console.WriteLine("Baggage Added!");
                Console.WriteLine("Baggage space remaining: {0}", capacity);
            }
            else if (capacity == 0)
            {
                Utility.AnyKeyReturn("No more Baggage space remaining");
            }
            return capacity;
        }
    }
}
