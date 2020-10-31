using System.Collections.Generic;

namespace PokerSharp
{

    public class Hand : List<Card>
    {

        public HandRanking handRanking = HandRanking.None;

        public Hand()
        {

        }

        public Hand(IEnumerable<Card> cards)
        {

            AddRange(cards);

        }

    }

}
