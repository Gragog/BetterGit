using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Gramini.Manager
{
    static class Input
    {
        static public string GetInput(string pattern, string requestMessage = "enter input ", string errorMessage = "invalid input")
        {
            Regex item = new Regex(pattern);
            bool validInput = false;
            string input = "";

            while (!validInput)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(requestMessage);

                Console.ForegroundColor = ConsoleColor.Magenta;
                input = Console.ReadLine();

                if (item.IsMatch(input))
                {
                    validInput = true;
                    break;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(errorMessage);
            }

            return input;
        }

        static public int GetInputInt(string pattern = @"^[-]?\d+$", string b = "enter input", string c = "invalid input")
        {
            return Convert.ToInt32(GetInput(pattern, b, c));
        }

        static public dynamic GetInputInt(int min, int max, bool allowEmpty, string b = "enter input", string c = "invalid input")
        {
            int number = 0;
            string numberInput = "";

            while (true)
            {
                numberInput = GetInput(@"^([-]?\d+)?$", b, c);

                if (numberInput == "") return null;

                number = Convert.ToInt32(number);
                if (number > min && number < max)
                {
                    break;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Input is out of range!");
            }


            return number;
        }

        static public string GetInputPath(string b = "enter path ", string c = "invalid input", string pattern = @"^.*$")
        {
            bool validInput = false;
            string input = "";

            while (!validInput)
            {
                input = GetInput(pattern, b, c);

                if (Directory.Exists(input))
                {
                    break;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(input + " does not exist!");
            }

            return input;
        }

        internal static string GetInputFile(string b = "enter path to file", string c = "invalid input", string pattern = @"^.*$")
        {

            bool validInput = false;
            string input = "";

            while (!validInput)
            {
                input = GetInput(pattern, b, c).Replace("\"", "");

                if (File.Exists(input))
                {
                    validInput = true;
                    break;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(input + " does not exist!");
            }

            return input;
        }
    }
}
