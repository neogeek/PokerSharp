using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerSharp
{

    public static class DetermineWinner
    {

        public static Hand CalculateHighestHandRanking(Hand player1Hand, Hand player2Hand)
        {

            player1Hand.handRanking = player1Hand.DetermineHandRankings();
            player2Hand.handRanking = player2Hand.DetermineHandRankings();

            var player1HighSingleCardRank = player1Hand.GetHighSingleCardRank();
            var player2HighSingleCardRank = player2Hand.GetHighSingleCardRank();

            var player1Pairs = player1Hand.GetHighPairRank().ToArray();
            var player2Pairs = player2Hand.GetHighPairRank().ToArray();

            var playerPairsMaxLength = Math.Max(player1Pairs.Length, player2Pairs.Length);

            var player1HighPairRank = 0;
            var player2HighPairRank = 0;

            for (var i = 0; i < playerPairsMaxLength; i += 1)
            {

                if (player1Pairs.Length > i)
                {
                    player1HighPairRank = player1Pairs[i].Key;
                }

                if (player2Pairs.Length > i)
                {
                    player2HighPairRank = player2Pairs[i].Key;
                }

                if (player1HighPairRank != player2HighPairRank)
                {
                    break;
                }

            }

            if (player1Hand.handRanking > player2Hand.handRanking ||
                player1Hand.handRanking == player2Hand.handRanking && player1HighPairRank > player2HighPairRank ||
                player1Hand.handRanking == player2Hand.handRanking && player1HighPairRank == player2HighPairRank &&
                player1HighSingleCardRank > player2HighSingleCardRank)
            {

                return player1Hand;

            }

            if (player2Hand.handRanking > player1Hand.handRanking ||
                player2Hand.handRanking == player1Hand.handRanking && player2HighPairRank > player1HighPairRank ||
                player2Hand.handRanking == player1Hand.handRanking && player2HighPairRank == player1HighPairRank &&
                player2HighSingleCardRank > player1HighSingleCardRank)
            {

                return player2Hand;

            }

            return null;

        }

        public static Hand CalculateHighestHandRanking(List<Card> player1Hand, List<Card> player2Hand)
        {

            return CalculateHighestHandRanking(new Hand(player1Hand), new Hand(player2Hand));

        }

    }

}
