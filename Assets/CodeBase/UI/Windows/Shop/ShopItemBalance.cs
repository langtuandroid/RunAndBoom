using CodeBase.Data.Progress;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;

namespace CodeBase.UI.Windows.Shop
{
    public class ShopItemBalance
    {
        private readonly ProgressData _progressData;

        public ShopItemBalance() =>
            _progressData = AllServices.Container.Single<IPlayerProgressService>().ProgressData;

        public bool IsMoneyEnough(int value) =>
            _progressData.AllStats.IsMoneyEnough(value);

        public void ReduceMoney(int value) =>
            _progressData.AllStats.ReduceMoney(value);
    }
}