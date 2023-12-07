using AdventOfCode2023.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2023.Solutions.Day_3
{
    /**
     *  PART 2
     *  1. Need a way to handle input string. The magic happens around the symbols.
     *  2. Scan through each line of input. Look for "*".
     *  4. Find the position(s) of the numbers in the current line.
     *  5. If a digit is found, look left and right for other digits until all digits are found.
     *  6. Multiply the numbers together if there are exactly 2 numbers
     *  7. Sum up all the products calculated in step 6
     **/
    public class SolutionB : Solution
    {
        protected override string GetSolutionOutput()
        {
            var sum = GetSumOfGearRatios();
            return sum.ToString();
        }

        private int GetSumOfGearRatios()
        {
            var sum = 0;
            for(int i = 0; i < stringInputs.Count - 1; i++)
            {
                var currentLine = stringInputs[i];
                for (int j = 0; j < currentLine.Length; j++)
                {
                    var currentCharacter = currentLine[j];
                    if (CurrentCharacterIsAnAsterix(currentCharacter))
                    {
                        var valueOfAdjacentNumbers = SumProductOfTwoAdjacentNumbers(i, j);
                        sum += valueOfAdjacentNumbers;
                    }
                }
            }

            return sum;
        }

        private bool CurrentCharacterIsAnAsterix(char currentCharacter)
        {
            return currentCharacter == '*';
        }

        private int SumProductOfTwoAdjacentNumbers(int currentLineIndex, int currentCharacterIndex)
        {
            var currentLine = stringInputs[currentLineIndex];
            var previousLinesNumbers = new List<int>();
            var nextLineNumbers = new List<int>();
            var currentLineNumbers = GetNumbersFromLine(currentLine, currentCharacterIndex);
            if (currentLineIndex > 0)
            {
                var previousLine = stringInputs[currentLineIndex - 1];
                previousLinesNumbers = GetNumbersFromLine(previousLine, currentCharacterIndex);
            }
            if (currentLineIndex < stringInputs.Count - 1)
            {
                var nextLine = stringInputs[currentLineIndex + 1];
                nextLineNumbers = GetNumbersFromLine(nextLine, currentCharacterIndex);
            }

            var allFoundNumbers = previousLinesNumbers
                    .Union(currentLineNumbers)
                    .Union(nextLineNumbers)
                    .ToList();

            if (allFoundNumbers.Count() == 2)
            {
                return allFoundNumbers[0] * allFoundNumbers[1];
            }
            
            return 0;
        }

        private List<int> GetNumbersFromLine(string line, int currentCharacterIndex)
        {
            var numbers = new List<int>();
            var currentDigitLeft = GetDigitStringLeft(line, currentCharacterIndex);
            var currentDigitRight = GetDigitStringRight(line, currentCharacterIndex);
            var currentCharacter = line.ElementAt(currentCharacterIndex);
            if (char.IsDigit(currentCharacter))
            {
                currentDigitLeft += currentCharacter;
                if (!string.IsNullOrEmpty(currentDigitRight))
                {
                    currentDigitLeft += currentDigitRight;
                    currentDigitRight = "";
                }
            }
            var leftValue = GetNumberValueOfString(currentDigitLeft);
            var rightValue = GetNumberValueOfString(currentDigitRight);
            if (leftValue > 0)
            {
                numbers.Add(leftValue);
            }

            if (rightValue > 0)
            {
                numbers.Add(rightValue);
            }

            return numbers;
        }

        private string GetDigitStringLeft(string line, int currentCharacterIndex)
        {
            currentCharacterIndex -= 1;
            var currentCharacter = '.';
            var currentDigit = "";
            if (currentCharacterIndex > 0)
            {
                currentCharacter = line.ElementAt(currentCharacterIndex);
            }

            while (char.IsDigit(currentCharacter) && currentCharacterIndex >= 0)
            {
                currentCharacter = line.ElementAt(currentCharacterIndex);
                if (char.IsDigit(currentCharacter))
                {
                    currentDigit = currentCharacter + currentDigit;
                }
                currentCharacterIndex--;
            }

            return currentDigit;
        }

        private string GetDigitStringRight(string line, int currentCharacterIndex)
        {
            currentCharacterIndex += 1;
            var currentCharacter = line.ElementAt(currentCharacterIndex);
            var currentDigit = "";
            while (char.IsDigit(currentCharacter) && currentCharacterIndex < line.Length)
            {
                currentCharacter = line.ElementAt(currentCharacterIndex);
                if (char.IsDigit(currentCharacter))
                {
                    currentDigit += currentCharacter;
                }
                currentCharacterIndex++;
            }

            return currentDigit;
        }

        private int GetNumberValueOfString(string currentDigit)
        {
            if (!string.IsNullOrEmpty(currentDigit))
            {
                return Int32.Parse(currentDigit);
            }

            return 0;
        }
    }
}