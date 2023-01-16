using System;

namespace CodeBase.Data
{
    [Serializable]
    public class ScoreData
    {
        public int Score { get; private set; }

        public Action ScoreChanged;

        public ScoreData()
        {
            Score = 50;
        }

        public void AddScore(int value)
        {
            Score += value;
            ScoreChanged?.Invoke();
        }

        public void ReduceScore(int value)
        {
            if (Score < value)
                return;

            Score -= value;
            ScoreChanged?.Invoke();
        }
    }
}