using AdventOfCode2023.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Solutions.Day_13
{
    /**
     *  PART 1
     *  1. Parse input strings into a list of strings, allowing for easy operations of rows
     *  2. For every row, determine if it's a vertical reflection
     *      2a. We expect the input to be symmetric. We will search for any symmetry by looking for matches of ".." or "##" in the first row
     *      2b. For every match on the first row, we will split the current row in two on the index where we had our match from step 2a.
     *      2c. If we reverse the second string and it's equal to the first string, we know it's a vertical reflection per definition of symmetry
     *  3. If we went through all of step 2 without finding a vertical reflection, the result must be horizontal.
     *      3a. Since the logic for finding a reflection is equal, we simply need to look at columns instead of rows.
     *      3b. To reduce amount of code, simply transform the matrix counter clockwise and re-use the same logic.
     *  4. Add value of matrix to sum
     *      4a. If vertical reflection, count rows to the left of vertical line (This should be equal to n+1)
     *      4b. If horizontal reflection, count rows above horizontal line (This should be equal to n+1) and multiply the value by 100
     **/
    public class SolutionA : Solution
    {
        protected override string GetSolutionOutput()
        {
            var sumOfAllNotes = GetSumOfAllNotes();
            return sumOfAllNotes.ToString();
        }

        private int GetSumOfAllNotes()
        {
            var terrains = ParseStringInputToTerrainMatrixes();
            var sum = 0;
            foreach (var terrain in terrains)
            {
                sum += CalculateTerrainValue(terrain);
            }
            return sum;
        }

        private List<List<string>> ParseStringInputToTerrainMatrixes()
        {
            List<List<string>> terrains = new List<List<string>>();
            List<string> terrain = new List<string>();
            for (int i = 0; i <= stringInputs.Count; i++)
            {
                var stringInput = i == stringInputs.Count ? "" : stringInputs[i];
                if (string.IsNullOrEmpty(stringInput))
                {
                    terrains.Add(terrain);
                    terrain = new List<string>();
                }
                else
                {
                    terrain.Add(stringInput);
                }
            }

            return terrains;
        }

        private int CalculateTerrainValue(List<string> terrain)
        {
            var verticalReflectionIndex = FindVerticalReflection(terrain);
            if (verticalReflectionIndex < 0)
            {
                var horizontalReflectionIndex = FindHorizontalReflection(terrain);
                if (horizontalReflectionIndex < 0)
                {
                    // Matrix does not have a valid reflection.
                    return 0;
                }
                else
                {
                    return (horizontalReflectionIndex + 1) * 100;
                }
            }

            return verticalReflectionIndex + 1;
        }

        private int FindVerticalReflection(List<string> terrain)
        {
            var firstRow = terrain[0];
            var matches = Regex.Matches(firstRow, "(?=(\\.{2}|##))");
            if (!matches.Any())
            {
                return -1;
            }

            int bestVerticalLineIndex = -1;
            int bestMirrorSize = 0;
            foreach(var match in matches.AsEnumerable())
            {
                var isValid = true;
                var verticalLineIndex = match.Index;
                foreach (var row in terrain)
                {
                    var stringPart1 = row.Substring(0, verticalLineIndex + 1);
                    var stringPart2 = row.Substring(verticalLineIndex + 1);
                    var mirrorSize = Math.Min(stringPart1.Length, stringPart2.Length);
                    stringPart1 = string.Join("", stringPart1.TakeLast(mirrorSize));
                    stringPart2 = string.Join("", stringPart2.Take(mirrorSize).Reverse());
                    if (stringPart1 != stringPart2)
                    {
                        isValid = false;
                        break;
                    }
                }
                
                if (isValid)
                {
                    var validIndex = match.Index + 1;
                    var mirrorSize = Math.Min(validIndex, firstRow.Length - validIndex);
                    if (mirrorSize > bestMirrorSize)
                    {
                        bestVerticalLineIndex = match.Index;
                        bestMirrorSize = mirrorSize;
                    }
                }
            }

            return bestVerticalLineIndex;
        }

        private int FindHorizontalReflection(List<string> terrain)
        {
            // The logic for finding the symmetry should be equal. We just need to rotate the matrix counter clockwise 90 degrees
            List<string> rotatedTerrain = RotateTerrainCounterClockwise(terrain);
            return FindVerticalReflection(rotatedTerrain);
        }

        private List<string> RotateTerrainCounterClockwise(List<string> terrain)
        {
            var rotatedTerrain = new List<string>();
            int rowLength = terrain[0].Length;
            for (int currentColumn = rowLength - 1; currentColumn >= 0; currentColumn--)
            {
                var currentNewRow = "";
                foreach (var row in terrain)
                {
                    currentNewRow += row.ElementAt(currentColumn);
                }

                rotatedTerrain.Add(currentNewRow);
            }

            return rotatedTerrain;
        }
    }
}