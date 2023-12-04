using AdventOfCode2023.Models;
using AdventOfCode2023.Services;
using System.Collections.Generic;

namespace AdventOfCode2023.Solutions.Day_4
{
    /**
     *  PART 2
     *  1. Create a dictionary to contain the amount of won card per scratch card
     *  2. Keep track of the total amount of copies for each scratch card
     *  3. Iterate through each copy and the amount won to calculate the amount of scratch cards
     *  4. Sum the value for each iteration
     **/
    public class SolutionB : ISolution
    {
        private const string FILE_PATH = "Solutions/Day_4/4.in";

        public void Solve()
        {
            var stringInputs = FileReaderService.ReadFile(FILE_PATH);
            var sum = GetAmountOfScratchCards(stringInputs);
            MenuPrinterService.PrintSolution(sum.ToString());
        }

        public int GetAmountOfScratchCards(List<string> scratchCards)
        {
            var sum = 0;
            return sum;
        }
    }
}


