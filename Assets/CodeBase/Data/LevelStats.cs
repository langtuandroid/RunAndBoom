using System;

namespace CodeBase.Data
{
    [Serializable]
    public class LevelStats
    {
        public string Name { get; private set; }
        public int KillCount { get; private set; }
        public int MaxCombo { get; private set; }
        public int CurrentCombo { get; private set; }

        public ScoreData ScoreData { get; private set; }

        public LevelStats()
        {
            KillCount = 0;
            MaxCombo = 0;
            CurrentCombo = 0;
            ScoreData = new ScoreData();
        }

        public void SetMaxCombo()
        {
            if (CurrentCombo < MaxCombo)
                return;

            MaxCombo = CurrentCombo;
        }
    }
}