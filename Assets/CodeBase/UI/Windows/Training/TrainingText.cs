using CodeBase.Services.Localization;
using CodeBase.UI.Elements;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Windows.Training
{
    public class TrainingText : BaseText
    {
        [SerializeField] private TextMeshProUGUI _healthText;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private TextMeshProUGUI _moneyText;
        [SerializeField] private TextMeshProUGUI _openCloseSettingsText;
        [SerializeField] private TextMeshProUGUI _leaveTrainingText;
        [SerializeField] private TextMeshProUGUI _shootText;
        [SerializeField] private TextMeshProUGUI _weaponsText;

        protected override void RuChosen()
        {
            if (Application.isMobilePlatform)
            {
                _openCloseSettingsText.text = LocalizationConstants.TrainingOpenCloseSettingsMobileRu;
                _shootText.text = LocalizationConstants.TrainingShootMobileRu;
                _weaponsText.text = LocalizationConstants.TrainingWeaponsMobileRu;
                _leaveTrainingText.text = LocalizationConstants.TrainingLeaveTrainingMobileRu;
            }
            else
            {
                _openCloseSettingsText.text = LocalizationConstants.TrainingOpenCloseSettingsPCRu;
                _shootText.text = LocalizationConstants.TrainingShootPCRu;
                _weaponsText.text = LocalizationConstants.TrainingWeaponsPCRu;
                _leaveTrainingText.text = LocalizationConstants.TrainingLeaveTrainingPCRu;
            }

            _healthText.text = LocalizationConstants.TrainingHealthBarRu;
            _levelText.text = LocalizationConstants.TrainingLevelRu;
            _moneyText.text = LocalizationConstants.TrainingMoneyRu;
        }

        protected override void TrChosen()
        {
            if (Application.isMobilePlatform)
            {
                _openCloseSettingsText.text = LocalizationConstants.TrainingOpenCloseSettingsMobileTr;
                _shootText.text = LocalizationConstants.TrainingShootMobileTr;
                _weaponsText.text = LocalizationConstants.TrainingWeaponsMobileTr;
                _leaveTrainingText.text = LocalizationConstants.TrainingLeaveTrainingMobileTr;
            }
            else
            {
                _openCloseSettingsText.text = LocalizationConstants.TrainingOpenCloseSettingsPCTr;
                _shootText.text = LocalizationConstants.TrainingShootPCTr;
                _weaponsText.text = LocalizationConstants.TrainingWeaponsPCTr;
                _leaveTrainingText.text = LocalizationConstants.TrainingLeaveTrainingPCTr;
            }

            _healthText.text = LocalizationConstants.TrainingHealthBarTr;
            _levelText.text = LocalizationConstants.TrainingLevelTr;
            _moneyText.text = LocalizationConstants.TrainingMoneyTr;
        }

        protected override void EnChosen()
        {
            if (Application.isMobilePlatform)
            {
                _openCloseSettingsText.text = LocalizationConstants.TrainingOpenCloseSettingsMobileEn;
                _shootText.text = LocalizationConstants.TrainingShootMobileEn;
                _weaponsText.text = LocalizationConstants.TrainingWeaponsMobileEn;
                _leaveTrainingText.text = LocalizationConstants.TrainingLeaveTrainingMobileEn;
            }
            else
            {
                _openCloseSettingsText.text = LocalizationConstants.TrainingOpenCloseSettingsPCEn;
                _shootText.text = LocalizationConstants.TrainingShootPCEn;
                _weaponsText.text = LocalizationConstants.TrainingWeaponsPCEn;
                _leaveTrainingText.text = LocalizationConstants.TrainingLeaveTrainingPCEn;
            }

            _healthText.text = LocalizationConstants.TrainingHealthBarEn;
            _levelText.text = LocalizationConstants.TrainingLevelEn;
            _moneyText.text = LocalizationConstants.TrainingMoneyEn;
        }
    }
}