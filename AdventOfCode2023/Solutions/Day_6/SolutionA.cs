using AdventOfCode2023.Models;
using AdventOfCode2023.Services;
using System.Collections.Generic;

namespace AdventOfCode2023.Solutions.Day_6
{
    /**
     *  PART 1
     *  1. Create a dictionary with key time and value distance
     *  2. For each key, determine how many ways the values 1..N-1 will create a distance larger than the value.
     *  3. Each race can be calculated by multiplying the amount of seconds held with the amount of seconds left. 
     *  4. Multiply all the values together
     **/
    public class SolutionA : ISolution
    {
        private const string FILE_PATH = "Solutions/Day_6/6.in";

        public void Solve()
        {
            var stringInputs = FileReaderService.ReadFile(FILE_PATH);
            var uniqueWaysToWin = GetUniqueWaysToWin(stringInputs);
            MenuPrinterService.PrintSolution(uniqueWaysToWin.ToString());
        }

        private int GetUniqueWaysToWin(List<string> races)
        {
            var uniqueWaysToWin = 0;
            return uniqueWaysToWin;
        }
    }
}