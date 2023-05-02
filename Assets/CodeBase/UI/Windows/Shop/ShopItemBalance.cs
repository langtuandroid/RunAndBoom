using CodeBase.Data;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;

namespace CodeBase.UI.Windows.Shop
{
    public class ShopItemBalance
    {
        private readonly PlayerProgress _progress;

        public ShopItemBalance() =>
            _progress = AllServices.Container.Single<IPlayerProgressService>().Progress;

        public bool IsMoneyEnough(int value) =>
            _progress.CurrentLevelStats.MoneyData.IsMoneyEnough(value);

        public void ReduceMoney(int value) =>
            _progress.CurrentLevelStats.MoneyData.ReduceMoney(value);
    }
}