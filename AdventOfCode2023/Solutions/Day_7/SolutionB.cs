using AdventOfCode2023.Models;
using AdventOfCode2023.Services;
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
    public class SolutionB : ISolution
    {
        private const string FILE_PATH = "Solutions/Day_7/7.in";

        public void Solve()
        {
            var stringInputs = FileReaderService.ReadFile(FILE_PATH);
            var uniqueWaysToWin = SumCamelCardHandsPlayed(stringInputs);
            MenuPrinterService.PrintSolution(uniqueWaysToWin.ToString());
        }

        private int SumCamelCardHandsPlayed(List<string> stringInputs)
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
                Bid = Int32.Parse(camelCardGameParts[1])
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

        internal class CamelCardGame
        {
            public string Hand { get; set; }
            public int Bid { get; set; }

            public bool BiggerThan(CamelCardGame camelCardGame)
            {
                var currentHand = this.GetCardHand();
                var compareHand = camelCardGame.GetCardHand();
                if (currentHand > compareHand)
                {
                    return true;
                }
                else if (currentHand < compareHand)
                {
                    return false;
                }
                return CompareHand(camelCardGame);
            }

            private bool CompareHand(CamelCardGame camelCardGame)
            {
                var cardStrengthOrder = "J23456789TQKA";
                for (int i = 0; i < Hand.Length; i++)
                {
                    var firstCharValue = cardStrengthOrder.IndexOf(Hand[i]);
                    var secondCharValue = cardStrengthOrder.IndexOf(camelCardGame.Hand[i]);
                    if (firstCharValue < secondCharValue)
                    {
                        return false;
                    }
                    else if (firstCharValue > secondCharValue)
                    {
                        return true;
                    }
                }
                return false;
            }

            private CardHand GetCardHand()
            {
                if (IsFiveOfAKind())
                {
                    return CardHand.FIVE_OF_A_KIND;
                }
                if (IsFourOfAKind())
                {
                    return CardHand.FOUR_OF_A_KIND;
                }
                if (IsFullHouse())
                {
                    return CardHand.FULL_HOUSE;
                }
                if (IsThreeOfAKind())
                {
                    return CardHand.THREE_OF_A_KIND;
                }
                if (IsTwoPairs())
                {
                    return CardHand.TWO_PAIRS;
                }
                if (IsOnePair())
                {
                    return CardHand.ONE_PAIR;
                }

                return CardHand.HIGH_CARD;
            }

            private bool IsFiveOfAKind()
            {
                var charCount = Hand
                    .Where(c => c != 'J')
                    .GroupBy(c => c)
                    .Select(g => new { Char = g.Key, Count = g.Count() })
                    .OrderByDescending(x => x.Count)
                    .ToList();

                var jokers = Hand.Where(c => c == 'J').Count();
                return jokers == 5 || charCount[0].Count == 5 || charCount[0].Count + jokers == 5;
            }

            private bool IsFourOfAKind()
            {
                var charCount = Hand
                    .Where(c => c != 'J')
                    .GroupBy(c => c)
                    .Select(g => new { Char = g.Key, Count = g.Count() })
                    .OrderByDescending(x => x.Count)
                    .ToList();

                var jokers = Hand.Where(c => c == 'J').Count();
                return charCount[0].Count == 4 || charCount[0].Count + jokers == 4;
            }

            private bool IsFullHouse()
            {
                var charCount = Hand
                    .Where(c => c != 'J')
                    .GroupBy(c => c)
                    .Select(g => new { Char = g.Key, Count = g.Count() })
                    .OrderByDescending(x => x.Count)
                    .ToList();

                var jokers = Hand.Where(c => c == 'J').Count();
                return charCount[0].Count == 3 && charCount[1].Count == 2 || charCount[0].Count + jokers == 3 && charCount[1].Count == 2;
            }

            private bool IsThreeOfAKind()
            {
                var charCount = Hand
                    .Where(c => c != 'J')
                    .GroupBy(c => c)
                    .Select(g => new { Char = g.Key, Count = g.Count() })
                    .OrderByDescending(x => x.Count)
                    .First();

                var jokers = Hand.Where(c => c == 'J').Count();
                return charCount.Count == 3 || charCount.Count + jokers == 3;
            }

            private bool IsTwoPairs()
            {
                var charCount = Hand
                    .GroupBy(c => c)
                    .Select(g => new { Char = g.Key, Count = g.Count() })
                    .OrderByDescending(x => x.Count)
                    .ToList();

                return charCount[0].Count == 2 && charCount[1].Count == 2;
            }

            private bool IsOnePair()
            {
                var charCount = Hand
                    .Where(c => c != 'J')
                    .GroupBy(c => c)
                    .Select(g => new { Char = g.Key, Count = g.Count() })
                    .OrderByDescending(x => x.Count)
                    .ToList();

                var jokers = Hand.Where(c => c == 'J').Count();
                return charCount[0].Count == 2 && charCount[1].Count == 1 || charCount[0].Count + jokers == 2;
            }
        }

        enum CardHand
        {
            HIGH_CARD = 0,
            ONE_PAIR = 1,
            TWO_PAIRS = 2,
            THREE_OF_A_KIND = 3,
            FULL_HOUSE = 4,
            FOUR_OF_A_KIND = 5,
            FIVE_OF_A_KIND = 6
        }
    }
}