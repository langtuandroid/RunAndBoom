using CodeBase.Data.Progress;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;

namespace CodeBase.UI.Windows.Gifts
{
    public class GiftsItemBalance
    {
        private ProgressData _progressData;

        public GiftsItemBalance() =>
            _progressData = AllServices.Container.Single<IPlayerProgressService>().ProgressData;

        public void AddMoney(int value) =>
            _progressData.AllStats.AddMoney(value);
    }
}