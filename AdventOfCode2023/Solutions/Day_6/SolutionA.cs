using AdventOfCode.Core.Models;
using System;
using System.Collections.Generic;

namespace AdventOfCode2023.Solutions.Day_6
{
    /**
     *  PART 1
     *  1. Create a dictionary with key time and value distance
     *  2. For each key, determine how many ways the values 1..N-1 will create a distance larger than the value.
     *  3. Each race can be calculated by multiplying the amount of seconds held with the amount of seconds left. 
     *  4. Multiply all the values together
     **/
    public class SolutionA : Solution
    {
        protected override string GetSolutionOutput()
        {
            var uniqueWaysToWin = GetUniqueWaysToWin();
            return uniqueWaysToWin.ToString();
        }

        private int GetUniqueWaysToWin()
        {
            var totalUniqueWaysToWin = 1;
            var races = ParseRacesDictionary();
            foreach (var race in races) 
            { 
                var uniqueWaysToWin = CalculateUniqueWaysToWinRace(race.Key, race.Value);
                totalUniqueWaysToWin *= uniqueWaysToWin;
            }
            return totalUniqueWaysToWin;
        }

        private Dictionary<int, int> ParseRacesDictionary()
        {
            var raceDictionary = new Dictionary<int, int>();
            var times = stringInputs[0].Split("Time:")[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var distances = stringInputs[1].Split("Distance:")[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < times.Length; i++)
            {
                var time = Int32.Parse(times[i]);
                var distance = Int32.Parse(distances[i]);
                raceDictionary[time] = distance;
            }

            return raceDictionary;
        }

        private int CalculateUniqueWaysToWinRace(int maximumTime, int targetDistance)
        {
            var firstWinIndex = maximumTime - 1;
            var lastWinIndex = 1;
            for (var timeHeld = 1; timeHeld <= firstWinIndex; timeHeld++)
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

        private int CalculateDistance(int maximumTime, int timeHeld)
        {
            var timeLeft = maximumTime - timeHeld;
            return timeLeft * timeHeld;            
        }
    }
}