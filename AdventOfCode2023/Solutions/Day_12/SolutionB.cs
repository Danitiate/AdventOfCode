using AdventOfCode.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace AdventOfCode2023.Solutions.Day_12
{
    /**
     *  PART 2
     *  1. The solution created in part 1 will not work here due to Big O.
     *  2. For the parser, we will simply repeat the stringInputs by 5 and add a delimiter '?' between each springCondition
     *  3. Instead of splitting the string on every '?' into two new strings, we can recursively make the springConditions smaller for every group found
     *  4. Eventually we will have reduced the string and groups into smaller values, where we can determine whether or not we have a valid input given the state of our current values
     *  5. Due to large amount of computation, we will cache the result of every compute we have completed. This will prevent duplicate computations and thus drastically improve performance.
     *  6. We sum up the values for every springRow. Need a long instead of int due to large numbers
     **/
    public class SolutionB : Solution
    {
        private Dictionary<string, long> Cache = new Dictionary<string, long>();

        protected override string GetSolutionOutput()
        {
            var sumOfPossibleArrangements = GetSumOfPossibleArrangements();
            return sumOfPossibleArrangements.ToString();
        }

        private long GetSumOfPossibleArrangements()
        {
            var springRows = ParseInputToSpringRows();
            var sum = 0L;
            foreach (var springRow in springRows)
            {
                sum += CountNumberOfPossibleArrangementsForSpringRow(springRow);
            }

            return sum;
        }

        private List<SpringRow> ParseInputToSpringRows()
        {
            var springRows = new List<SpringRow>();
            foreach (var stringInput in stringInputs)
            {
                var splitStringInput = stringInput.Split(" ");
                var groupSizeString = string.Join(',', Enumerable.Repeat(splitStringInput[1], 5));
                springRows.Add(new SpringRow
                {
                    SpringConditions = string.Join('?', Enumerable.Repeat(splitStringInput[0], 5)),
                    GroupSizes = groupSizeString.Split(",")
                        .Select(s => Int32.Parse(s))
                        .ToList()
                });
            }

            return springRows;
        }

        private long CountNumberOfPossibleArrangementsForSpringRow(SpringRow springRow)
        {
            var result = 0L;
            if (Cache.TryGetValue($"{springRow.SpringConditions}_{string.Join('_', springRow.GroupSizes)}", out long value)) {
                return value;
            }
            if (springRow.GroupSizes.Count == 0)
            {
                return springRow.SpringConditions.Contains("#") ? 0 : 1;
            }

            int currentGroupSize = springRow.GroupSizes[0];

            List<int> remainingSprings = springRow.GroupSizes.Skip(1).ToList();
            var indexRange = springRow.SpringConditions.Length - remainingSprings.Sum() - remainingSprings.Count - currentGroupSize + 1;
            for (int i = 0; i < indexRange; i++)
            {
                if (springRow.SpringConditions.Substring(0, i).Contains("#"))
                {
                    break;
                }

                int nextGroupLength = i + currentGroupSize;
                var nextChar = springRow.SpringConditions.ElementAtOrDefault(nextGroupLength);
                if (nextGroupLength <= springRow.SpringConditions.Length && !springRow.SpringConditions.Substring(i, nextGroupLength - i).Contains(".") && nextChar != '#')
                {
                    var nextSpringRow = new SpringRow
                    {
                        SpringConditions = nextChar != default ? springRow.SpringConditions.Substring(nextGroupLength + 1) : "",
                        GroupSizes = remainingSprings
                    };

                    result += CountNumberOfPossibleArrangementsForSpringRow(nextSpringRow);
                }
            }

            Cache[$"{springRow.SpringConditions}_{string.Join('_', springRow.GroupSizes)}"] = result;
            return result;
        }
    }
}