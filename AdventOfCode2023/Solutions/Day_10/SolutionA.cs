using AdventOfCode2023.Models;

namespace AdventOfCode2023.Solutions.Day_10
{
    /**
     *  PART 1
     *  1. Parse input into a list of strings
     *  2. Find the starting point "S" and all possible connected pipes
     *  3. Considering every pipe only has an entry point and an exit, we can create a nested object that points to each connected pipe
     *  4. Using the position of previous object, we can figure out where the next object is
     *  5. We can continue down the path until we end up back at "S", counting the amount of steps on the way
     *  6. Once a path is found, do the same path in reverse and count the amount of steps again. The smallest number determines its real distance
     *  7. Return the tile with the highest number
     **/
    public class SolutionA : Solution
    {
        protected override string GetSolutionOutput()
        {
            var tileDistance = GetDistanceOfTileFurthestAwayFromStartingPoint();
            return tileDistance.ToString();
        }

        private int GetDistanceOfTileFurthestAwayFromStartingPoint()
        {
            var distance = -1;
            return distance;
        }
    }
}