using System;
using CodeBase.Data.Stats;

namespace CodeBase.Data
{
    [Serializable]
    public class SceneDataDictionary : SerializableDictionary<Scene, LevelStats>
    {
    }
}