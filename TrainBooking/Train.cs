using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainBooking
{
    class Train
    {
        // The carriages that the train will have
        Carriage compartmentCarA = new Carriage("Compartment", "First", "A", 32);
        Carriage compartmentCarB = new Carriage("Compartment", "Third", "B", 32);
        Carriage standardCarC = new Carriage("Standard", "Third", "C", 50);
        Carriage standardCarD = new Carriage("Standard", "Third", "D", 50);

        //  User Input for choices
        private int userInt;

        public void CarChoice()
        {
            Console.Clear();
            Utility.MultiWriteLine(
                "Compartment or Standard Carriage?",
                "[1] Compartment (Choice of First or Third Class)",
                "[2] Standard (Third Class Only)",
                "[3] Return");

            try
            {
                userInt = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                Utility.AnyKeyReturn("Invalid Input! Please try again.");
                CarChoice();
            }

            switch (userInt)
            {
                case 1:
                    TravelChoice();
                    break;
                case 2:
                    StandardChoice();
                    break;
                case 3:
                    return;
                default:
                    Utility.AnyKeyReturn("Invalid Input! Please try again.");
                    CarChoice();
                    break;
            }
        }

        private void TravelChoice()
        {
            Console.Clear();
            Utility.MultiWriteLine(
                "Compartment Carriage",
                "First or Third Class?",
                "[1] First",
                "[2] Third",
                "[3] Return");

            try
            {
                userInt = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                Utility.AnyKeyReturn("Invalid Input! Please try again");
            }

            if (userInt == 1)
            {
                compartmentCarA.DisplayMenu();
            }
            else if (userInt == 2)
            {
                compartmentCarB.DisplayMenu();
            }
            else if (userInt == 3)
            {
                CarChoice();
            }
            else
            {
                Utility.AnyKeyReturn("Invalid Input! Please try again");
            }
        }

        private void StandardChoice()
        {
            Console.Clear();
            Utility.MultiWriteLine(
                "Standard C carriage or Standard D carriage?",
                "[1] C",
                "[2] D",
                "[3] Return");
            try
            {
                userInt = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                Utility.AnyKeyReturn("Invalid Input! Please try again");
                StandardChoice();
            }

            if (userInt == 1)
            {
                standardCarC.DisplayMenu();
            }
            else if (userInt == 2)
            {
                standardCarD.DisplayMenu();
            }
            else if (userInt == 3)
            {
                TravelChoice();
            }
            else
            {
                Utility.AnyKeyReturn("Invalid Input! Please try again");
                StandardChoice();
            }
        }
    }
}
