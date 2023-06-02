using CodeBase.Data;
using CodeBase.Data.Settings;
using CodeBase.Services;
using CodeBase.Services.Localization;
using CodeBase.UI.Elements;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Finish
{
    public class FinishText : BaseText
    {
        [SerializeField] private TextMeshProUGUI _title;

        protected override void RuChosen() => 
            _title.text = LocalizationConstants.FinishTitleRu;

        protected override void TrChosen() => 
            _title.text = LocalizationConstants.FinishTitleTr;

        protected override void EnChosen() => 
            _title.text = LocalizationConstants.FinishTitleEn;
    }
}