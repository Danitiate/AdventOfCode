using AdventOfCode2023.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2023.Solutions.Day_10
{
    /**
     *  PART 2
     *  
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