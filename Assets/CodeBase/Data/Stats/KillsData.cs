﻿using System;

namespace CodeBase.Data.Stats
{
    [Serializable]
    public class KillsData
    {
        public int KilledEnemies;
        public int TotalEnemies;

        public KillsData(int totalEnemies)
        {
            TotalEnemies = totalEnemies;
            Clear();
        }

        public void Increment() =>
            KilledEnemies++;

        public void Clear() =>
            KilledEnemies = (int)Constants.Zero;

        public bool IsTotalKilled() =>
            KilledEnemies == TotalEnemies;

        public float GetRatio() =>
            KilledEnemies / (float)TotalEnemies;
    }
}