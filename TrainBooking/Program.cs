using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainBooking
{
    class Program
    {
        //  Keep the program running whilst it is active
        static void Main(string[] args)
        {
            bool isActive = true;

            do
            {
                Menu.Instance.MainMenu();
            }
            while (isActive);
        }
    }
}
