using System;

namespace CodeBase.Data.Progress.Stats
{
    [Serializable]
    public class SceneDataDictionary : SerializableDictionary<SceneId, LevelStats>
    {
    }
}