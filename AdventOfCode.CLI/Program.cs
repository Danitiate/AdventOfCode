using AdventOfCode.Core.Models;
using AdventOfCode.Core.Services;
using AdventOfCode2023.Solutions;

namespace AdventOfCode.CLI
{
    public class AdventOfCodeCLI
    {
        private static string? UserInput { get; set; }

        private static void Main(string[] args)
        {
            var state = MenuState.MAIN_MENU;
            MenuPrinterService.PrintStartUpHeader();

            while (state != MenuState.EXIT)
            {
                if (state == MenuState.MAIN_MENU)
                {
                    MenuPrinterService.PrintUsage();
                    state = MainMenu();
                }
                else if (state == MenuState.SOLUTION)
                {
                    state = SolutionMenu();
                }
            }

            MenuPrinterService.PrintGoodBye();
        }

        private static MenuState MainMenu()
        {
            UserInput = Console.ReadLine();
            if (string.IsNullOrEmpty(UserInput))
            {
                return MenuState.MAIN_MENU;
            }

            UserInput = UserInput.Trim().ToUpper();
            if (UserInput == "Q")
            {
                return MenuState.EXIT;
            }

            if (IsValidInput())
            {
                return MenuState.SOLUTION;
            }
            else
            {
                MenuPrinterService.PrintUnknownSelectedSolution(UserInput);
            }

            return MenuState.MAIN_MENU;
        }

        private static MenuState SolutionMenu()
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
                    while (true)
                    {
                        UserInput = Console.ReadLine();
                        UserInput = UserInput!.Trim().ToUpper();
                        if (UserInput == "Q")
                        {
                            return MenuState.EXIT;
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

            return MenuState.MAIN_MENU;
        }

        private static bool IsValidInput(int max = 25)
        {
            int value = 0;
            Int32.TryParse(UserInput, out value);
            return value >= 1 && value <= max;
        }
    }
}