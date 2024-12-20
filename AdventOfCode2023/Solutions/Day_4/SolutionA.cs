﻿using AdventOfCode.Core.Models;
using System;
using System.Linq;

namespace AdventOfCode2023.Solutions.Day_4
{
    /**
     *  PART 1
     *  1. Seperate string input into two lists: Winning numbers and current numbers
     *  2. Reduce the list of current numbers to contain only numbers in the winning numbers list
     *  3. The value of this list is equal to 2^N-1, where N represent the amount of matching numbers and N >= 1
     *  4. Return the sum of matching number values
     **/
    public class SolutionA : Solution
    {
        protected override string GetSolutionOutput()
        {
            var sum = GetSumOfMatchingNumbers();
            return sum.ToString();
        }

        private int GetSumOfMatchingNumbers()
        {
            var sum = 0;
            foreach (var scratchCard in stringInputs)
            {
                var numbers = scratchCard.Substring(scratchCard.IndexOf(":") + 1).Split("|");
                var winningNumbers = numbers[0].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                var currentNumbers = numbers[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                sum += CalculateScratchCardPoints(winningNumbers, currentNumbers);
            }

            return sum;
        }

        private int CalculateScratchCardPoints(string[] winningNumbers, string[] currentNumbers)
        {
            var matchingNumbers = currentNumbers.Where(n => winningNumbers.Contains(n)).ToList();
            var amountOfMatches = matchingNumbers.Count;
            if (amountOfMatches == 0)
            {
                return 0;
            }

            return (int)Math.Pow(2, amountOfMatches - 1);
        }
    }
}