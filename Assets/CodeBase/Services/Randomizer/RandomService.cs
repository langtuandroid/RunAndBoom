using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Services.Randomizer
{
    public class RandomService : IRandomService
    {
        public int Next(int min, int max) =>
            Random.Range(min, max);

        public int NextNumberFrom(HashSet<int> set)
        {
            int i = Next(set.Min(), set.Count);

            if (!set.Contains(i))
                return NextNumberFrom(set);

            set.Remove(i);
            return i;
        }

        public T NextFrom<T>(List<T> from)
        {
            int i = Next(0, from.Count);
            return from[i];
        }
    }
}