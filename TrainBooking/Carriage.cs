using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TrainBooking
{
    class Carriage
    {
        //  Stations
        private string[] station = new string[]
        {
            "Beggin Terminal",
            "Suddean Halt",
            "Multhy Pass",
            "Conn Junction",
            "Endline Station"
        };

        //  Station departure times
        private string[] departTime = new string[]
        {
            "08:00", "08:45", "09:30", "10:15",
            "11:00", "11:45", "12:30", "13;15",
            "14:00", "14:45", "15:30", "16:15",
            "17:00", "17:45", "18:30", "19:15"
        };

        //  Class variables
        private string carName;
        private string carClass;
        private string carLetter;
        private int capacity;
        private string seatNumber;

        //  User variables
        private int userInt;
        private string userString;

        //  Instaniate a List containing the Seats
        List<Seat> seatList = new List<Seat>();
        Seat bSeat;

        //  Default empty Constructor
        public Carriage() { }

        //  Carriage Constructor that requires a name, class, letter and capacity
        public Carriage(string carName, string carClass, string carLetter, int capacity)
        {
            //  The 'this.' variables store the variables entered into the constructor
            //  when it is instantiated 
            this.carName = carName;
            this.carClass = carClass;
            this.carLetter = carLetter;
            this.capacity = capacity;
        }

        public void DisplayMenu()
        {
            //  Clear the console
            Console.Clear();

            Utility.MultiWriteLine(
                carName + " Car : " + carLetter,
                "Travel Class : " + carClass,
                "Number of Seats available : " + capacity,
                "Amount of Baggage space left : " + Baggage.Instance.Capacity,
                "=============================================================",
                "To choose an option type the required number then press Enter",
                "=============================================================",
                "[1] Book a Seat",
                "[2] List all booked Seats",
                "[3] Search for your Seat or Seats",
                "[4] Write to text file",
                "[5] Return to Main Menu",
                "[0] Exit!");

            try
            {
                userInt = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                Utility.AnyKeyReturn("Invalid option! Please try again.");
                DisplayMenu();
            }

            switch (userInt)
            {
                case 1:
                    BookSeat();
                    break;
                case 2:
                    DisplaySeats();
                    break;
                case 3:
                    SeatSearch();
                    break;
                case 4:
                    WriteToFile();
                    break;
                case 5:
                    Menu.Instance.MainMenu();
                    break;
                case 0:
                    Environment.Exit(0);
                    break;
                default:
                    Utility.AnyKeyReturn("Invalid option! Please try again.");
                    DisplayMenu();
                    break;
            }
        }


        private void BookSeat()
        {
            Console.Clear();
            Utility.MultiWriteLine(
                "Single or Group?",
                "[1] Single",
                "[2] Group",
                "[3] Return back to menu");

            try
            {
                userInt = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                Utility.AnyKeyReturn("Invalid option! Please try again.");
                BookSeat();
            }

            switch (userInt)
            {
                case 1:
                    Console.Clear();
                    Single();
                    break;
                case 2:
                    Console.Clear();
                    Utility.MultiWriteLine(
                        "How many Seats would you like to book?",
                        "(Maximum amount you can book is 8!)",
                        "");
                    try
                    {
                        userInt = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (Exception)
                    {
                        Utility.AnyKeyReturn("Invalid Input! Please try again");
                        BookSeat();
                    }
                    //  If the group exceeds 8...
                    if (userInt > 8)
                    {
                        Utility.AnyKeyReturn("Group is too big! Please try again");
                        //  Returns back to seat selection
                        BookSeat();
                    }
                    else
                        Group(userInt);
                    break;
                case 3:
                    //  Returns to DisplayMenu
                    DisplayMenu();
                    break;
                default:
                    Utility.AnyKeyReturn("Invalid option! Please try again.");
                    BookSeat();
                    break;
            }
        }

        private void Single()
        {
            Console.Clear();

            //  Only one Seat is being booked
            int singleSeat = 1;

            //  If the capacity is not yet reached...
            if (singleSeat < capacity)
            {
                StationAndTime(singleSeat);
            }
            //  Otherwise
            else
                Utility.AnyKeyReturn("No more Seats remaining!");
        }

        /// <summary>
        /// Adds Seats to the seatList based on the size of the group
        /// </summary>
        /// <param name="groupSize">the number of seats that will be added to seatList</param>
        /// <returns></returns>
        private int Group(int groupSize)
        {
            Console.Clear();
            if (groupSize <= capacity)
            {
                StationAndTime(groupSize);

                Utility.AnyKeyReturn("Your Seat has been booked!");
                DisplayMenu();
            }
            else if (groupSize > capacity)
            {
                Utility.AnyKeyReturn("No more Seats remaining!");
            }
            DisplayMenu();

            return groupSize;
        }

        private void DisplaySeats()
        {
            Console.Clear();

            //  Searches through all the seats within the seatList
            foreach (Seat seat in seatList)
            {
                //  Dislpays the seats information
                seat.Print();
            }
            //  Displays "Press Any Key to return"
            Utility.AnyKeyReturn("");
            DisplayMenu();
        }

        private void SeatSearch()
        {
            Console.Clear();

            //  If there are seats in the list
            if (seatList.Count > 0)
            {
                //  Request user's name
                Console.WriteLine("Please enter your name to find your seat: ");
                userString = Console.ReadLine();

                double catchInt;
                if (double.TryParse(userString, out catchInt))
                {
                    Utility.AnyKeyReturn("Invalid Input! Please try again.");
                    Console.Clear();
                    SeatSearch();
                }
                else if (userString == "")
                {
                    Utility.AnyKeyReturn("Invalid Input! Please try again.");
                    Console.Clear();
                    SeatSearch();
                }

                //  If a Seat in the seatList matches the user's name...
                foreach (Seat seat in seatList)
                {
                    if (seat.Passenger == userString)
                    {
                        //  Display the User's seat information
                        seat.Print();
                    }
                }
                Utility.AnyKeyReturn("");
                DisplayMenu();
            }
            //  If there isn't any Seats
            else if (seatList.Count == 0)
            {
                Utility.AnyKeyReturn("Currently No Seats have been Booked!");
                DisplayMenu();
            }
        }

        private void WriteToFile()
        {
            string seatDetails = "";
            StreamWriter writer;

            //  Ask user if they would like to save file...
            Utility.MultiWriteLine(
                "WARNING! This will overwrite an existing file! Would you like to continue?",
                "[1] Yes",
                "[2] No");
            try
            {
                userInt = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                Utility.AnyKeyReturn("Invalid Input! Please try again.");
                BookSeat();
            }

            //  If the user said Yes
            if (userInt == 1)
            {
                //  If the carriage class is First
                //  Create a new text file
                if (carClass == "First")
                {
                    writer = new StreamWriter(carName + "" + carLetter + "" + ".txt");

                    //  For every Seat in the list, write the details to the text file
                    foreach (Seat seat in seatList)
                    {
                        //  Convert information to string
                        seatDetails = seat.TravelClass + " " + seat.SeatClass + "" + seat.SeatIndex + " " + seat.Passenger + " " + seat.DepartStation + " " + seat.DepartTime + Environment.NewLine;
                        //  Write information to file
                        writer.Write(seatDetails);
                    }

                    Utility.AnyKeyReturn("Seating Information has been saved!");
                    writer.Close();
                    DisplayMenu();
                }

                //  Otherwise if the carriage class is Third
                else if (carClass == "Third")
                {
                    writer = new StreamWriter(carName + "" + carLetter + "" + ".txt");

                    //  For every Seat in the list, write the details to the text file
                    foreach (Seat seat in seatList)
                    {
                        //  Convert information to string
                        seatDetails = seat.TravelClass + " " + seat.SeatClass + "" + seat.SeatIndex + " " + seat.Passenger + " " + seat.DepartStation + " " + seat.DepartTime + Environment.NewLine;
                        //  Write information to file
                        writer.Write(seatDetails);
                    }

                    Utility.AnyKeyReturn("Seating Information has been saved!");
                    writer.Close();
                    DisplayMenu();
                }
            }
            //  Otherwise if the user said No
            else if (userInt == 2)
            {
                //  Return back to the menu
                DisplayMenu();
            }
        }

        private Seat SeatInfo(string name, int tempNum, string station, string time)
        {
            //  Turn back into a string keeping double digit format
            seatNumber = tempNum.ToString("D2");

            //  Instantiate a seat
            bSeat = new Seat();

            //  Assign seat properties values
            bSeat.TravelClass = carClass;
            bSeat.SeatClass = carLetter;
            bSeat.SeatIndex = seatNumber;
            bSeat.Passenger = name;
            bSeat.DepartStation = station;
            bSeat.DepartTime = time;

            return bSeat;
        }

        private void StationAndTime(int amountToBook)
        {
            Console.Clear();

            //  convert seatNumber into an integer for incrementing
            int tempNum = Convert.ToInt32(seatNumber);
            string depTime;
            string depStation;

            //  Request User's name
            Console.WriteLine("Enter your Name: ");
            userString = Console.ReadLine();

            //  Check to see if anything other than a name has been entered,
            //  if there is display Invalid Input message
            double catchInt;
            if (double.TryParse(userString, out catchInt))
            {
                Utility.AnyKeyReturn("Invalid Input! Please try again.");
                Console.Clear();
                StationAndTime(amountToBook);
            }
            else if (userString == "")
            {
                Utility.AnyKeyReturn("Invalid Input! Please try again.");
                Console.Clear();
                StationAndTime(amountToBook);
            }

            //  Ask user for station choice
            Utility.MultiWriteLine(
                "Choose a station",
                "[1] Beggin Terminal",
                "[2] Suddean Halt",
                "[3] Multhy Pass",
                "[4] Conn Junction",
                "[5] Endline Station",
                "[6] Return");
            try
            {
                userInt = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                Utility.AnyKeyReturn("Invalid option! Please try again.");
                StationAndTime(amountToBook);
            }
            //  Display departure times based on station choice
            if (userInt == 1)
            {
                Utility.MultiWriteLine(
                "Choose a departure time",
                "[1] " + departTime[0],
                "[2] " + departTime[8],
                "[3] Change Station Choice");
                try
                {
                    userInt = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    Utility.AnyKeyReturn("Invalid option! Please try again.");
                    StationAndTime(amountToBook);
                }

                switch (userInt)
                {
                    case 1:
                        depStation = station[0];
                        depTime = departTime[0];

                        AddBaggage();
                        AddSeat(amountToBook, tempNum, depStation, depTime);
                        break;
                    case 2:
                        depStation = station[0];
                        depTime = departTime[8];

                        AddBaggage();
                        AddSeat(amountToBook, tempNum, depStation, depTime);
                        break;
                    case 3:
                        StationAndTime(amountToBook);
                        break;
                }
            }
            else if (userInt == 2)
            {
                Utility.MultiWriteLine(
                "Choose a departure time",
                "[1] " + departTime[1],
                "[2] " + departTime[7],
                "[3] " + departTime[9],
                "[4] " + departTime[15],
                "[5] Change Station Choice");

                try
                {
                    userInt = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    Utility.AnyKeyReturn("Invalid option! Please try again.");
                    StationAndTime(amountToBook);
                }

                switch (userInt)
                {
                    case 1:
                        depStation = station[1];
                        depTime = departTime[1];

                        AddBaggage();
                        AddSeat(amountToBook, tempNum, depStation, depTime);
                        break;
                    case 2:
                        depStation = station[1];
                        depTime = departTime[7];

                        AddBaggage();
                        AddSeat(amountToBook, tempNum, depStation, depTime);
                        break;
                    case 3:
                        depStation = station[1];
                        depTime = departTime[9];

                        AddBaggage();
                        AddSeat(amountToBook, tempNum, depStation, depTime);
                        break;
                    case 4:
                        depStation = station[1];
                        depTime = departTime[15];

                        AddBaggage();
                        AddSeat(amountToBook, tempNum, depStation, depTime);
                        break;
                    case 5:
                        StationAndTime(amountToBook);
                        break;
                }
            }
            else if (userInt == 3)
            {
                Utility.MultiWriteLine(
                "Choose a departure time",
                "[1] " + departTime[2],
                "[2] " + departTime[6],
                "[3] " + departTime[10],
                "[4] " + departTime[14],
                "[5] Change Station Choice");

                try
                {
                    userInt = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    Utility.AnyKeyReturn("Invalid option! Please try again.");
                    StationAndTime(amountToBook);
                }

                switch (userInt)
                {
                    case 1:
                        depStation = station[2];
                        depTime = departTime[2];

                        AddBaggage();
                        AddSeat(amountToBook, tempNum, depStation, depTime);
                        break;
                    case 2:
                        depStation = station[2];
                        depTime = departTime[6];

                        AddBaggage();
                        AddSeat(amountToBook, tempNum, depStation, depTime);
                        break;
                    case 3:
                        depStation = station[2];
                        depTime = departTime[10];

                        AddBaggage();
                        AddSeat(amountToBook, tempNum, depStation, depTime);
                        break;
                    case 4:
                        depStation = station[2];
                        depTime = departTime[14];

                        AddBaggage();
                        AddSeat(amountToBook, tempNum, depStation, depTime);
                        break;
                    case 5:
                        StationAndTime(amountToBook);
                        break;
                }
            }
            else if (userInt == 4)
            {
                Utility.MultiWriteLine(
                "Choose a departure time",
                "[1] " + departTime[3],
                "[2] " + departTime[5],
                "[3] " + departTime[11],
                "[4] " + departTime[13],
                "[5] Change Station Choice");

                try
                {
                    userInt = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    Utility.AnyKeyReturn("Invalid option! Please try again.");
                    StationAndTime(amountToBook);
                }

                switch (userInt)
                {
                    case 1:
                        depStation = station[3];
                        depTime = departTime[3];

                        AddBaggage();
                        AddSeat(amountToBook, tempNum, depStation, depTime);
                        break;
                    case 2:
                        depStation = station[3];
                        depTime = departTime[5];

                        AddBaggage();
                        AddSeat(amountToBook, tempNum, depStation, depTime);
                        break;
                    case 3:
                        depStation = station[3];
                        depTime = departTime[11];

                        AddBaggage();
                        AddSeat(amountToBook, tempNum, depStation, depTime);
                        break;
                    case 4:
                        depStation = station[3];
                        depTime = departTime[13];

                        AddBaggage();
                        AddSeat(amountToBook, tempNum, depStation, depTime);
                        break;
                    case 5:
                        StationAndTime(amountToBook);
                        break;
                }
            }
            if (userInt == 5)
            {
                Utility.MultiWriteLine(
                "Choose a departure time",
                "[1] " + departTime[4],
                "[2] " + departTime[12],
                "[3] Change Station Choice");

                try
                {
                    userInt = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    Utility.AnyKeyReturn("Invalid option! Please try again.");
                    StationAndTime(amountToBook);
                }

                switch (userInt)
                {
                    case 1:
                        depStation = station[4];
                        depTime = departTime[4];

                        AddBaggage();
                        AddSeat(amountToBook, tempNum, depStation, depTime);
                        break;
                    case 2:
                        depStation = station[4];
                        depTime = departTime[12];

                        AddBaggage();
                        AddSeat(amountToBook, tempNum, depStation, depTime);

                        break;
                    case 3:
                        StationAndTime(amountToBook);
                        break;
                }
            }
            else if (userInt == 6)
            {
                BookSeat();
            }
        }

        private void AddBaggage()
        {
            //  Ask user if they will be taking any luggage
            Utility.MultiWriteLine(
                "Will there be any luggage?",
                "[1] Yes",
                "[2] No");
            try
            {
                userInt = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                Utility.AnyKeyReturn("Invalid option! Please try again.");
                AddBaggage();
            }
            //  if yes ask how much
            if (userInt == 1)
            {
                Console.WriteLine("How many items?");
                try
                {
                    userInt = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    Utility.AnyKeyReturn("Invalid input! Please try again.");
                }
                //  Add baggage
                Baggage.Instance.CalcBagCapacity(userInt);
            }
        }

        private void AddSeat(int amount, int num, string station, string time)
        {
            for (int i = 0; i < amount; i++)
            {
                num++;
                seatList.Add(SeatInfo(userString, num, station, time));
                capacity--;
            }
            Utility.AnyKeyReturn("Your Seat has been Booked!");
            DisplayMenu();
        }
    }
}
