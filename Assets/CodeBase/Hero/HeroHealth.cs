using System;
using CodeBase.Data;
using CodeBase.Data.Perks;
using CodeBase.Logic;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Items;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroHealth : MonoBehaviour, IHealth, IHeal, IProgressSaver
    {
        private const int BaseDamageRatio = 1;

        private IStaticDataService _staticDataService;
        private PerkItemData _regenerationItemData;
        private float _regenerationValue;
        private float _regenerationCurrentTime = 1f;
        private float _regenerationDelay = 1f;
        private PerkItemData _vampirismItemData;
        private float _vampirismValue;
        private float _baseMaxHp;
        private float _maxHealthRatio = 1.0f;
        private float _armorRatio = 0.0f;
        private PerkItemData _upMaxHealthItemData;
        private PerkItemData _armorItemData;
        private PlayerProgress _progress;

        public float Current { get; private set; }
        public float Max { get; private set; }

        public event Action HealthChanged;
        public event Action<float> EnemyKilled;

        private void Update()
        {
            TryRegenerate();
        }

        private void TryRegenerate()
        {
            if (NeedRegenerate())
            {
                if (!IsDelaySpent())
                {
                    ChangeCurrent(_regenerationValue);
                    _regenerationCurrentTime -= Time.deltaTime;
                }
                else
                {
                    _regenerationCurrentTime = _regenerationDelay;
                }
            }
        }

        private bool NeedRegenerate() =>
            _regenerationValue > 0 && Current < Max;

        private bool IsDelaySpent() =>
            _regenerationCurrentTime <= 0f;

        public void Construct(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        public void TakeDamage(float damage)
        {
            float result = (BaseDamageRatio - _armorRatio) * damage;
            Current -= result;
            HealthChanged?.Invoke();
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _progress = progress;
            _baseMaxHp = progress.HealthState.BaseMaxHp;
            Current = progress.HealthState.CurrentHp;
            SetupUpMaxHealth();
            SetupRegeneration();
            SetupVampirism();
            SetupArmor();
            Debug.Log($"LoadProgress progress {_progress.GetHashCode()}");
        }

        private void SetupVampirism()
        {
            _vampirismItemData = _progress.PerksData.Perks.Find(x => x.PerkTypeId == PerkTypeId.Vampire);
            _vampirismItemData.LevelChanged += IncreaseVampirismItemData;
            EnemyKilled += Vampire;
        }

        private void Vampire(float enemyHealth)
        {
            float value = enemyHealth * _vampirismValue;
            ChangeCurrent(value);
        }

        private void SetupRegeneration()
        {
            _regenerationItemData =
                _progress.PerksData.Perks.Find(x => x.PerkTypeId == PerkTypeId.Regeneration);
            _regenerationItemData.LevelChanged += IncreaseRegenerationItemData;
            IncreaseRegenerationItemData();
            _regenerationCurrentTime = _regenerationDelay;
        }

        private void IncreaseRegenerationItemData()
        {
            if (_regenerationItemData.LevelTypeId != LevelTypeId.None)
            {
                _regenerationItemData =
                    _progress.PerksData.Perks.Find(x => x.PerkTypeId == PerkTypeId.Regeneration);
                _regenerationValue = _staticDataService
                    .ForPerk(PerkTypeId.Regeneration, _regenerationItemData.LevelTypeId)
                    .Value;
            }
        }

        private void IncreaseVampirismItemData()
        {
            if (_vampirismItemData.LevelTypeId != LevelTypeId.None)
            {
                _vampirismItemData = _progress.PerksData.Perks.Find(x => x.PerkTypeId == PerkTypeId.Vampire);
                _vampirismValue = _staticDataService.ForPerk(PerkTypeId.Vampire, _vampirismItemData.LevelTypeId).Value;
            }
        }

        private void ChangeCurrent(float value)
        {
            float next = Current + value;
            Current = next > Max ? Max : next;
            HealthChanged?.Invoke();
        }

        private void SetupUpMaxHealth()
        {
            _upMaxHealthItemData = _progress.PerksData.Perks.Find(x => x.PerkTypeId == PerkTypeId.UpMaxHealth);
            _upMaxHealthItemData.LevelChanged += MaxChanged;
            MaxChanged();
        }

        private void MaxChanged()
        {
            if (_upMaxHealthItemData.LevelTypeId != LevelTypeId.None)
            {
                _upMaxHealthItemData = _progress.PerksData.Perks.Find(x => x.PerkTypeId == PerkTypeId.UpMaxHealth);
                _maxHealthRatio = _staticDataService.ForPerk(PerkTypeId.UpMaxHealth, _upMaxHealthItemData.LevelTypeId)
                    .Value;
                UpMaxHp(_baseMaxHp * _maxHealthRatio);
            }
        }

        private void SetupArmor()
        {
            _armorItemData = _progress.PerksData.Perks.Find(x => x.PerkTypeId == PerkTypeId.Armor);
            _armorItemData.LevelChanged += ArmorChanged;
            ArmorChanged();
        }

        private void ArmorChanged()
        {
            if (_armorItemData.LevelTypeId != LevelTypeId.None)
            {
                _armorItemData = _progress.PerksData.Perks.Find(x => x.PerkTypeId == PerkTypeId.Armor);
                _armorRatio = _staticDataService.ForPerk(PerkTypeId.Armor, _armorItemData.LevelTypeId).Value;
            }
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.HealthState.ChangeCurrentHP(Current);
            progress.HealthState.ChangeMaxHP(Max);
        }

        public void UpMaxHp(float value)
        {
            Max = value;
            HealthChanged?.Invoke();
        }

        public void Heal()
        {
            Current = Max;
            HealthChanged?.Invoke();
        }
    }
}