using UnityEngine;

namespace CodeBase.Enemy
{
    public abstract class Follow : MonoBehaviour
    {
        public abstract void Move();
        public abstract void Stop();
    }
}