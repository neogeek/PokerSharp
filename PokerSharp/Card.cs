using System;

namespace PokerSharp
{

    [Serializable]
    public class Card
    {

        [Flags]
        public enum Suit
        {

            Clubs = 1 << 0,

            Diamonds = 1 << 1,

            Hearts = 1 << 2,

            Spades = 1 << 3

        }

        public int rank;

        public Suit suit;

        public override string ToString()
        {

            var suitLabel = suit.ToString();
            var rankLabel = rank.ToString();

            switch (suit)
            {
                case Suit.Clubs:
                    suitLabel = "♣";

                    break;
                case Suit.Diamonds:
                    suitLabel = "♦";

                    break;
                case Suit.Hearts:
                    suitLabel = "♥";

                    break;
                case Suit.Spades:
                    suitLabel = "♠";

                    break;
            }

            switch (rank)
            {
                case 11:
                    rankLabel = "J";

                    break;
                case 12:
                    rankLabel = "Q";

                    break;
                case 13:
                    rankLabel = "K";

                    break;
                case 14:
                    rankLabel = "A";

                    break;
            }

            return $"{suitLabel} {rankLabel}";

        }

    }

}
