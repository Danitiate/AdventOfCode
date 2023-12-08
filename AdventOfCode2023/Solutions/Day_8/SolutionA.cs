using AdventOfCode2023.Models;

namespace AdventOfCode2023.Solutions.Day_8
{
    /**
     *  PART 1
     *  1. Figure out what operations are needed
     *  2. Create a dictionary of a tuple, containing two string values left and right
     *  3. Loop through each operation until a solution is found, counting each step on the way
     **/
    public class SolutionA : Solution
    {
        protected override string GetSolutionOutput()
        {
            var amountOfStepsRequired = CountAmountOfStepsRequired();
            return amountOfStepsRequired.ToString();
        }

        private int CountAmountOfStepsRequired()
        {
            var amountOfStepsRequired = 0;
            return amountOfStepsRequired;
        }
    }
}