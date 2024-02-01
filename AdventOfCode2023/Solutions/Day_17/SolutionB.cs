using AdventOfCode2023.Models;
using System;

namespace AdventOfCode2023.Solutions.Day_17
{
    /**
     *  PART 2
     *  1. 
     **/
    public class SolutionB : Solution
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