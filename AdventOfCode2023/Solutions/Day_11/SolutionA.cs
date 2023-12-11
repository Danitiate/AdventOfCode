using AdventOfCode2023.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2023.Solutions.Day_11
{
    /**
     *  PART 1
     *  1. Parse input into a list of strings
     *  2. If an input line is all dots ".", expand the universe by adding another row of the same line
     *  3. After all lines are read, consider all columns and find the columns where all characters are dots ".".
     *  4. Add another dot at this position for every line, resulting in another column
     *  5. Find every galaxy and its coordinates
     *  6. For every galaxy, find the distance to every other galaxy by finding the difference between their coordinates
     *  7. Sum all values found in step 6 and return
     **/
    public class SolutionA : Solution
    {
        protected override string GetSolutionOutput()
        {
            var sumOfGalaxyDistances = GetSumOfShortestPathsBetweenGalaxies();
            return sumOfGalaxyDistances.ToString();
        }

        private int GetSumOfShortestPathsBetweenGalaxies()
        {
            var galaxies = GetGalaxiesFromInput();
            var sum = calculateDistanceOfAllGalaxies(galaxies);
            return sum;
        }

        private List<(int, int)> GetGalaxiesFromInput()
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
                universe.Add(stringInput);
                if (stringInput.All(s => s == '.'))
                {
                    universe.Add(stringInput);
                }
            }

            var universeWidth = universe[0].Length;
            for(int i = 0; i < universeWidth; i++)
            {
                var columnIsEmptySpace = true;
                for(int j = 0; j < universe.Count(); j++)
                {
                    var nextChar = universe[j].ElementAt(i);
                    if (nextChar != '.')
                    {
                        columnIsEmptySpace = false;
                        break;
                    }
                }
                if (columnIsEmptySpace)
                {
                    for (int j = 0; j < universe.Count(); j++)
                    {
                        universe[j] = universe[j].Insert(i, ".");
                    }
                    i++;
                    universeWidth++;
                }
            }

            return universe;
        }

        private List<(int, int)> GetGalaxyCoordinatesInUniverse(List<string> universe)
        {
            var galaxies = new List<(int, int)>();
            for (int i = 0; i < universe.Count; i++)
            {
                var universeInput = universe[i];
                for(int j = 0; j < universeInput.Length; j++)
                {
                    if (universeInput.ElementAt(j) == '#')
                    {
                        galaxies.Add((j, i));
                    }
                }
            }

            return galaxies;
        }

        private int calculateDistanceOfAllGalaxies(List<(int, int)> galaxies)
        {
            var sum = 0;
            for (int i = 0; i < galaxies.Count - 1; i++)
            {
                for(int j = i + 1; j < galaxies.Count; j++)
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