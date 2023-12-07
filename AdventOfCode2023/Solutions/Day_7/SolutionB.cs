using AdventOfCode2023.Models;
using AdventOfCode2023.Services;
using System;
using System.Collections.Generic;

namespace AdventOfCode2023.Solutions.Day_7
{
    /**
     *  PART 2
     *  TBD
     **/
    public class SolutionB : ISolution
    {
        private const string FILE_PATH = "Solutions/Day_7/7.in";

        public void Solve()
        {
            var stringInputs = FileReaderService.ReadFile(FILE_PATH);
            var uniqueWaysToWin = PlayCamelCardGames(stringInputs);
            MenuPrinterService.PrintSolution(uniqueWaysToWin.ToString());
        }

        private int PlayCamelCardGames(List<string> stringInputs)
        {
            var totalWinnings = 0;
            return totalWinnings;
        }
    }
}