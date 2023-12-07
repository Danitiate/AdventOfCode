using AdventOfCode2023.Models;
using System;

namespace AdventOfCode2023.Solutions.Day_6
{
    /**
     *  PART 2
     *  1. Create a tuple with key time and value distance. Append all numbers together before adding.
     *  2. Calculate the amount of unique ways to win
     *  3. The amount of possible solutions can be calculated using parabolas: https://en.wikipedia.org/wiki/Quadratic_formula
     *  4. Return the result of lower and upper bounds after calculation
     **/
    public class SolutionB : Solution
    {
        protected override string GetSolutionOutput()
        {
            var uniqueWaysToWin = GetUniqueWaysToWin();
            return uniqueWaysToWin.ToString();
        }

        private long GetUniqueWaysToWin()
        {
            var race = ParseRaceTuple();
            return CalculateUniqueWaysToWinRace(race.Item1, race.Item2);
        }

        private (long, long) ParseRaceTuple()
        {
            var times = stringInputs[0].Split("Time:")[1];
            var distances = stringInputs[1].Split("Distance:")[1];
            times = times.Replace(" ", "");
            distances = distances.Replace(" ", "");
            var time = long.Parse(times);
            var distance = long.Parse(distances);
            return (time, distance);
        }

        private long CalculateUniqueWaysToWinRace(long maximumTime, long targetDistance)
        {
            /** 
             * Quadratic Formula: (-B +/- sqrt(B^2 - 4AC) / 2A
             * A is a constant, in this case it is simply 1 and can be ignored
             * B is our maximumTime
             * C is our targetDistance
             */
            var root = Math.Sqrt(Math.Pow(maximumTime, 2) - (4.0 * targetDistance));
            var quadraticFormula1 = -maximumTime + root / (-2);
            var quadraticFormula2 = -maximumTime - root / (-2);
            long firstWin = (long)Math.Ceiling(quadraticFormula1);
            long secondWin = (long)Math.Ceiling(quadraticFormula2);
            return secondWin - firstWin;
        }
    }
}