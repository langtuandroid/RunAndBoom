using System;

namespace CodeBase.Infrastructure
{
    public interface ILoadingCurtain
    {
        event Action FadedOut;
        void Show();
        void Hide();
    }
}