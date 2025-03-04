﻿using System;

namespace CodeBase.Data.Progress.Stats
{
    [Serializable]
    public class RestartsData
    {
        public int Count;

        public RestartsData() =>
            Count = (int)Constants.Zero;

        public void Increment() =>
            Count++;
    }
}