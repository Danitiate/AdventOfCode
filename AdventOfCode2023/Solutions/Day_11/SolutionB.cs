using AdventOfCode2023.Models;

namespace AdventOfCode2023.Solutions.Day_11
{
    /**
     *  PART 2
     *  1. 
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