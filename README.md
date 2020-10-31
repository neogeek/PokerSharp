# PokerSharp

> PokerSharp is a small poker hand evaluation library.

[![Build Status](https://travis-ci.com/neogeek/PokerSharp.svg?branch=master)](https://travis-ci.com/neogeek/PokerSharp)

## Install

Download latest [release](https://github.com/neogeek/poker-sharp/releases).

## Usage

### EvaluateHand.DetermineHandRankings

```csharp
const hand = new List<Card>
{
    new Card { suit = Card.Suit.Clubs, rank = 5 },
    new Card { suit = Card.Suit.Diamonds, rank = 5 },
    new Card { suit = Card.Suit.Hearts, rank = 5 },
    new Card { suit = Card.Suit.Spades, rank = 5 },
    new Card { suit = Card.Suit.Clubs, rank = 4 }
};

Debug.WriteLine(hand.DetermineHandRankings()) // FullHouse
```

### DetermineWinner.CalculateHighestHandRanking

```csharp
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
```
