using AdventOfCode2023.Models;

namespace AdventOfCode2023.Solutions.Day_9
{
    /**
     *  PART 2
     *  1. 
     **/
    public class SolutionB : Solution
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