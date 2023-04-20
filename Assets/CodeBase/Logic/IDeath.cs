using System;

namespace CodeBase.Logic
{
    public interface IDeath
    {
        void Die();
        event Action Died;
    }
}