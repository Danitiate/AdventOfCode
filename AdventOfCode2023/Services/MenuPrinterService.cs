using System;

namespace AdventOfCode2023.Services
{
    public class MenuPrinterService
    {
        public static void PrintStartUpHeader()
        {
            Console.WriteLine(@"
============================================================
          A D V E N T     O F     C O D E     2023
============================================================");
            PrintBreak();
        }

        public static void PrintUsage()
        {
            Console.WriteLine(@"
Use your keyboard to select the solution of a given day.
Valid input: 1 - 25
You can exit at any time by pressing Q and then ENTER
____________________________________________________________");
            PrintBreak();
        }

        public static void PrintUnknownSelectedSolution(string userInput)
        {
            Console.WriteLine($"I could not understand what you meant by: '{userInput}'");
            PrintBreak();
        }

        public static void SolutionDoesNotExist(string userInput)
        {
            Console.WriteLine($"The solution for '{userInput}' does not exist yet");
            PrintBreak();
        }

        public static void PrintSolution(string output)
        {
            Console.WriteLine($"The solution is: \n{output}");
        }

        public static void PrintGoodBye()
        {
            Console.WriteLine("Thank you, and good bye! :)");
        }

        private static void PrintBreak()
        {
            Console.WriteLine("\n");
        }
    }
}
