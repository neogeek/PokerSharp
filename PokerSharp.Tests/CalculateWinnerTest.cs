using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using NUnit.Framework;

namespace PokerSharp.Tests
{

    public class CalculateWinnerTest
    {

        [Serializable]
        public struct CompareHandsMockData
        {

            public HandsMockData[] compare;

            public CompareHandsMockData(HandsMockData[] compare)
            {
                this.compare = compare;
            }

        }

        [Serializable]
        public struct HandsMockData
        {

            public int winner;

            public Hand[] hands;

            public HandsMockData(int winner, Hand[] hands)
            {
                this.winner = winner;
                this.hands = hands;
            }

        }

        [Serializable]
        public struct Hand
        {

            public List<Card> cards;

            public Hand(List<Card> cards)
            {
                this.cards = cards;
            }

        }

        [Test]
        public void TestSimpleHandsForWinner()
        {

            var player1Hand = new PokerSharp.Hand
            {
                new Card { suit = Card.Suit.Clubs, rank = 5 },
                new Card { suit = Card.Suit.Diamonds, rank = 5 },
                new Card { suit = Card.Suit.Hearts, rank = 5 },
                new Card { suit = Card.Suit.Spades, rank = 5 },
                new Card { suit = Card.Suit.Clubs, rank = 4 }
            };

            var player2Hand = new PokerSharp.Hand
            {
                new Card { suit = Card.Suit.Clubs, rank = 5 },
                new Card { suit = Card.Suit.Diamonds, rank = 5 },
                new Card { suit = Card.Suit.Hearts, rank = 3 },
                new Card { suit = Card.Suit.Spades, rank = 4 },
                new Card { suit = Card.Suit.Clubs, rank = 4 }
            };

            var winner =
                DetermineWinner.CalculateHighestHandRanking(player1Hand, player2Hand);

            Assert.AreEqual(player1Hand, winner);

        }

        [Test]
        public void TestMultipleHandsForWinner()
        {

            var fileContents =
                File.ReadAllText("../../../Mocks/compare.json");

            var compareHandsMockData = JsonConvert.DeserializeObject<CompareHandsMockData>(fileContents);

            foreach (var handsMockData in compareHandsMockData.compare)
            {

                var player1Hand = handsMockData.hands[0].cards;

                var player2Hand = handsMockData.hands[1].cards;

                var winner =
                    DetermineWinner.CalculateHighestHandRanking(player1Hand, player2Hand);

                Assert.AreEqual(handsMockData.winner == 0 ? player1Hand : player2Hand, winner);

            }

        }

    }

}
