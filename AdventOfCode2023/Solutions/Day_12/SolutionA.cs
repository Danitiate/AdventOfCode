using AdventOfCode.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Solutions.Day_12
{
    /**
     *  PART 1
     *  1. Parse input into two categories:
     *      A: The spring conditions
     *      B: A list of group sizes
     *  2. Find every possible spring condition rows by replacing any "?" value with "." and "#" as two new instances
     *  3. Recursively iterate through all possible spring conditions and count all results with a valid arrangement
     *  4. Sum the result of every row iterated through step 3
     **/
    public class SolutionA : Solution
    {
        protected override string GetSolutionOutput()
        {
            var sumOfPossibleArrangements = GetSumOfPossibleArrangements();
            return sumOfPossibleArrangements.ToString();
        }

        private int GetSumOfPossibleArrangements()
        {
            var springRows = ParseInputToSpringRows();
            var sum = 0;
            foreach(var springRow in springRows)
            {
                sum += CountNumberOfPossibleArrangementsForSpringRow(springRow);
            }
            return sum;
        }

        private List<SpringRow> ParseInputToSpringRows()
        {
            var springRows = new List<SpringRow>();
            foreach(var stringInput in stringInputs)
            {
                var splitStringInput = stringInput.Split(" ");
                springRows.Add(new SpringRow
                {
                    SpringConditions = splitStringInput[0],
                    GroupSizes = splitStringInput[1].Split(",")
                        .Select(s => Int32.Parse(s))
                        .ToList()
                });
            }

            return springRows;
        }

        private int CountNumberOfPossibleArrangementsForSpringRow(SpringRow springRow)
        {
            if (!HasEnoughDefectsAndWildcardsToMatchGroup(springRow))
            {
                return 0;
            }

            var amountOfPossibleArrangements = 0;
            if (IsPredictionComplete(springRow))
            {
                if (IsArrangementPossible(springRow))
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            } 
            else
            {
                var regex = new Regex("\\?");
                var currentSpringConditionString = springRow.SpringConditions;
                var springRowOk = new SpringRow
                {
                    GroupSizes = springRow.GroupSizes,
                    SpringConditions = regex.Replace(currentSpringConditionString, ".", 1)
                };

                var springRowDefect = new SpringRow
                {
                    GroupSizes = springRow.GroupSizes,
                    SpringConditions = regex.Replace(currentSpringConditionString, "#", 1)
                };

                amountOfPossibleArrangements += CountNumberOfPossibleArrangementsForSpringRow(springRowOk);
                amountOfPossibleArrangements += CountNumberOfPossibleArrangementsForSpringRow(springRowDefect);
            }

            return amountOfPossibleArrangements;
        }

        private bool HasEnoughDefectsAndWildcardsToMatchGroup(SpringRow springRow)
        {
            var expectedAmountOfDefectsAndWildcards = springRow.GroupSizes.Sum();
            var amountOfDefects = springRow.SpringConditions.Count(s => s == '#');
            var amountOfWildcards = springRow.SpringConditions.Count(s => s == '?');
            return amountOfDefects + amountOfWildcards >= expectedAmountOfDefectsAndWildcards && amountOfDefects <= expectedAmountOfDefectsAndWildcards;
        }

        private bool IsPredictionComplete(SpringRow springRow)
        {
            return springRow.SpringConditions.All(c => c != '?');
        }

        private bool IsArrangementPossible(SpringRow springRow)
        {
            var springGroups = springRow.SpringConditions.Split(".", StringSplitOptions.RemoveEmptyEntries);
            if (springGroups.Length == springRow.GroupSizes.Count)
            {
                for(int i = 0; i < springGroups.Length; i++)
                {
                    if (springGroups[i].Length != springRow.GroupSizes[i])
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }

            return true;
        }
    }
}