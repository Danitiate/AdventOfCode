using AdventOfCode2023.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2023.Solutions.Day_9
{
    /**
     *  PART 2
     *  1. Logic is mainly the same as part 1, but need some changes for the last steps
     *  2. For every input, collect all the numbers
     *  3. Recursively subtract the numbers xN - xN-1 in every list and add the difference to a new list
     *  4. Continue recursion until you have a list of all zeroes
     *  5. Return the first value of the sequence (this will eventually be 0)
     *  6. Subtract the first value in the list with the value returned in step 5 return the new sum
     *  7. The final sum will be the sum of all extrapolated numbers
     **/
    public class SolutionB : Solution
    {
        protected override string GetSolutionOutput()
        {
            var amountOfStepsRequired = GetSumOfExtrapolatedNumbers();
            return amountOfStepsRequired.ToString();
        }

        private int GetSumOfExtrapolatedNumbers()
        {
            var sum = 0;
            var numbersList = GetNumbersFromInput();
            foreach (var numbers in numbersList)
            {
                sum += GetExtrapolatedNumberFromList(numbers);
            }
            return sum;
        }

        private List<List<int>> GetNumbersFromInput()
        {
            var numbers = new List<List<int>>();
            foreach (var stringInput in stringInputs)
            {
                var numbersString = stringInput.Split(" ");
                numbers.Add(numbersString.Select(n => Int32.Parse(n)).ToList());
            }

            return numbers;
        }

        private int GetExtrapolatedNumberFromList(List<int> numbers)
        {
            if (numbers.Count == 0 || numbers.All(n => n == 0))
            {
                return 0;
            }

            var numbersDifferenceList = GetDifferencesFromNumbers(numbers);
            return numbers.First() - GetExtrapolatedNumberFromList(numbersDifferenceList);
        }

        private List<int> GetDifferencesFromNumbers(List<int> numbers)
        {
            var numbersDifferenceList = new List<int>();
            for (int i = 1; i < numbers.Count; i++)
            {
                numbersDifferenceList.Add(numbers[i] - numbers[i - 1]);
            }

            return numbersDifferenceList;
        }
    }
}