using AdventOfCode2023.Models;
using AdventOfCode2023.Services;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Solutions.Day_2
{
    /**
     *  PART 1
     *  1. We need to consider one set of cubes at the time as they are always put back into the bag
     *  2. Given a string, we need to parse it to get the amount of each cube
     *  3. We can seperate each set of cubes by splitting the string on the delimiter ';'
     *  4. If no color is above the maximum limit, return the game ID
     *      4a. The maximum limit will be a dictionary that we can compare to. We simply feed the color and get the maximum value back.
     *  5. Sum the value of all the game IDs.
     **/
    public class SolutionA : ISolution
    {
        private const string FILE_PATH = "Solutions/Day_2/2.in";
        private readonly Dictionary<string, int> CUBE_CONFIGURATION_DICTIONARY = new Dictionary<string, int>()
        {
            { "red", 12 },
            { "green", 13 },
            { "blue", 14 }
        };

        public void Solve()
        {
            var stringInputs = FileReaderService.ReadFile(FILE_PATH);
            var sum = GetSumOfPossibleGames(stringInputs);
            MenuPrinterService.PrintSolution(sum.ToString());
        }

        private int GetSumOfPossibleGames(List<string> stringInputs)
        {
            var sum = 0;
            for (int currentGame = 1; currentGame <= stringInputs.Count; currentGame++)
            {
                var gameInput = stringInputs[currentGame - 1];
                if (GameIsPossible(gameInput))
                {
                    sum += currentGame;
                }
            }

            return sum;
        }

        private bool GameIsPossible(string gameInput)
        {
            var parsedGameInput = Regex.Replace(gameInput, "Game\\s\\d+:", ""); // Remove game ID from string
            var cubeSets = parsedGameInput.Split(";"); // Seperate each set and compare
            foreach (var cubeSet in cubeSets)
            {
                var cubeColors = ParseSetIntoDictionary(cubeSet);
                foreach (var cubeColor in cubeColors)
                {
                    if (CUBE_CONFIGURATION_DICTIONARY[cubeColor.Key] < cubeColor.Value)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private Dictionary<string, int> ParseSetIntoDictionary(string cubeSet)
        {
            var cubeSetDictionary = new Dictionary<string, int>();
            // "3 blue, 4 red" -> ["3 blue", "4 red"]
            var colorSet = cubeSet.Split(",", StringSplitOptions.RemoveEmptyEntries);
            foreach (var color in colorSet)
            {
                // "3 blue" -> ["3", "blue"]
                var keyValuePair = color.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                var key = keyValuePair[1];
                var value = Int32.Parse(keyValuePair[0]);
                cubeSetDictionary.Add(key, value);
            }

            return cubeSetDictionary;
        }
    }
}