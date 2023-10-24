using System;
using CodeBase.Data.Progress;

namespace CodeBase.Infrastructure
{
    public interface ISceneLoader
    {
        void Load(SceneId sceneId, Action<SceneId> onLoaded = null);
    }
}