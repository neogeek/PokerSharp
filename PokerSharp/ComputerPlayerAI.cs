// Copyright (c) Scott Doxey. All Rights Reserved. Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Linq;

namespace PokerSharp
{

    public static class ComputerPlayerAI
    {

        public static Card FindBestCardToPlay(Hand cards, Hand river, int maxCardsInHand = 5)
        {

            var handRankingData = FindBestHandsToPlay(cards, river, maxCardsInHand).FirstOrDefault();

            return handRankingData.handCards != null && handRankingData.handCards.Count > 0
                ? handRankingData.handCards.Shuffle().FirstOrDefault()
                : null;

        }

        public static IEnumerable<HandRankingData> FindBestHandsToPlay(Hand hand, Hand river, int maxCardsInHand = 5) =>
            hand.Permutations()
                .Where(item => item.Count <= maxCardsInHand - river.Count)
                .Select(permutationHand => new HandRankingData
                {
                    riverCards = river,
                    handCards = permutationHand,
                    handRanking = new List<Card>(river).Concat(permutationHand).ToList().DetermineHandRankings()
                })
                .Where(handRankingData => handRankingData.handRanking > river.DetermineHandRankings())
                .OrderByDescending(handRankingData => handRankingData.handRanking)
                .ThenBy(handRankingData => handRankingData.handCards.Count)
                .ToList();

        public struct HandRankingData
        {

            public Hand riverCards;

            public Hand handCards;

            public HandRanking handRanking;

        }

    }

}
