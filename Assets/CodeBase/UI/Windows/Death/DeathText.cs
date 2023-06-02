using CodeBase.Data.Settings;
using CodeBase.Services;
using CodeBase.Services.Localization;
using CodeBase.UI.Elements;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Death
{
    public class DeathText : BaseText
    {
        [SerializeField] private TextMeshProUGUI _title;

        protected override void RuChosen() => 
            _title.text = LocalizationConstants.DeathTitleRu;

        protected override void TrChosen() => 
            _title.text = LocalizationConstants.DeathTitleTr;

        protected override void EnChosen() => 
            _title.text = LocalizationConstants.DeathTitleEn;
    }
}