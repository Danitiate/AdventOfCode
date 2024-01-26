using AdventOfCode2023.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2023.Solutions.Day_14
{
    /**
     *  PART 1
     *  1. Parse puzzle input into list of rows
     *  2. Select the first row and iterate through each index (column)
     *  3. For each column, iterate through each row
     *      3a. Keep track of the starting index (currentRow), counting any "O" (roundedRocksCount) on the way until a "#" is encountered.
     *      3b. Mark the cells from currentRow to currentRow + roundedRocksCount as "O" and the rest as "."
     *      3c. Set currentRow to the index after "#" was found
     *  4. Calculate the sum of rounded rocks by counting the amount of rounded rocks per row, multiplying it by amountOfRows - currentRowIndex
     **/
    public class SolutionA : Solution
    {
        protected override string GetSolutionOutput()
        {
            var sumOfRoundedRocksLoad = GetSumOfRoundedRocksLoad();
            return sumOfRoundedRocksLoad.ToString();
        }

        private int GetSumOfRoundedRocksLoad()
        {
            var tiltedPlatform = TiltPlatformNorth(stringInputs);
            return CalculatePlatformLoad(tiltedPlatform);
        }

        private List<string> TiltPlatformNorth(List<string> platform)
        {
            var northTiltedPlatform = platform.ToList();
            var firstRow = platform[0];
            for (int currentColumn = 0; currentColumn < firstRow.Length; currentColumn++)
            {
                var roundedRocksCount = 0;
                var currentRow = 0;
                for (int i = 0; i <= platform.Count; i++)
                {
                    var nextChar = i == platform.Count ? '#' : platform[i].ElementAt(currentColumn);
                    if (nextChar == 'O')
                    {
                        roundedRocksCount++;
                    }
                    else if(nextChar == '#')
                    {
                        for (int j = currentRow; j < i; j++)
                        {
                            var newString = northTiltedPlatform[j].Remove(currentColumn, 1);
                            if (roundedRocksCount > 0)
                            {
                                newString = newString.Insert(currentColumn, "O");
                                roundedRocksCount--;
                            }
                            else
                            {
                                newString = newString.Insert(currentColumn, ".");
                            }
                            northTiltedPlatform[j] = newString;
                        }

                        currentRow = i + 1;
                    }
                }
            }

            return northTiltedPlatform;
        }

        private int CalculatePlatformLoad(List<string> tiltedPlatform)
        {
            var sum = 0;
            for(int i = 0; i < tiltedPlatform.Count; i++)
            {
                var amountOfRocks = tiltedPlatform[i].Count(c => c == 'O');
                sum += amountOfRocks * (tiltedPlatform.Count - i);
            }

            return sum;
        }
    }
}