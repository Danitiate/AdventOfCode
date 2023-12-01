using AdventOfCode2023.Models;
using AdventOfCode2023.Services;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Solutions.Day_1
{
    /**
     *  PART 1
     *  1. We are given a list of string. We need to seperate the strings and attack them one at the time. A loop should do the trick.
     *  2. Given a string, we need to get the first and last digit.
     *     2a. Use a Regex and remove all non-digit characters. Then select first and last digit.
     *     2b. Iterate at the beginning of the string and the end of the string. Select first digits that appear in each search.
     *  3. Then we need to append those digits so that we get a two digit number.
     *  4. Then we need to sum the value of all those numbers.
     **/
    public class SolutionA : ISolution
    {
        private const string FILE_PATH = "Solutions/Day_1/1.in";

        public void Solve()
        {
            var stringInputs = FileReaderService.ReadFile(FILE_PATH);
            var sum = 0;
            foreach (var stringInput in stringInputs)
            {
                var digit = GetTwoDigitNumber(stringInput);
                sum += digit;
            }

            MenuPrinterService.PrintSolution(sum.ToString());
        }

        private int GetTwoDigitNumber(string stringInput)
        {
            return GetDigitsFromStringUsingRegex(stringInput);

            // Alternative:
            //return GetDigitsFromStringUsingLoops(stringInput);
        }

        private int GetDigitsFromStringUsingRegex(string stringInput)
        {
            stringInput = Regex.Replace(stringInput, "\\D", "");
            var firstDigit = stringInput.First();
            var lastDigit = stringInput.Last();
            var digitsAsString = AppendDigitsToString(firstDigit, lastDigit);
            return Int32.Parse(digitsAsString);
        }

        private int GetDigitsFromStringUsingLoops(string stringInput)
        {
            char firstDigit = '_';
            char lastDigit = '_';
            for (int i = 0; i < stringInput.Length; i++)
            {
                if (char.IsDigit(stringInput.ElementAt(i)))
                {
                    firstDigit = stringInput.ElementAt(i);
                    break;
                }
            }

            for (int i = stringInput.Length - 1; i >= 0; i--)
            {
                if (char.IsDigit(stringInput.ElementAt(i)))
                {
                    lastDigit = stringInput.ElementAt(i);
                    break;
                }
            }

            var digitsAsString = AppendDigitsToString(firstDigit, lastDigit);
            return Int32.Parse(digitsAsString);
        }

        private string AppendDigitsToString(char firstDigit, char lastDigit)
        {
            return string.Join("", firstDigit, lastDigit);
        }
    }
}