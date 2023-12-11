using AdventOfCode2023.Models;

namespace AdventOfCode2023.Solutions.Day_11
{
    /**
     *  PART 2
     *  1. Keep same solution as part 1
     *  2. Instead of adding new columns or rows to the expanded universe list, replace the empty row / column with a row of "E" for empty.
     *  3. Then in the calculations, count distance like normal, but for every "E" that is passed, add 1.000.000
     **/
    public class SolutionB : Solution
    {
        protected override string GetSolutionOutput()
        {
            var sumOfGalaxyDistances = GetSumOfShortestPathsBetweenGalaxies();
            return sumOfGalaxyDistances.ToString();
        }

        private int GetSumOfShortestPathsBetweenGalaxies()
        {
            var sum = 0;
            return sum;
        }
    }
}