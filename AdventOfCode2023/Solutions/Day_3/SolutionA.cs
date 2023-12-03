using AdventOfCode2023.Models;
using AdventOfCode2023.Services;

namespace AdventOfCode2023.Solutions.Day_3
{
    /**
     *  PART 1
     *  1. Need a way to handle input string. The magic happens around the symbols.
     *  2. Scan through each line of input.
     *  3. Keep track of the previous and next line in input.
     *  4. Find the position(s) of the symbol(s) in the current line.
     *  5. When a symbol position is found, look at the surrounding area for any digits.
     *  6. If a digit is found, look left and right for other digits until all digits are found.
     *  7. Sum up this value to total
     *  8. Mark this digit as "used" by replacing the digits with "."
     **/
    public class SolutionA : ISolution
    {
        private const string FILE_PATH = "Solutions/Day_3/3.in";

        public void Solve()
        {
            var stringInputs = FileReaderService.ReadFile(FILE_PATH);
        }
    }
}