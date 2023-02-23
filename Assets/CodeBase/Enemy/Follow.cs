using UnityEngine;

namespace CodeBase.Enemy
{
    public abstract class Follow : MonoBehaviour
    {
        public abstract void Run();
        public abstract void Stop();
    }
}