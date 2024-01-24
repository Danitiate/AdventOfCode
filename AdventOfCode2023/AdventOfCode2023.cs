using AdventOfCode2023.Models;
using AdventOfCode2023.Services;
using System;

namespace AdventOfCode2023
{
    public class AdventOfCode2023
    {
        private static string? UserInput { get; set; }

        private static void Main(string[] args)
        {
            var state = State.MAIN_MENU;
            MenuPrinterService.PrintStartUpHeader();

            while(state != State.EXIT)
            {
                if (state == State.MAIN_MENU)
                {
                    MenuPrinterService.PrintUsage();
                    state = MainMenu();
                }
                else if(state == State.SOLUTION) 
                {
                    state = SolutionMenu();
                }
            }

            MenuPrinterService.PrintGoodBye();
        }

        private static State MainMenu()
        {
            UserInput = Console.ReadLine();
            if (string.IsNullOrEmpty(UserInput))
            {
                return State.MAIN_MENU;
            }

            UserInput = UserInput.Trim().ToUpper();
            if (UserInput == "Q")
            {
                return State.EXIT;
            }
            
            if (IsValidInput())
            {
                return State.SOLUTION;
            }
            else
            {
                MenuPrinterService.PrintUnknownSelectedSolution(UserInput);
            }

            return State.MAIN_MENU;
        }

        private static State SolutionMenu()
        {
            var selectedSolutions = SolutionSelectorService.GetRequestedSolutions(UserInput!);
            if (selectedSolutions.Count == 0)
            {
                MenuPrinterService.SolutionDoesNotExist(UserInput!);
            }
            else
            {
                if (selectedSolutions.Count > 1)
                {
                    MenuPrinterService.MultipleParts(selectedSolutions.Count);
                    while(true)
                    {
                        UserInput = Console.ReadLine();
                        UserInput = UserInput!.Trim().ToUpper();
                        if (UserInput == "Q")
                        {
                            return State.EXIT;
                        }

                        if (IsValidInput(selectedSolutions.Count))
                        {
                            var selectedSolution = Int32.Parse(UserInput);
                            selectedSolutions[selectedSolution - 1].Solve();
                            break;
                        }
                        else
                        {
                            MenuPrinterService.PrintUnknownSelectedSolution(UserInput);
                        }
                    }
                }
                else
                {
                    selectedSolutions[0].Solve();
                }
            }

            return State.MAIN_MENU;
        }

        private static bool IsValidInput(int max = 25)
        {
            int value = 0;
            Int32.TryParse(UserInput, out value);
            return value >= 1 && value <= max;
        }
    }
}