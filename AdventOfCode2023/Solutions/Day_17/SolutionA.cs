using AdventOfCode2023.Models;
using System;

namespace AdventOfCode2023.Solutions.Day_17
{
    /**
     *  PART 1
     *  1. 
     **/
    public class SolutionA : Solution
    {
        protected override string GetSolutionOutput()
        {
            var minimumAmountOfHeatLoss = FindMinimumHeatLossPath();
            return minimumAmountOfHeatLoss.ToString();
        }

        private int FindMinimumHeatLossPath()
        {
            throw new NotImplementedException();
        }
    }
}