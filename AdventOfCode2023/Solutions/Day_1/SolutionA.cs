using AdventOfCode.Core.Models;
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
    public class SolutionA : Solution
    {
        protected override string GetSolutionOutput()
        {
            var output1 = SolutionUsingRegex();
            var output2 = SolutionUsingLoops();
            return output1 + "\n" + output2;
        }

        private string SolutionUsingRegex()
        {
            var sum = 0;
            foreach (var stringInput in stringInputs)
            {
                var digit = GetDigitsFromStringUsingRegex(stringInput);
                sum += digit;
            }

            return $"SolutionUsingRegex: {sum}";
        }

        private string SolutionUsingLoops()
        {
            var sum = 0;
            foreach (var stringInput in stringInputs)
            {
                var digit = GetDigitsFromStringUsingLoops(stringInput);
                sum += digit;
            }

            return $"SolutionUsingLoops: {sum}";
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