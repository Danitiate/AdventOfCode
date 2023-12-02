using AdventOfCode2023.Models;
using AdventOfCode2023.Services;

namespace AdventOfCode2023.Solutions.Day_2
{
    /**
     *  PART 1
     *  1. We need to consider one set of cubes at the time as they are always put back into the bag
     *  2. Given a string, we need to parse it to get the amount of each cube
     *  3. We can seperate each set of cubes by splitting the string on the delimiter ';'
     *  4. If any color is above the maximum limit, return the game ID
     *  5. The maximum limit will be a dictionary that we can compare to. We simply feed the color and get the maximum value back.
     *  6. Sum the value of all the game IDs.
     **/
    public class SolutionA : ISolution
    {
        private const string FILE_PATH = "Solutions/Day_2/2.in";

        public void Solve()
        {
            var stringInputs = FileReaderService.ReadFile(FILE_PATH);
        }
    }
}