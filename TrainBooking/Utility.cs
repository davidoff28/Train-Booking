using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainBooking
{
    //  This is a Utility Class that contains helper methods that will be
    //  re-used throughout the program
    class Utility
    {
        /// <summary>
        /// Outputs a line to the console window along
        /// with "Press any key to return" line and a ReadKey response.
        /// </summary>
        /// <param name="text">The string that will be displayed on the console</param>
        /// <returns></returns>
        public static string AnyKeyReturn(string text)
        {
            Console.WriteLine(text);
            Console.WriteLine();
            Console.WriteLine("Press any key to return");
            Console.ReadKey();

            return text;
        }

        /// <summary>
        /// Outputs multiple lines to the console window depending on the number of
        /// strings entered
        /// </summary>
        /// <param name="textLine">The line that will be displayed on the console</param>
        /// <returns></returns>
        public static string MultiWriteLine(params string[] textLine)
        {
            string print = "";

            foreach (string text in textLine)
            {
                print = text;
                Console.WriteLine(print);
            }

            return print;
        }
    }
}
