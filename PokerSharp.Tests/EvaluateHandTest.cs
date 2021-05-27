// Copyright (c) Scott Doxey. All Rights Reserved. Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using NUnit.Framework;

namespace PokerSharp.Tests
{

    public class EvaluateHandTest
    {

        [Serializable]
        public struct HandsMockData
        {

            public Hand[] hands;

        }

        [Serializable]
        public struct Hand
        {

            public string name;

            public List<Card> cards;

        }

        [Test]
        public void TestSimpleHand()
        {

            Assert.AreEqual(HandRanking.FourOfAKind,
                new List<Card>
                {
                    new Card { suit = Card.Suit.Clubs, rank = 5 },
                    new Card { suit = Card.Suit.Diamonds, rank = 5 },
                    new Card { suit = Card.Suit.Hearts, rank = 5 },
                    new Card { suit = Card.Suit.Spades, rank = 5 },
                    new Card { suit = Card.Suit.Clubs, rank = 4 }
                }.DetermineHandRankings());

        }

        [Test]
        public void TestAllPossibleHands()
        {
            var fileContents =
                File.ReadAllText("../../../Mocks/hands.json");

            var handsMockData = JsonConvert.DeserializeObject<HandsMockData>(fileContents);

            foreach (var hand in handsMockData.hands)
            {
                Assert.AreEqual(hand.name, hand.cards.DetermineHandRankings().ToString());
            }
        }

        [Test]
        public void TestSortingTwoPair()
        {

            var hand = new PokerSharp.Hand
            {
                new Card { suit = Card.Suit.Hearts, rank = 2 },
                new Card { suit = Card.Suit.Clubs, rank = 5 },
                new Card { suit = Card.Suit.Spades, rank = 2 },
                new Card { suit = Card.Suit.Clubs, rank = 3 },
                new Card { suit = Card.Suit.Diamonds, rank = 5 }
            };

            hand.handRanking = hand.DetermineHandRankings();

            Assert.AreEqual(new List<int>
            {
                5,
                5,
                2,
                2,
                3
            }, hand.SortCardsByRanking(hand.handRanking).Select(card => card.rank));

        }

        [Test]
        public void TestSortingFlush()
        {

            var hand = new PokerSharp.Hand
            {
                new Card { suit = Card.Suit.Hearts, rank = 4 },
                new Card { suit = Card.Suit.Clubs, rank = 3 },
                new Card { suit = Card.Suit.Spades, rank = 6 },
                new Card { suit = Card.Suit.Clubs, rank = 2 },
                new Card { suit = Card.Suit.Diamonds, rank = 5 }
            };

            hand.handRanking = hand.DetermineHandRankings();

            Assert.AreEqual(new List<int>
            {
                2,
                3,
                4,
                5,
                6
            }, hand.SortCardsByRanking(hand.handRanking).Select(card => card.rank));

        }

    }

}
