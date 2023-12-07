using AdventOfCode2023.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Solutions.Day_1
{
    /**
     *  PART 2
     *  1. Same as Part 1
     *  2. We not only need to consider digits, but also strings that represents numbers. Whichever appears first should be returned.
     *      2a. Keep the regular solution, but replace all the input string values with its number value before searching
     *      2b. We can still use a Regex that keeps the matching string values as well as digits. If a digit appears first, return that. If not, return the number representation of the string.
     *      2c. If iterating, we need to spell out the string number and see if it matches. If true, return it. If false, continue iterating.
     *  3. Convert the string values to integer values
     *  4. Append integers to a two digit number
     *  5. Sum values
     * */
    public class SolutionB : Solution
    {
        private readonly Dictionary<string, char> NUMBER_TO_DIGIT_DICTIONARY = new Dictionary<string, char>()
        {
            { "one", '1' },
            { "two", '2' },
            { "three", '3' },
            { "four", '4' },
            { "five", '5' },
            { "six", '6' },
            { "seven", '7' },
            { "eight", '8' },
            { "nine", '9' }
        };

        protected override string GetSolutionOutput()
        {
            var output1 = SolutionUsingReplace();
            var output2 = SolutionUsingRegex();
            var output3 = SolutionUsingLoops();
            return output1 + "\n" + output2 + "\n" + output3;
        }

        private string SolutionUsingReplace()
        {
            var sum = 0;
            foreach (var stringInput in stringInputs)
            {
                var digit = GetDigitsFromStringUsingReplace(stringInput);
                sum += digit;
            }

            return $"SolutionUsingReplace: {sum}";
        }

        private string SolutionUsingRegex()
        {
            var sum = 0;
            foreach (var stringInput in stringInputs)
            {
                var digit = GetDigitsFromStringUsingAdvancedRegex(stringInput);
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

        private int GetDigitsFromStringUsingReplace(string stringInput)
        {
            // Replace values with its initial value before and after in case of overlapping numbers.
            // This will prevent input such as twone to be replaced as tw1, thus removing the wrong string.
            // Instead we get twone1one, giving us 2ne1one.
            stringInput = stringInput
                .Replace("one", "one1one")
                .Replace("two", "two2two")
                .Replace("three", "three3three")
                .Replace("four", "four4four")
                .Replace("five", "five5five")
                .Replace("six", "six6six")
                .Replace("seven", "seven7seven")
                .Replace("eight", "eight8eight")
                .Replace("nine", "nine9nine");

            return GetDigitsFromStringUsingRegex(stringInput);
        }

        private int GetDigitsFromStringUsingRegex(string stringInput)
        {
            stringInput = Regex.Replace(stringInput, "\\D", "");
            var firstDigit = stringInput.First();
            var lastDigit = stringInput.Last();
            var digitsAsString = AppendDigitsToString(firstDigit, lastDigit);
            return Int32.Parse(digitsAsString);
        }

        private int GetDigitsFromStringUsingAdvancedRegex(string stringInput)
        {
            var allMatchesRegex = "((one)|(two)|(three)|(four)|(five)|(six)|(seven)|(eight)|(nine)|[0-9])";
            var firstMatch = Regex.Match(stringInput, allMatchesRegex).Value;
            var lastMatch = Regex.Match(stringInput, allMatchesRegex, RegexOptions.RightToLeft).Value;
            char firstDigit = '_';
            char lastDigit = '_';

            if (!NUMBER_TO_DIGIT_DICTIONARY.TryGetValue(firstMatch, out firstDigit))
            {
                firstDigit = char.Parse(firstMatch);
            }
            if (!NUMBER_TO_DIGIT_DICTIONARY.TryGetValue(lastMatch, out lastDigit))
            {
                lastDigit = char.Parse(lastMatch);
            }

            var digitsAsString = AppendDigitsToString(firstDigit, lastDigit);
            return Int32.Parse(digitsAsString);
        }

        private int GetDigitsFromStringUsingLoops(string stringInput)
        {
            char firstDigit = '_';
            char lastDigit = '_';
            string currentPotentialMatch = "";
            for (int i = 0; i < stringInput.Length; i++)
            {
                char currentChar = stringInput.ElementAt(i);
                if (char.IsDigit(currentChar))
                {
                    firstDigit = currentChar;
                    break;
                }

                currentPotentialMatch += currentChar;
                var potentialMatch = GetPotentialMatch(currentPotentialMatch);
                if (potentialMatch != null)
                {
                    firstDigit = potentialMatch.Value;
                    break;
                }
            }

            currentPotentialMatch = "";
            for (int i = stringInput.Length - 1; i >= 0; i--)
            {
                char currentChar = stringInput.ElementAt(i);
                if (char.IsDigit(currentChar))
                {
                    lastDigit = currentChar;
                    break;
                }

                currentPotentialMatch = currentChar + currentPotentialMatch;
                var potentialMatch = GetPotentialMatch(currentPotentialMatch);
                if (potentialMatch != null)
                {
                    lastDigit = potentialMatch.Value;
                    break;
                }
            }

            var digitsAsString = AppendDigitsToString(firstDigit, lastDigit);
            return Int32.Parse(digitsAsString);
        }

        private char? GetPotentialMatch(string currentPotentialMatch)
        {
            var firstThree = string.Concat(currentPotentialMatch.Take(3));
            var lastThree = string.Concat(currentPotentialMatch.TakeLast(3));
            var firstFour = string.Concat(currentPotentialMatch.Take(4));
            var lastFour = string.Concat(currentPotentialMatch.TakeLast(4));
            var firstFive = string.Concat(currentPotentialMatch.Take(5));
            var lastFive = string.Concat(currentPotentialMatch.TakeLast(5));

            if (NUMBER_TO_DIGIT_DICTIONARY.TryGetValue(firstThree, out var digit1))
            {
                return digit1;
            }
            else if (NUMBER_TO_DIGIT_DICTIONARY.TryGetValue(lastThree, out var digit2))
            {
                return digit2;
            }
            else if (NUMBER_TO_DIGIT_DICTIONARY.TryGetValue(firstFour, out var digit3))
            {
                return digit3;
            }
            else if (NUMBER_TO_DIGIT_DICTIONARY.TryGetValue(lastFour, out var digit4))
            {
                return digit4;
            }
            else if (NUMBER_TO_DIGIT_DICTIONARY.TryGetValue(firstFive, out var digit5))
            {
                return digit5;
            }
            else if (NUMBER_TO_DIGIT_DICTIONARY.TryGetValue(lastFive, out var digit6))
            {
                return digit6;
            }

            return null;
        }

        private string AppendDigitsToString(char firstDigit, char lastDigit)
        {
            return string.Join("", firstDigit, lastDigit);
        }
    }
}