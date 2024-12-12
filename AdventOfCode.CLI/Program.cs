using AdventOfCode.Core.Models;
using AdventOfCode.Core.Services;

namespace AdventOfCode.CLI
{
    public class AdventOfCodeCLI
    {
        private static string? UserInput { get; set; }
        private static int CurrentYear = 2024;

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
                else if (state == MenuState.CONFIGURATION)
                {
                    state = ConfigurationMenu();
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

            if (IsValidConfigurationInput())
            {
                return MenuState.CONFIGURATION;
            }

            if (IsValidSolutionInput())
            {
                return MenuState.SOLUTION;
            }

            MenuPrinterService.PrintUnknownSelectedSolution(UserInput);
            return MenuState.MAIN_MENU;
        }

        private static MenuState SolutionMenu()
        {
            var solutionSelectorService = GetSolutionSelectorService();
            var selectedDay = Int32.Parse(UserInput!);
            var selectedSolutions = solutionSelectorService.GetRequestedSolutions(selectedDay);
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

                        if (IsValidSolutionInput(selectedSolutions.Count))
                        {
                            var selectedSolution = Int32.Parse(UserInput);
                            var solutionOutput = selectedSolutions[selectedSolution - 1].Solve();
                            if (solutionOutput.IsMissingInput)
                            {
                                MenuPrinterService.PrintMissingInput(solutionOutput.ErrorMessage);
                            }
                            else
                            {
                                MenuPrinterService.PrintSolution(solutionOutput.Output);
                                MenuPrinterService.PrintComputationTime(solutionOutput.ComputationTimeInMs);
                            }
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

        private static ISolutionSelectorService GetSolutionSelectorService()
        {
            switch(CurrentYear)
            {
                case 2023: return new AdventOfCode2023.Solutions.SolutionSelectorService();
                case 2024: return new AdventOfCode2024.Solutions.SolutionSelectorService();
            }

            return new AdventOfCode2024.Solutions.SolutionSelectorService();
        }

        private static MenuState ConfigurationMenu()
        {
            CurrentYear = Int32.Parse(UserInput!);
            MenuPrinterService.PrintConfigurationChanged(CurrentYear);
            return MenuState.MAIN_MENU;
        }

        private static bool IsValidConfigurationInput()
        {
            int value = 0;
            Int32.TryParse(UserInput, out value);
            return value == 2023 || value == 2024;
        }

        private static bool IsValidSolutionInput(int max = 25)
        {
            int value = 0;
            Int32.TryParse(UserInput, out value);
            return value >= 1 && value <= max;
        }
    }
}