﻿using System;
using CodeBase.Data.Progress;
using CodeBase.Data.Progress.Perks;
using CodeBase.Logic;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Items;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroHealth : MonoBehaviour, IHealth, IProgressSaver
    {
        private const float BaseRatio = 1f;
        private const float BaseArmorRatio = 0f;
        private const float ZeroValue = 0f;

        private IStaticDataService _staticDataService;
        private ProgressData _progressData;
        private PerkItemData _regenerationItemData;
        private PerkItemData _vampirismItemData;
        private PerkItemData _upMaxHealthItemData;
        private PerkItemData _armorItemData;
        private float _regenerationValue;
        private float _regenerationCurrentTime = 1f;
        private float _regenerationDelay = 1f;
        private float _vampirismValue;
        private float _maxHealthRatio = 1.0f;
        private float _armorRatio = 0.0f;

        public float Current { get; private set; }
        public float Max { get; private set; }

        public event Action HealthChanged;
        public event Action HealthDamaged;

        private void OnEnable()
        {
            if (_regenerationItemData != null)
                _regenerationItemData.LevelChanged += ChangeRegeneration;

            if (_vampirismItemData != null)
                _vampirismItemData.LevelChanged += ChangeVampirism;

            if (_upMaxHealthItemData != null)
                _upMaxHealthItemData.LevelChanged += ChangeMaxHealth;

            if (_armorItemData != null)
                _armorItemData.LevelChanged += ChangeArmor;
        }

        private void OnDisable()
        {
            if (_regenerationItemData != null)
                _regenerationItemData.LevelChanged -= ChangeRegeneration;

            if (_vampirismItemData != null)
                _vampirismItemData.LevelChanged -= ChangeVampirism;

            if (_upMaxHealthItemData != null)
                _upMaxHealthItemData.LevelChanged -= ChangeMaxHealth;

            if (_armorItemData != null)
                _armorItemData.LevelChanged -= ChangeArmor;
        }

        private void Update()
        {
            TryRegenerate();
        }

        public void Recover()
        {
            _progressData.HealthState.CurrentHp = Max;
            Current = Max;
            HealthChanged?.Invoke();
        }

        private void TryRegenerate()
        {
            if (NeedRegenerate())
            {
                if (!IsDelaySpent())
                {
                    _regenerationCurrentTime -= Time.deltaTime;
                }
                else
                {
                    _regenerationCurrentTime = _regenerationDelay;
                    IncreaseCurrent(_regenerationValue);
                }
            }
        }

        private bool NeedRegenerate() =>
            _regenerationValue > 0 && Current < Max;

        private bool IsDelaySpent() =>
            _regenerationCurrentTime <= 0f;

        public void Construct(IStaticDataService staticDataService) =>
            _staticDataService = staticDataService;

        public void LoadProgressData(ProgressData progressData)
        {
            _progressData = progressData;
            SetupUpHealth();
            SetupRegeneration();
            SetupVampirism();
            SetupArmor();
        }

        public void UpdateProgressData(ProgressData progressData)
        {
            _progressData.HealthState.CurrentHp = Current;
            _progressData.HealthState.MaxHp = Max;
        }

        private void SetupUpHealth()
        {
            _upMaxHealthItemData =
                _progressData.PerksData.Perks.Find(x => x.PerkTypeId == PerkTypeId.UpMaxHealth);
            _upMaxHealthItemData.LevelChanged += ChangeMaxHealth;

            if (_upMaxHealthItemData.LevelTypeId == LevelTypeId.None)
                _maxHealthRatio = BaseRatio;
            else
                _maxHealthRatio =
                    _staticDataService.ForPerk(PerkTypeId.UpMaxHealth, _upMaxHealthItemData.LevelTypeId).Value;

            Max = _progressData.HealthState.MaxHp;
            Current = _progressData.HealthState.CurrentHp;
            HealthChanged?.Invoke();
        }

        private void SetupVampirism()
        {
            _vampirismItemData =
                _progressData.PerksData.Perks.Find(x => x.PerkTypeId == PerkTypeId.Vampire);
            _vampirismItemData.LevelChanged += ChangeVampirism;
        }

        public void Vampire(float enemyHealth)
        {
            float value = enemyHealth * _vampirismValue;
            IncreaseCurrent(value);
        }

        private void SetupRegeneration()
        {
            _regenerationItemData =
                _progressData.PerksData.Perks.Find(x => x.PerkTypeId == PerkTypeId.Regeneration);
            _regenerationItemData.LevelChanged += ChangeRegeneration;
            ChangeRegeneration();
            _regenerationCurrentTime = _regenerationDelay;
        }

        private void ChangeRegeneration()
        {
            _regenerationItemData =
                _progressData.PerksData.Perks.Find(x => x.PerkTypeId == PerkTypeId.Regeneration);

            if (_regenerationItemData.LevelTypeId == LevelTypeId.None)
                _regenerationValue = ZeroValue;
            else
                _regenerationValue = _staticDataService
                    .ForPerk(PerkTypeId.Regeneration, _regenerationItemData.LevelTypeId)
                    .Value;
        }

        private void ChangeVampirism()
        {
            _vampirismItemData =
                _progressData.PerksData.Perks.Find(x => x.PerkTypeId == PerkTypeId.Vampire);

            if (_vampirismItemData.LevelTypeId == LevelTypeId.None)
                _vampirismValue = ZeroValue;
            else
                _vampirismValue = _staticDataService.ForPerk(PerkTypeId.Vampire, _vampirismItemData.LevelTypeId).Value;
        }

        private void ChangeMaxHealth()
        {
            _upMaxHealthItemData =
                _progressData.PerksData.Perks.Find(x => x.PerkTypeId == PerkTypeId.UpMaxHealth);

            if (_upMaxHealthItemData.LevelTypeId == LevelTypeId.None)
                _maxHealthRatio = BaseRatio;
            else
                _maxHealthRatio =
                    _staticDataService.ForPerk(PerkTypeId.UpMaxHealth, _upMaxHealthItemData.LevelTypeId).Value;

            SetMaxHealth();
            Current = Max;
            _progressData.HealthState.MaxHp = Max;
            _progressData.HealthState.CurrentHp = Current;
            HealthChanged?.Invoke();
        }

        private void SetMaxHealth() =>
            Max = Constants.InitialMaxHp * _maxHealthRatio;

        private void SetupArmor()
        {
            _armorItemData = _progressData.PerksData.Perks.Find(x => x.PerkTypeId == PerkTypeId.Armor);
            _armorItemData.LevelChanged += ChangeArmor;
            ChangeArmor();
        }

        private void ChangeArmor()
        {
            _armorItemData = _progressData.PerksData.Perks.Find(x => x.PerkTypeId == PerkTypeId.Armor);

            if (_armorItemData.LevelTypeId == LevelTypeId.None)
                _armorRatio = BaseArmorRatio;
            else
                _armorRatio = _staticDataService.ForPerk(PerkTypeId.Armor, _armorItemData.LevelTypeId).Value;
        }

        private void IncreaseCurrent(float value)
        {
            if (value <= ZeroValue)
                return;

            float next = Current + value;
            Current = next > Max ? Max : next;
            _progressData.HealthState.CurrentHp = Current;
            HealthChanged?.Invoke();
        }

        public void TakeDamage(float damage)
        {
            float result = (BaseRatio - _armorRatio) * damage;
            Current -= result;
            _progressData.HealthState.CurrentHp = Current;
            HealthChanged?.Invoke();
            HealthDamaged?.Invoke();
        }
    }
}