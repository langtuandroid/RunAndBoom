using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Services.Randomizer
{
    public class RandomService : IRandomService
    {
        public int Next(int min, int max) =>
            Random.Range(min, max);

        public int NextNumberFrom(HashSet<int> set)
        {
            int i = Next(0, set.Count);

            if (!set.Contains(i))
            {
                set.Add(i);
                return i;
            }

            return NextNumberFrom(set);
        }

        public T NextFrom<T>(List<T> from)
        {
            int i = Next(0, from.Count);
            return from[i];
        }
    }
}