using AdventOfCode2023.Models;
using System;

namespace AdventOfCode2023.Solutions.Day_18
{
    /**
     *  PART 2
     *  1. 
     **/
    public class SolutionB : Solution
    {
        protected override string GetSolutionOutput()
        {
            var minimumAmountOfHeatLoss = CalculateLavaVolume();
            return minimumAmountOfHeatLoss.ToString();
        }

        private int CalculateLavaVolume()
        {
            throw new NotImplementedException();
        }
    }
}