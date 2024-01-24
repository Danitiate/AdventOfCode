using System.Linq;

namespace AdventOfCode2023.Solutions.Day_7
{
    public class CamelCardGame
    {
        public string Hand { get; set; } = string.Empty;
        public int Bid { get; set; }
        public bool UsesJokers { get; set; } = false;

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
            var cardStrengthOrder = UsesJokers ? "J23456789TQKA" : "23456789TJQKA";
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
            var jokers = UsesJokers ? Hand.Where(c => c == 'J').Count() : 0;
            var mostCommonCards = Hand
                .Where(character => UsesJokers && character != 'J' || !UsesJokers)
                .GroupBy(character => character)
                .Select(group => group.Count())
                .OrderByDescending(count => count)
                .ToList();

            var mostCommonCardCount = mostCommonCards.FirstOrDefault();
            var secondMostCommonCardCount = mostCommonCards.Skip(1).FirstOrDefault();

            if (IsFiveOfAKind(mostCommonCardCount, jokers))
            {
                return CardHand.FIVE_OF_A_KIND;
            }
            if (IsFourOfAKind(mostCommonCardCount, jokers))
            {
                return CardHand.FOUR_OF_A_KIND;
            }
            if (IsFullHouse(mostCommonCardCount, secondMostCommonCardCount, jokers))
            {
                return CardHand.FULL_HOUSE;
            }
            if (IsThreeOfAKind(mostCommonCardCount, jokers))
            {
                return CardHand.THREE_OF_A_KIND;
            }
            if (IsTwoPairs(mostCommonCardCount, secondMostCommonCardCount))
            {
                return CardHand.TWO_PAIRS;
            }
            if (IsOnePair(mostCommonCardCount, secondMostCommonCardCount, jokers))
            {
                return CardHand.ONE_PAIR;
            }

            return CardHand.HIGH_CARD;
        }

        private bool IsFiveOfAKind(int mostCommonCardCount, int jokers)
        {
            return mostCommonCardCount + jokers == 5;
        }

        private bool IsFourOfAKind(int mostCommonCardCount, int jokers)
        {
            return mostCommonCardCount + jokers == 4;
        }

        private bool IsFullHouse(int mostCommonCardCount, int secondMostCommonCardCount, int jokers)
        {
            return mostCommonCardCount + jokers == 3 && secondMostCommonCardCount == 2;
        }

        private bool IsThreeOfAKind(int mostCommonCardCount, int jokers)
        {
            return mostCommonCardCount + jokers == 3;
        }

        private bool IsTwoPairs(int mostCommonCardCount, int secondMostCommonCardCount)
        {
            return mostCommonCardCount == 2 && secondMostCommonCardCount == 2;
        }

        private bool IsOnePair(int mostCommonCardCount, int secondMostCommonCardCount, int jokers)
        {
            return mostCommonCardCount + jokers == 2 && secondMostCommonCardCount == 1;
        }
    }
}