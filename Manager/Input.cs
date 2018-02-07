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
        static public string GetInput(string pattern, string requestMessage = "enter input ", string errorMessage = "invalid input", ConsoleColor requestColor = ConsoleColor.Green, ConsoleColor inputColor = ConsoleColor.Magenta)
        {
            ConsoleColor oldColor = Console.ForegroundColor;

            Regex item = new Regex(pattern);
            bool validInput = false;
            string input = "";

            while (!validInput)
            {
                Console.ForegroundColor = requestColor;
                Console.Write(requestMessage);

                Console.ForegroundColor = inputColor;
                input = Console.ReadLine();

                if (item.IsMatch(input))
                {
                    validInput = true;
                    break;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(errorMessage);
            }

            Console.ForegroundColor = oldColor;
            return input;
        }

        static public int GetInputInt(string pattern = @"^[-]?\d+$", string b = "enter input", string c = "invalid input", ConsoleColor requestColor = ConsoleColor.Green, ConsoleColor inputColor = ConsoleColor.Magenta)
        {
            return Convert.ToInt32(GetInput(pattern, b, c, requestColor, inputColor));
        }

        static public int GetInputInt(int min, int max, string b = "enter input", string c = "invalid input", ConsoleColor requestColor = ConsoleColor.Green, ConsoleColor inputColor = ConsoleColor.Magenta)
        {
            int number = 0;

            while (true)
            {
                number = Convert.ToInt32(GetInput(@"^[-]?\d+$", b, c, requestColor, inputColor));

                if (number > min && number < max)
                {
                    break;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Input is out of range!");
            }


            return number;
        }

        static public string GetInputPath(string b = "enter path ", string c = "invalid input", string pattern = @"^.*$", ConsoleColor requestColor = ConsoleColor.Green, ConsoleColor inputColor = ConsoleColor.Magenta)
        {
            bool validInput = false;
            string input = "";

            while (!validInput)
            {
                input = GetInput(pattern, b, c, requestColor, inputColor);

                if (Directory.Exists(input))
                {
                    break;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(input + " does not exist!");
            }

            return input;
        }

        internal static string GetInputFile(string b = "enter path to file", string c = "invalid input", string pattern = @"^.*$", ConsoleColor requestColor = ConsoleColor.Green, ConsoleColor inputColor = ConsoleColor.Magenta)
        {

            bool validInput = false;
            string input = "";

            while (!validInput)
            {
                input = GetInput(pattern, b, c, requestColor, inputColor).Replace("\"", "");

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
