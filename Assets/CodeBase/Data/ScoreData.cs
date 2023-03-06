using System;

namespace CodeBase.Data
{
    [Serializable]
    public class ScoreData
    {
        public int Score { get; private set; }

        public event Action ScoreChanged;

        public ScoreData()
        {
            Score = 0;
        }

        public void AddScore(int value)
        {
            Score += value;
            ScoreChanged?.Invoke();
        }

        public void ReduceScore(int value)
        {
            if (Score > value)
            {
                Score -= value;
                ScoreChanged?.Invoke();
            }
        }
    }
}