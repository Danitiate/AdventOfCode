using AdventOfCode.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2023.Solutions.Day_8
{
    /**
     *  PART 2
     *  1. Figure out what operations are needed
     *  2. Create a dictionary of a tuple, containing two string values left and right
     *  3. Find all starting positions (Keys in the dictionary that end with "A") and store these in a list
     *  4. For each starting position, find the solution following the algorithm created in Part 1
     *  5. Find the least common multiple of all the solutions found in step 4. This will be the amount of steps required when all solutions have the same ending
     **/
    public class SolutionB : Solution
    {
        protected override string GetSolutionOutput()
        {
            var amountOfStepsRequired = CountAmountOfStepsRequired();
            return amountOfStepsRequired.ToString();
        }

        private long CountAmountOfStepsRequired()
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

        private long TraverseMap(string operations, Dictionary<string, (string, string)> leftRightMap)
        {
            var amountOfSteps = 0;
            var startingPositions = GetStartingPositionsFromMap(leftRightMap);
            var amountOfStepsRequiredForEachSolution = new List<long>();
            for (int i = 0; i < startingPositions.Count; i++)
            {
                var currentPosition = startingPositions[i];
                for (int j = 0; j <= operations.Length; j++)
                {
                    if (currentPosition.Last() == 'Z')
                    {
                        amountOfStepsRequiredForEachSolution.Add(amountOfSteps);
                        amountOfSteps = 0;
                        break;
                    }
                    else if (j == operations.Length)
                    {
                        j = -1;
                        continue;
                    }

                    amountOfSteps++;
                    var operation = operations[j];
                    if (operation == 'L')
                    {
                        currentPosition = leftRightMap[currentPosition].Item1;
                    }
                    else
                    {
                        currentPosition = leftRightMap[currentPosition].Item2;
                    }
                }
            }

            return FindLeastCommonMultiple(amountOfStepsRequiredForEachSolution);
        }

        private List<string> GetStartingPositionsFromMap(Dictionary<string, (string, string)> leftRightMap)
        {
            return leftRightMap.Keys.Where(k => k.Last() == 'A').ToList();
        }

        private long FindLeastCommonMultiple(List<long> amountOfStepsRequiredForEachSolution)
        {
            return amountOfStepsRequiredForEachSolution.Aggregate((sum, value) => sum * value / GetGreatedCommonDenominator(sum, value));
        }

        private long GetGreatedCommonDenominator(long n1, long n2)
        {
            if (n2 == 0)
            {
                return n1;
            }
            else
            {
                return GetGreatedCommonDenominator(n2, n1 % n2);
            }
        }
    }
}