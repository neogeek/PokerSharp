// Copyright (c) Scott Doxey. All Rights Reserved. Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Linq;

namespace PokerSharp
{

    public enum HandRanking
    {

        None = 0,

        HighCard = 1 << 0,

        OnePair = 1 << 1,

        TwoPair = 1 << 2,

        ThreeOfAKind = 1 << 3,

        Straight = 1 << 4,

        Flush = 1 << 5,

        FullHouse = 1 << 6,

        FourOfAKind = 1 << 7,

        StraightFlush = 1 << 8,

        RoyalFlush = 1 << 9

    }

    public static class EvaluateHand
    {

        public static int GetHighSingleCardRank(this List<Card> cards)
        {

            var sortedCards = cards.SortCards();

            var singleCards = sortedCards.FindPairs().Where(i => i.Value == 1).OrderByDescending(i => i.Key);

            var highestSingleCardRank = singleCards.FirstOrDefault().Key;

            return highestSingleCardRank;

        }

        public static IOrderedEnumerable<KeyValuePair<int, int>> GetHighPairRank(this List<Card> cards)
        {

            var sortedCards = cards.SortCards();

            var pairs = sortedCards.FindPairs().Where(i => i.Value > 1);

            return pairs.OrderBy(i => i.Value);

        }

        public static HandRanking DetermineHandRankings(this List<Card> cards)
        {

            var sortedCards = cards.SortCards();

            var pairs = sortedCards.FindPairs();

            var pairCount = pairs.Count(i => i.Value == 2);

            var onePair = pairCount == 1;
            var twoPairs = pairCount == 2;

            var threeOfAKind = pairs.Count(i => i.Value == 3) == 1;

            var straight =
                (sortedCards.CalculateStraight() || sortedCards.SwapCardRank(14, 1).SortCards().CalculateStraight()) &&
                sortedCards.Count == 5;

            var flush = sortedCards.FindPairs("suit").Count(i => i.Value == 5) == 1;

            var fullHouse = threeOfAKind && onePair;

            var fourOfAKind = pairs.Count(i => i.Value == 4) == 1;

            var straightFlush = straight && flush;

            var royalFlush = straightFlush && sortedCards.First().rank == 14;

            if (royalFlush)
            {
                return HandRanking.RoyalFlush;
            }

            if (straightFlush)
            {
                return HandRanking.StraightFlush;
            }

            if (fourOfAKind)
            {
                return HandRanking.FourOfAKind;
            }

            if (fullHouse)
            {
                return HandRanking.FullHouse;
            }

            if (flush)
            {
                return HandRanking.Flush;
            }

            if (straight)
            {
                return HandRanking.Straight;
            }

            if (threeOfAKind)
            {
                return HandRanking.ThreeOfAKind;
            }

            if (twoPairs)
            {
                return HandRanking.TwoPair;
            }

            if (onePair)
            {
                return HandRanking.OnePair;
            }

            return HandRanking.None;

        }

        public static List<Card> SortCards(this IEnumerable<Card> cards)
        {

            return cards.OrderBy(card => card.rank).Reverse().ToList();

        }

        public static bool CalculateStraight(this List<Card> cards)
        {

            var highestRank = cards.First().rank;

            for (var i = 0; i < cards.Count; i += 1)
            {

                if (cards[i].rank != highestRank - i)
                {

                    return false;

                }

            }

            return true;

        }

        public static IEnumerable<Card> SwapCardRank(this IEnumerable<Card> cards, int oldRank, int newRank)
        {

            var updatedCards = new List<Card>(cards);

            for (var i = 0; i < updatedCards.Count; i += 1)
            {

                if (updatedCards[i].rank == oldRank)
                {

                    updatedCards[i] = new Card { suit = updatedCards[i].suit, rank = newRank };

                }

            }

            return updatedCards;

        }

        public static IEnumerable<KeyValuePair<int, int>> FindPairs(this List<Card> cards, string type = "rank")
        {

            var matches = new Dictionary<int, int>();

            for (var i = 0; i < cards.Count; i += 1)
            {

                var key = 0;

                switch (type)
                {
                    case "rank":
                        key = cards[i].rank;

                        break;
                    case "suit":
                        key = (int)cards[i].suit;

                        break;
                }

                if (!matches.ContainsKey(key))
                {

                    matches.Add(key, 1);

                }
                else
                {

                    matches[key] += 1;

                }

            }

            return matches.ToArray();

        }

    }

}
