using AdventOfCode2023.Models;
using AdventOfCode2023.Services;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Solutions.Day_2
{
    /**
     *  PART 2
     *  1. We need to consider one set of cubes at the time as they are always put back into the bag
     *  2. Given a string, we need to parse it to get the amount of each cube
     *  3. Instead of seperating the sets, we only need to consider what is the maximum of each color for each game?
     *  4. Replace the semi colon with commas and consider the whole input at once
     *  5. Create a temporary dictionary. 
     *  6. For each key-value (color-amount) pair, store it in the dictionary if the current value is bigger than the previous value
     *  7. Return the dictionary and multiply all its values
     *  8. Sum the value of all the product values in step 7
     **/
    public class SolutionB : ISolution
    {
        private const string FILE_PATH = "Solutions/Day_2/2.in";

        public void Solve()
        {
            var stringInputs = FileReaderService.ReadFile(FILE_PATH);
            var sum = GetSumOfPowerOfEachGame(stringInputs);
            MenuPrinterService.PrintSolution(sum.ToString());
        }

        private int GetSumOfPowerOfEachGame(List<string> stringInputs)
        {
            var sum = 0;
            for (int currentGame = 1; currentGame <= stringInputs.Count; currentGame++)
            {
                var powerOfSet = 1;
                var gameInput = stringInputs[currentGame - 1];
                var minimumAmountOfCubesNeededInThisGame = FindMinimumAmountOfCubesNeeded(gameInput);
                foreach(var colorValuePair in minimumAmountOfCubesNeededInThisGame)
                {
                    powerOfSet *= colorValuePair.Value;
                }
                sum += powerOfSet;
            }

            return sum;
        }

        private Dictionary<string, int> FindMinimumAmountOfCubesNeeded(string gameInput)
        {
            var parsedGameInput = Regex.Replace(gameInput, "Game\\s\\d+:", ""); // Remove game ID from string
            parsedGameInput = parsedGameInput.Replace(";", ",");
            return ParseSetIntoDictionary(parsedGameInput);
        }

        private Dictionary<string, int> ParseSetIntoDictionary(string cubeSet)
        {
            var cubeSetDictionary = new Dictionary<string, int>();
            var colorSet = cubeSet.Split(",");
            foreach (var color in colorSet)
            {
                var keyValuePair = color.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                var key = keyValuePair[1];
                var value = Int32.Parse(keyValuePair[0]);
                if (cubeSetDictionary.ContainsKey(key))
                {
                    if (cubeSetDictionary[key] < value)
                    {
                        cubeSetDictionary[key] = value;
                    }
                }
                else
                {
                    cubeSetDictionary.Add(key, value);
                }
            }

            return cubeSetDictionary;
        }
    }
}