using AdventOfCode2023.Models;
using System;
using System.Collections.Generic;

namespace AdventOfCode2023.Solutions.Day_8
{
    /**
     *  PART 2
     *  1. Figure out what operations are needed
     *  2. Create a dictionary of a tuple, containing two string values left and right
     *  3. Find all starting positions (Keys in the dictionary that end with "A") and store these in a list
     *  4. Loop through each operation until a solution is found, counting each step on the way
     *  5. A solution is found when the list defined in step 3 all end with "Z"
     **/
    public class SolutionB : Solution
    {
        protected override string GetSolutionOutput()
        {
            var amountOfStepsRequired = CountAmountOfStepsRequired();
            return amountOfStepsRequired.ToString();
        }

        private int CountAmountOfStepsRequired()
        {
            var operations = ParseOperationsFromInput();
            var leftRightMap = ParseLeftRightMapFromInput();
            var amountOfStepsRequired = TraverseMap(operations, leftRightMap);
            return amountOfStepsRequired;
        }

        private string ParseOperationsFromInput()
        {
            return stringInputs[0];
        }

        private Dictionary<string, (string, string)> ParseLeftRightMapFromInput()
        {
            var leftRightMap = new Dictionary<string, (string, string)>();
            foreach (var stringInput in stringInputs)
            {
                var splitString = stringInput.Split(" = ");
                if (splitString.Length != 2)
                {
                    continue;
                }

                var key = splitString[0];
                var values = splitString[1]
                    .Replace("(", "")
                    .Replace(")", "")
                    .Split(", ");

                leftRightMap.Add(key, (values[0], values[1]));
            }

            return leftRightMap;
        }

        private int TraverseMap(string operations, Dictionary<string, (string, string)> leftRightMap)
        {
            var amountOfSteps = 0;
            return amountOfSteps;
        }
    }
}