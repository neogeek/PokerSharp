// Copyright (c) Scott Doxey. All Rights Reserved. Licensed under the MIT License. See LICENSE in the project root for license information.

using NUnit.Framework;

namespace PokerSharp.Tests
{

    public class ComputerPlayerAITest
    {

        [Test]
        public void FindHandToPlay()
        {

            var bestCardToPlay = ComputerPlayerAI.FindBestCardToPlay(
                new Hand
                {
                    new Card { suit = Card.Suit.Hearts, rank = 7 },
                    new Card { suit = Card.Suit.Spades, rank = 14 },
                    new Card { suit = Card.Suit.Clubs, rank = 4 }
                },
                new Hand
                {
                    new Card { suit = Card.Suit.Clubs, rank = 14 }, new Card { suit = Card.Suit.Diamonds, rank = 5 }
                });

            Assert.That(bestCardToPlay.suit, Is.EqualTo(Card.Suit.Spades));
            Assert.That(bestCardToPlay.rank, Is.EqualTo(14));

        }

    }

}
