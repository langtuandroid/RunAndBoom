using CodeBase.Services.Localization;
using CodeBase.UI.Windows.Common;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Windows.Authorization
{
    public class AuthorizationText : BaseText
    {
        [SerializeField] private TextMeshProUGUI _messageText;
        [SerializeField] private TextMeshProUGUI _applyText;
        [SerializeField] private TextMeshProUGUI _denyText;

        protected override void RuChosen()
        {
            Title.text = LocalizationConstants.AuthorizationTitleRu;
            _messageText.text = LocalizationConstants.AuthorizationMessageRu;
            _applyText.text = LocalizationConstants.ApplyRu;
            _denyText.text = LocalizationConstants.DenyRu;
        }

        protected override void TrChosen()
        {
            Title.text = LocalizationConstants.AuthorizationTitleTr;
            _messageText.text = LocalizationConstants.AuthorizationMessageTr;
            _applyText.text = LocalizationConstants.ApplyTr;
            _denyText.text = LocalizationConstants.DenyTr;
        }

        protected override void EnChosen()
        {
            Title.text = LocalizationConstants.AuthorizationTitleEn;
            _messageText.text = LocalizationConstants.AuthorizationMessageEn;
            _applyText.text = LocalizationConstants.ApplyEn;
            _denyText.text = LocalizationConstants.DenyEn;
        }
    }
}