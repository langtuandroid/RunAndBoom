using CodeBase.Data;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;

namespace CodeBase.UI.Windows.Finish
{
    public class GiftsItemBalance
    {
        private PlayerProgress _progress;

        public GiftsItemBalance() =>
            _progress = AllServices.Container.Single<IPlayerProgressService>().Progress;

        public void AddMoney(int value) =>
            _progress.CurrentLevelStats.MoneyData.AddMoney(value);
    }
}