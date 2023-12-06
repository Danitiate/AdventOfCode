using AdventOfCode2023.Models;
using AdventOfCode2023.Services;
using System.Collections.Generic;

namespace AdventOfCode2023.Solutions.Day_6
{
    /**
     *  PART 2
     *  1. Create a tuple with key time and value distance. Append all numbers together before adding.
     *  2. For each key, determine how many ways the values 1..N-1 will create a distance larger than the value.
     *  3. Each race can be calculated by multiplying the amount of seconds held with the amount of seconds left. 
     *  4. Multiply all the values together
     **/
    public class SolutionB : ISolution
    {
        private const string FILE_PATH = "Solutions/Day_6/6.in";

        public void Solve()
        {
            var stringInputs = FileReaderService.ReadFile(FILE_PATH);
            var uniqueWaysToWin = GetUniqueWaysToWin(stringInputs);
            MenuPrinterService.PrintSolution(uniqueWaysToWin.ToString());
        }

        private long GetUniqueWaysToWin(List<string> stringInputs)
        {
            var race = ParseRaceTuple(stringInputs);
            return CalculateUniqueWaysToWinRace(race.Item1, race.Item2);
        }

        private (long, long) ParseRaceTuple(List<string> stringInputs)
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
            var firstWinIndex = maximumTime - 1;
            var lastWinIndex = 1L;
            for (var timeHeld = 1L; timeHeld <= firstWinIndex; timeHeld++)
            {
                var distance = CalculateDistance(maximumTime, timeHeld);
                if (distance > targetDistance)
                {
                    firstWinIndex = timeHeld;
                    break;
                }
            }

            for (var timeHeld = maximumTime - 1; timeHeld >= lastWinIndex; timeHeld--)
            {
                var distance = CalculateDistance(maximumTime, timeHeld);
                if (distance > targetDistance)
                {
                    lastWinIndex = timeHeld;
                    break;
                }
            }

            return lastWinIndex - firstWinIndex + 1;
        }

        private long CalculateDistance(long maximumTime, long timeHeld)
        {
            var timeLeft = maximumTime - timeHeld;
            return timeLeft * timeHeld;            
        }
    }
}