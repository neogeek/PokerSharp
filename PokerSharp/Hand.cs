// Copyright (c) Scott Doxey. All Rights Reserved. Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<Hand> Permutations()
        {

            var results = new List<Hand>();

            var numberOfPossibleCombinations = (int)Math.Pow(2, Count);

            for (var i = 1; i < numberOfPossibleCombinations; i += 1)
            {

                var combination = new Hand();

                for (var j = 0; j < Count; j += 1)
                {

                    if (((i >> j) & 1) == 1)
                    {

                        combination.Add(this[j]);

                    }

                }

                results.Add(combination);

            }

            return results;

        }

        public Hand Shuffle(int seed)
        {

            var random = new Random(seed);

            var shuffledList = new Hand(this.OrderBy(_ => random.Next()));

            return shuffledList;

        }

        public Hand Shuffle()
        {

            var random = new Random();

            var shuffledList = new Hand(this.OrderBy(_ => random.Next()));

            return shuffledList;

        }

    }

}
