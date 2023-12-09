using AdventOfCode2023.Models;

namespace AdventOfCode2023.Solutions.Day_9
{
    /**
     *  PART 1
     *  1. For every input, collect all the numbers
     *  2. Recursively subtract the numbers xN - xN-1 in every list and add the difference to a new list
     *  3. Continue recursion until you have a list of all zeroes
     *  4. Return the last value of the sequence (this will eventually be 0)
     *  5. Add the value returned in step 5 with the last value in the list and return the new sum
     *  6. The final sum will be the sum of all extrapolated numbers
     **/
    public class SolutionA : Solution
    {
        protected override string GetSolutionOutput()
        {
            var amountOfStepsRequired = GetSumOfExtrapolatedNumbers();
            return amountOfStepsRequired.ToString();
        }

        private int GetSumOfExtrapolatedNumbers()
        {
            var sum = 0;
            return sum;
        }
    }
}