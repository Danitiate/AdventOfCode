using AdventOfCode2023.Models;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Solutions.Day_3
{
    /**
     *  PART 1
     *  1. Need a way to handle input string. The magic happens around the symbols.
     *  2. Scan through each line of input.
     *  3. Keep track of the current and next line in input.
     *  4. Find the position(s) of the symbol(s) in the current line.
     *  5. When a symbol position is found, look at the surrounding area for any digits.
     *  6. If a digit is found, look left and right for other digits until all digits are found.
     *  7. Sum up this value to total
     *  8. Mark this digit as "used" by replacing the digits with "."
     **/
    public class SolutionA : Solution
    {
        protected override string GetSolutionOutput()
        {
            var sum = GetSumOfNumbersAdjacentToSymbols();
            return sum.ToString();
        }

        private int GetSumOfNumbersAdjacentToSymbols()
        {
            var sum = 0;
            for(int i = 0; i < stringInputs.Count - 1; i++)
            {
                var currentLine = stringInputs[i];
                var nextLine = stringInputs[i+1];
                for (int j = 0; j < currentLine.Length; j++)
                {
                    var currentCharacter = currentLine[j];
                    if (CurrentCharacterIsASymbol(currentCharacter))
                    {
                        var valueOfAdjacentNumbers = SumAdjacentNumbers(i, j);
                        sum += valueOfAdjacentNumbers;
                    }
                }

                for (int j = 0; j < nextLine.Length; j++)
                {
                    var currentCharacter = nextLine[j];
                    if (CurrentCharacterIsASymbol(currentCharacter))
                    {
                        var valueOfAdjacentNumbers = SumAdjacentNumbers(i, j);
                        sum += valueOfAdjacentNumbers;
                    }
                }
            }

            return sum;
        }

        private bool CurrentCharacterIsASymbol(char currentCharacter)
        {
            return Regex.IsMatch(currentCharacter.ToString(), "[^.\\d\\w]");
        }

        private int SumAdjacentNumbers(int currentLineIndex, int currentCharacterIndex)
        {
            var sum = 0;
            var currentLine = stringInputs[currentLineIndex];
            var nextLine = stringInputs[currentLineIndex + 1];
            sum += SumLine(currentCharacterIndex, currentLine, out var newCurrentLine);
            sum += SumLine(currentCharacterIndex, nextLine, out var newNextLine);
            stringInputs[currentLineIndex] = newCurrentLine;
            stringInputs[currentLineIndex + 1] = newNextLine;
            return sum;
        }

        private int SumLine(int currentCharacterIndex, string currentLine, out string newLine)
        {
            newLine = currentLine;
            var sum = 0;
            var currentDigitLeft = GetDigitStringLeft(newLine, currentCharacterIndex, out newLine);
            var currentDigitRight = GetDigitStringRight(newLine, currentCharacterIndex, out newLine);
            var currentCharacter = currentLine.ElementAt(currentCharacterIndex);
            if (char.IsDigit(currentCharacter))
            {
                newLine = newLine.Remove(currentCharacterIndex, 1)
                                 .Insert(currentCharacterIndex, ".");
                currentDigitLeft += currentCharacter;
                if (!string.IsNullOrEmpty(currentDigitRight))
                {
                    currentDigitLeft += currentDigitRight;
                    currentDigitRight = "";
                }
            }
            sum += GetNumberValueOfString(currentDigitLeft);
            sum += GetNumberValueOfString(currentDigitRight);
            return sum;
        }

        private string GetDigitStringLeft(string line, int currentCharacterIndex, out string newLine)
        {
            newLine = line;
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
                    newLine = newLine.Remove(currentCharacterIndex, 1)
                                     .Insert(currentCharacterIndex, ".");
                }
                currentCharacterIndex--;
            }

            return currentDigit;
        }

        private string GetDigitStringRight(string line, int currentCharacterIndex, out string newLine)
        {
            newLine = line;
            currentCharacterIndex += 1;
            var currentCharacter = line.ElementAt(currentCharacterIndex);
            var currentDigit = "";
            while (char.IsDigit(currentCharacter) && currentCharacterIndex < line.Length)
            {
                currentCharacter = line.ElementAt(currentCharacterIndex);
                if (char.IsDigit(currentCharacter))
                {
                    currentDigit += currentCharacter;
                    newLine = newLine.Remove(currentCharacterIndex, 1)
                                     .Insert(currentCharacterIndex, ".");
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