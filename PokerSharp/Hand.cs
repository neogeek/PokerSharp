// Copyright (c) Scott Doxey. All Rights Reserved. Licensed under the MIT License. See LICENSE in the project root for license information.

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
