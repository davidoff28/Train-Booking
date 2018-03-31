using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TrainBooking
{
    class Menu
    {
        //  Setting up MenuManager as a Singleton as there only needs to be one instance of the class
        private static Menu instance;

        //  If there is no MenuManager this will instaniate one
        public static Menu Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Menu();
                }
                return instance;
            }
        }

        //  User Input for choices
        private int userInt;

        //  Instaniate train
        Train train = new Train();

        public void MainMenu()
        {
            Console.Clear();
            Utility.MultiWriteLine(
                "Main Menu",
                "Welcome to Old Time Rail",
                "To choose an option, type in the number of choice then press Enter",
                "[1] Start",
                "[2] View Station Time Table",
                "[0] Exit");

            try
            {
                userInt = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                Utility.AnyKeyReturn("Invalid option! Please try again.");
                MainMenu();
            }

            switch (userInt)
            {
                case 1:
                    train.CarChoice();
                    break;
                case 2:
                    Console.Clear();
                    TimeTable();
                    break;
                case 0:
                    Environment.Exit(0);
                    break;
                default:
                    Utility.AnyKeyReturn("Invalid option! Please try again.");
                    return;
            }
        }

        private void TimeTable()
        {
            //  Open TimeTable file and read it
            FileStream file = File.OpenRead("TimeTable.txt");
            StreamReader reader = new StreamReader(file);

            //  Store text into table variable
            string table;
            table = reader.ReadToEnd();

            //  Output Timetable to console
            Console.WriteLine(table);

            Utility.AnyKeyReturn("");
        }
    }
}
