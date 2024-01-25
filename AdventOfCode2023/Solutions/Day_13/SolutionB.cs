using AdventOfCode2023.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Solutions.Day_13
{
    /**
     *  PART 2
     *  1. Parse input strings into a list of strings, allowing for easy operations of rows
     *  2. For every row, determine if it's a vertical reflection
     *      2a. We expect the input to be symmetric. We will search for any symmetry by looking for matches of ".." or "##" in the first row
     *      2b. For every match on the first row, we will split the current row in two on the index where we had our match from step 2a.
     *      2c. If we reverse the second string and it's equal to the first string, we know it's a vertical reflection per definition of symmetry
     *  3. If we went through all of step 2 without finding a vertical reflection, the result must be horizontal.
     *      3a. Instead of rotating the matrix, we will instead have custom logic for horizontal reflections
     *      3b. The starting point will have equal rows next to each other
     *      3c. We then collect all matching rows and its indexes next to one another and iterate both directions until the array stops in either direction.
     *  4. After getting the value of the original matrix, we have to run the whole thing again, making sure to only update a single character at the time
     *  5. We also need a check to prevent selecting the same matching row or column reflection again. Once we find a matching result that differs, we return
     *  6. Add value of matrix to sum
     *      6a. If vertical reflection, count rows to the left of vertical line (This should be equal to n+1)
     *      6b. If horizontal reflection, count rows above horizontal line (This should be equal to n+1) and multiply the value by 100
     **/
    public class SolutionB : Solution
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
                sum += CalculateTerrainValueWithoutSmudge(terrain);
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

        private int CalculateTerrainValueWithoutSmudge(List<string> terrain)
        {
            var originalReflectionValue = CalculateTerrainValue(terrain);
            for (int i = 0; i < terrain.Count; i++)
            {
                var row = terrain[i];
                var rowCharArray = row.ToCharArray();
                for (int j = 0; j < rowCharArray.Length; j++)
                {
                    var oldChar = rowCharArray[j];
                    var newChar = oldChar == '.' ? '#' : '.';
                    var terrainCopy = terrain.ToList();
                    rowCharArray[j] = newChar;
                    terrainCopy[i] = new string(rowCharArray);
                    rowCharArray[j] = oldChar;

                    var newReflectionValue = FindHorizontalReflection(terrainCopy, originalReflectionValue);
                    if (newReflectionValue >= 0)
                    {
                        return (newReflectionValue + 1) * 100;
                    }

                    newReflectionValue = FindVerticalReflection(terrainCopy, originalReflectionValue);
                    if (newReflectionValue >= 0)
                    {
                        return newReflectionValue + 1;
                    }
                }
            }

            return originalReflectionValue;
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

        private int FindVerticalReflection(List<string> terrain, int previousIndex = -1)
        {
            var firstRow = terrain[0];
            var matches = Regex.Matches(firstRow, "(?=(\\.{2}|##))");
            if (!matches.Any())
            {
                return -1;
            }

            int bestVerticalLineIndex = -1;
            int bestMirrorSize = 0;
            foreach (var match in matches.AsEnumerable())
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
                    if (mirrorSize > bestMirrorSize && validIndex != previousIndex)
                    {
                        bestVerticalLineIndex = match.Index;
                        bestMirrorSize = mirrorSize;
                    }
                }
            }

            return bestVerticalLineIndex;
        }

        private int FindHorizontalReflection(List<string> terrain, int previousIndex = -1)
        {
            var matches = new Dictionary<int, string>(); 
            for (int i = 1; i < terrain.Count; i++)
            {
                if (terrain[i] == terrain[i - 1])
                {
                    matches.Add(i - 1, terrain[i - 1]);
                }
            }

            if (!matches.Any())
            {
                return -1;
            }

            int bestHorizontalLineIndex = -1;
            int bestMirrorSize = 0;
            foreach (var (index, match) in matches)
            {
                var isValid = true;
                var lowerRowIndex = index - 1;
                var upperRowIndex = index + 2;
                var mirrorSize = 1;
                while (lowerRowIndex >= 0 && upperRowIndex < terrain.Count)
                {
                    mirrorSize++;
                    if (terrain[lowerRowIndex] != terrain[upperRowIndex])
                    {
                        isValid = false;
                        break;
                    }

                    lowerRowIndex--;
                    upperRowIndex++;
                }

                if (isValid)
                {
                    var validIndex = index + 1;
                    if (mirrorSize > bestMirrorSize && validIndex * 100 != previousIndex)
                    {
                        bestHorizontalLineIndex = index;
                        bestMirrorSize = mirrorSize;
                    }
                }
            }

            return bestHorizontalLineIndex;
        }
    }
}