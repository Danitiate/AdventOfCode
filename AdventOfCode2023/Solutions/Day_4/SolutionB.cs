using AdventOfCode2023.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2023.Solutions.Day_4
{
    /**
     *  PART 2
     *  1. Create a dictionary to contain the amount of won card per scratch card
     *  2. Keep track of the total amount of copies for each scratch card
     *  3. Iterate through each copy and the amount won to calculate the amount of scratch cards
     *  4. Sum the value for each iteration
     **/
    public class SolutionB : Solution
    {
        protected override string GetSolutionOutput()
        {
            var sum = GetAmountOfScratchCards();
            return sum.ToString();
        }

        public int GetAmountOfScratchCards()
        {
            var scratchCardsWonAmountDictionary = PopulateScratchCardsDictionary(stringInputs);
            var totalAmountOfCopies = scratchCardsWonAmountDictionary.ToDictionary(kv => kv.Key, kv => 0);
            for (var i = 1; i <= scratchCardsWonAmountDictionary.Count(); i++)
            {
                var amountOfCopies = totalAmountOfCopies[i];
                totalAmountOfCopies[i]++;
                var amountOfScratchCardsWon = scratchCardsWonAmountDictionary[i];
                var maxRange = Math.Min(i + amountOfScratchCardsWon, scratchCardsWonAmountDictionary.Count);
                for (var j = i + 1; j <= maxRange; j++)
                {
                    totalAmountOfCopies[j] += amountOfCopies + 1;
                }
            }

            return totalAmountOfCopies.Sum(kv => kv.Value);
        }

        private Dictionary<int, int> PopulateScratchCardsDictionary(List<string> scratchCards)
        {
            var scratchCardsWonAmountDictionary = new Dictionary<int, int>();
            for (int currentScratchCardNumber = 1; currentScratchCardNumber <= scratchCards.Count; currentScratchCardNumber++)
            {
                var scratchCard = scratchCards[currentScratchCardNumber - 1];
                var numbers = scratchCard.Substring(scratchCard.IndexOf(":") + 1).Split("|");
                var winningNumbers = numbers[0].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                var currentNumbers = numbers[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                var matchingNumbers = GetMatchingNumbers(winningNumbers, currentNumbers);
                var wonScratchCards = scratchCards.GetRange(currentScratchCardNumber, matchingNumbers.Count);
                scratchCardsWonAmountDictionary.Add(currentScratchCardNumber, wonScratchCards.Count);
            }

            return scratchCardsWonAmountDictionary;
        }

        private List<string> GetMatchingNumbers(string[] winningNumbers, string[] currentNumbers)
        {
            return currentNumbers.Where(n => winningNumbers.Contains(n)).ToList();
        }
    }
}


