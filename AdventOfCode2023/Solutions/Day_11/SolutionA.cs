using AdventOfCode2023.Models;

namespace AdventOfCode2023.Solutions.Day_11
{
    /**
     *  PART 1
     *  1. Parse input into a list of strings
     *  2. If an input line is all dots ".", expand the universe by adding another row of the same line
     *  3. After all lines are read, consider all columns and find the columns where all characters are dots ".".
     *  4. Add another dot at this position for every line, resulting in another column
     *  5. Find every galaxy and its coordinates
     *  6. For every galaxy, find the distance to every other galaxy by finding the difference between their coordinates
     *  7. Sum all values found in step 6 and return
     **/
    public class SolutionA : Solution
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