using System;

namespace CodeBase.Infrastructure
{
    public interface ISceneLoader
    {
        void Load(string name, Action<string> onLoaded = null);
    }
}