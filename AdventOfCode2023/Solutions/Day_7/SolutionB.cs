using AdventOfCode.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2023.Solutions.Day_7
{
    /**
    *  PART 2
    *  1. Logic can remain mostly the same as Part 1
    *  2. Parse every line of input into a CamelCardGame
    *  3. A CamelCardGame should consist of a hand (string of length 5) and a bid (int)
    *  4. Insert the CamelCardGame into a list of CamelCardGames such that the list is sorted by hand strength
    *      4a. Need an algorithm to determine which hand is stronger. This will be used in the comparison.
    *      4b. Update the strengths of cards such that J is weaker than 2
    *      4c. Count jokers before determining what kind of CardHand exist. Ignore jokers for two pairs -> If a joker exist that could create this type of hand, there are stronger options available
    *      4d. As our list of CamelCardGames is sorted during insertion, we can do a binary search through the current list to figure out the correct position
    *  5. For each CamelCardGame in the list, calculate its winnings by multiplying its rank (index + 1) with the bid: (index + 1) * bid = winnings
    *  6. Sum up all winnings
    **/
    public class SolutionB : Solution
    {
        protected override string GetSolutionOutput()
        {
            var uniqueWaysToWin = SumCamelCardHandsPlayed();
            return uniqueWaysToWin.ToString();
        }

        private int SumCamelCardHandsPlayed()
        {
            var totalWinnings = 0;
            List<CamelCardGame> camelCardHands = ParseCamelCardGames(stringInputs);
            for (int i = 1; i <= camelCardHands.Count; i++)
            {
                totalWinnings += camelCardHands[i - 1].Bid * i;
            }

            return totalWinnings;
        }

        private List<CamelCardGame> ParseCamelCardGames(List<string> camelCardHands)
        {
            var camelCardGames = new List<CamelCardGame>();
            foreach (var camelCardHand in camelCardHands)
            {
                var camelCardGame = GetCamelCardGameFromString(camelCardHand);
                InsertIntoList(camelCardGames, camelCardGame);
            }

            return camelCardGames;
        }

        private CamelCardGame GetCamelCardGameFromString(string camelCardHand)
        {
            var camelCardGameParts = camelCardHand.Split(" ");
            return new CamelCardGame
            {
                Hand = camelCardGameParts[0],
                Bid = Int32.Parse(camelCardGameParts[1]),
                UsesJokers = true
            };
        }

        private void InsertIntoList(List<CamelCardGame> camelCardGames, CamelCardGame camelCardGame)
        {
            if (camelCardGames.Count == 0)
            {
                camelCardGames.Add(camelCardGame);
                return;
            }

            var left = 0;
            var right = camelCardGames.Count - 1;
            while (left <= right)
            {
                var midPoint = left + (right - left) / 2;
                var isBigger = camelCardGame.BiggerThan(camelCardGames[midPoint]);
                if (isBigger)
                {
                    left = midPoint + 1;
                }
                else
                {
                    right = midPoint - 1;
                }
            }
            camelCardGames.Insert(left, camelCardGame);
        }
    }
}