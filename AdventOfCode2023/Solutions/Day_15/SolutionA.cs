using AdventOfCode2023.Models;
using System;
using System.Collections.Generic;

namespace AdventOfCode2023.Solutions.Day_15
{
    /**
     *  PART 1
     *  1. Input is a single line of comma seperated strings. Parse input into a list of strings using string.split(",")
     *  2. For each string in the list created in step 1, apply the hash function on every character and sum its value:
     *      a. Set the current value to 0
            b. Add the ASCII value of the character to the current value
            c. Multiply the current value by 17
            d. Set the current value to the remainder of currentValue / 256 (Use the modulo operator)
            e. Continue looping through every character in the current string
        3. Once the hash function is complete, sum its value with the result of the hash function to all other strings
     **/
    public class SolutionA : Solution
    {
        protected override string GetSolutionOutput()
        {
            var hashSum = GetSumOfHashAlgorithm();
            return hashSum.ToString();
        }

        private int GetSumOfHashAlgorithm()
        {
            var strings = ParseInputIntoStrings();
            var sum = 0;
            foreach(var s in strings) 
            {
                sum += GetHashValueOfString(s);
            }
            return sum;
        }

        private List<string> ParseInputIntoStrings()
        {
            var allStrings = stringInputs[0];
            return new List<string>(allStrings.Split(','));
        }

        private int GetHashValueOfString(string currentString)
        {
            var hashValue = 0;
            foreach(var character in currentString)
            {
                hashValue += (int)character;
                hashValue *= 17;
                hashValue %= 256;
            }

            return hashValue;
        }
    }
}