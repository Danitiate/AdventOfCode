using AdventOfCode.Core.Models;
using System.Collections.Generic;

namespace AdventOfCode2023.Solutions.Day_8
{
    /**
     *  PART 1
     *  1. Figure out what operations are needed
     *  2. Create a dictionary of a tuple, containing two string values left and right
     *  3. Loop through each operation until a solution is found, counting each step on the way
     **/
    public class SolutionA : Solution
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
            var currentPosition = "AAA";
            for (int i = 0; i <= operations.Length; i++)
            {
                if (currentPosition == "ZZZ")
                {
                    break;
                }
                else if (i == operations.Length)
                {
                    i = -1;
                    continue;
                }

                amountOfSteps++;
                var operation = operations[i];
                if (operation == 'L')
                {
                    currentPosition = leftRightMap[currentPosition].Item1;
                }
                else
                {
                    currentPosition = leftRightMap[currentPosition].Item2;
                }
            }

            return amountOfSteps;
        }
    }
}