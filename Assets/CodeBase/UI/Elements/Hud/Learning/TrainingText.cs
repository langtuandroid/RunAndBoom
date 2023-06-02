using CodeBase.Services.Localization;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Elements.Hud.Learning
{
    public class TrainingText : BaseText
    {
        [SerializeField] private TextMeshProUGUI _healthText;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private TextMeshProUGUI _moneyText;
        [SerializeField] private TextMeshProUGUI _openCloseSettingsText;
        [SerializeField] private TextMeshProUGUI _leaveTrainingText;
        [SerializeField] private TextMeshProUGUI _fireText;
        [SerializeField] private TextMeshProUGUI _weapon1Text;
        [SerializeField] private TextMeshProUGUI _weapon2Text;
        [SerializeField] private TextMeshProUGUI _weapon3Text;
        [SerializeField] private TextMeshProUGUI _weapon4Text;

        protected override void RuChosen()
        {
            _healthText.text = LocalizationConstants.TrainingHealthBarRu;
            _levelText.text = LocalizationConstants.TrainingLevelRu;
            _moneyText.text = LocalizationConstants.TrainingMoneyRu;
            _openCloseSettingsText.text = LocalizationConstants.TrainingOpenCloseSettingsRu;
            _leaveTrainingText.text = LocalizationConstants.TrainingLeaveTrainingRu;
            _fireText.text = LocalizationConstants.TrainingFireRu;
            _weapon1Text.text = LocalizationConstants.TrainingWeapon1Ru;
            _weapon2Text.text = LocalizationConstants.TrainingWeapon2Ru;
            _weapon3Text.text = LocalizationConstants.TrainingWeapon3Ru;
            _weapon4Text.text = LocalizationConstants.TrainingWeapon4Ru;
        }

        protected override void TrChosen()
        {
            _healthText.text = LocalizationConstants.TrainingHealthBarTr;
            _levelText.text = LocalizationConstants.TrainingLevelTr;
            _moneyText.text = LocalizationConstants.TrainingMoneyTr;
            _openCloseSettingsText.text = LocalizationConstants.TrainingOpenCloseSettingsTr;
            _leaveTrainingText.text = LocalizationConstants.TrainingLeaveTrainingTr;
            _fireText.text = LocalizationConstants.TrainingFireTr;
            _weapon1Text.text = LocalizationConstants.TrainingWeapon1Tr;
            _weapon2Text.text = LocalizationConstants.TrainingWeapon2Tr;
            _weapon3Text.text = LocalizationConstants.TrainingWeapon3Tr;
            _weapon4Text.text = LocalizationConstants.TrainingWeapon4Tr;
        }

        protected override void EnChosen()
        {
            _healthText.text = LocalizationConstants.TrainingHealthBarEn;
            _levelText.text = LocalizationConstants.TrainingLevelEn;
            _moneyText.text = LocalizationConstants.TrainingMoneyEn;
            _openCloseSettingsText.text = LocalizationConstants.TrainingOpenCloseSettingsEn;
            _leaveTrainingText.text = LocalizationConstants.TrainingLeaveTrainingEn;
            _fireText.text = LocalizationConstants.TrainingFireEn;
            _weapon1Text.text = LocalizationConstants.TrainingWeapon1En;
            _weapon2Text.text = LocalizationConstants.TrainingWeapon2En;
            _weapon3Text.text = LocalizationConstants.TrainingWeapon3En;
            _weapon4Text.text = LocalizationConstants.TrainingWeapon4En;
        }
    }
}