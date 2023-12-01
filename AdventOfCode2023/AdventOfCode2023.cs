using AdventOfCode2023.Services;
using System;

namespace AdventOfCode2023
{
    public class AdventOfCode2023
    {
        private static void Main(string[] args)
        {
            MenuPrinterService.PrintStartUpHeader();
            MenuPrinterService.PrintUsage();

            while(true)
            {
                var userInput = Console.ReadLine();
                if (string.IsNullOrEmpty(userInput))
                {
                    continue;
                }
                
                userInput = userInput.Trim().ToUpper();
                if (userInput == "Q")
                {
                    break;
                }
                else if(IsValidInput(userInput))
                {
                    var selectedSolution = SolutionSelectorService.GetRequestedSolution(userInput);
                    if (selectedSolution == null)
                    {
                        MenuPrinterService.SolutionDoesNotExist(userInput);
                    }
                    else
                    {
                        selectedSolution.Solve();
                    }
                }
                else
                {
                    MenuPrinterService.PrintUnknownSelectedSolution(userInput);
                }
            }

            MenuPrinterService.PrintGoodBye();
        }

        private static bool IsValidInput(string userInput)
        {
            int value = 0;
            Int32.TryParse(userInput, out value);
            return value >= 1 && value <= 25;
        }
    }
}