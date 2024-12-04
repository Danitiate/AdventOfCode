using AdventOfCode.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace AdventOfCode2023.Solutions.Day_11
{
    /**
     *  PART 2
     *  1. Keep same solution as part 1
     *  2. Instead of adding new columns or rows to the expanded universe list, replace the empty row / column with a row of "E" for empty.
     *  3. For every "E" that is passed, add 1.000.000 to the galaxy coordinates
     *  4. Keep calculations like normal
     **/
    public class SolutionB : Solution
    {
        protected override string GetSolutionOutput()
        {
            var sumOfGalaxyDistances = GetSumOfShortestPathsBetweenGalaxies();
            return sumOfGalaxyDistances.ToString();
        }

        private long GetSumOfShortestPathsBetweenGalaxies()
        {
            var galaxies = GetGalaxiesFromInput();
            var sum = calculateDistanceOfAllGalaxies(galaxies);
            return sum;
        }

        private List<(long, long)> GetGalaxiesFromInput()
        {
            var universe = GetExpandedUniverseFromInput();
            var galaxies = GetGalaxyCoordinatesInUniverse(universe);
            return galaxies;
        }

        private List<string> GetExpandedUniverseFromInput()
        {
            var universe = new List<string>();
            foreach (var stringInput in stringInputs)
            {
                if (stringInput.All(s => s == '.'))
                {
                    var emptySpace = String.Concat(Enumerable.Repeat("E", stringInput.Length));
                    universe.Add(emptySpace);
                }
                else
                {
                    universe.Add(stringInput);
                }
            }

            var universeWidth = universe[0].Length;
            for (int i = 0; i < universeWidth; i++)
            {
                var columnIsEmptySpace = true;
                for (int j = 0; j < universe.Count(); j++)
                {
                    var nextChar = universe[j].ElementAt(i);
                    if (nextChar != '.' && nextChar != 'E')
                    {
                        columnIsEmptySpace = false;
                        break;
                    }
                }
                if (columnIsEmptySpace)
                {
                    for (int j = 0; j < universe.Count(); j++)
                    {
                        universe[j] = universe[j].Remove(i, 1);
                        universe[j] = universe[j].Insert(i, "E");
                    }
                }
            }

            return universe;
        }

        private List<(long, long)> GetGalaxyCoordinatesInUniverse(List<string> universe)
        {
            var galaxies = new List<(long, long)>();
            var valueOfEmptySpace = 1000000;
            var amountOfEmptySpaceInRows = 0;
            for (int i = 0; i < universe.Count; i++)
            {
                var universeInput = universe[i];
                if (universeInput.All(s => s == 'E'))
                {
                    amountOfEmptySpaceInRows++;
                    continue;
                }
                var amountOfEmptySpaceInColumns = 0;
                for (int j = 0; j < universeInput.Length; j++)
                {
                    if (universeInput.ElementAt(j) == 'E')
                    {
                        amountOfEmptySpaceInColumns++;
                    }
                    if (universeInput.ElementAt(j) == '#')
                    {
                        galaxies.Add((j - amountOfEmptySpaceInColumns + valueOfEmptySpace * amountOfEmptySpaceInColumns, i - amountOfEmptySpaceInRows + valueOfEmptySpace * amountOfEmptySpaceInRows));
                    }
                }
            }

            return galaxies;
        }

        private long calculateDistanceOfAllGalaxies(List<(long, long)> galaxies)
        {
            var sum = 0L;
            for (int i = 0; i < galaxies.Count - 1; i++)
            {
                for (int j = i + 1; j < galaxies.Count; j++)
                {
                    var galaxyA = galaxies[i];
                    var galaxyB = galaxies[j];
                    sum += Math.Abs(galaxyB.Item1 - galaxyA.Item1);
                    sum += Math.Abs(galaxyB.Item2 - galaxyA.Item2);
                }
            }

            return sum;
        }
    }
}