using System.Collections.Generic;

namespace CodeBase.Services.Randomizer
{
    public interface IRandomService : IService
    {
        int Next(int min, int max);
        int NextNumberFrom(HashSet<int> set);
        T NextFrom<T>(List<T> from);
    }
}