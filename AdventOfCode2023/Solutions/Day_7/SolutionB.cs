using AdventOfCode2023.Models;
using AdventOfCode2023.Services;
using System;
using System.Collections.Generic;

namespace AdventOfCode2023.Solutions.Day_7
{
    /**
    *  PART 2
    *  1. Logic can remain mostly the same as Part 1
    *  2. Parse every line of input into a CamelCardGame
    *  3. A CamelCardGame should consist of a hand (string of length 5) and a bid (int)
    *  4. Insert the CamelCardGame into a list of CamelCardGames such that the list is sorted by hand strength
    *      4a. Need an algorithm to determine which hand is stronger. This will be used in the comparison.
    *      4b. As our list of CamelCardGames is sorted during insertion, we can do a binary search through the current list to figure out the correct position
    *  5. For each CamelCardGame in the list, calculate its winnings by multiplying its rank (index + 1) with the bid: (index + 1) * bid = winnings
    *  6. Sum up all winnings
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