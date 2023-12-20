using UnityEngine;

namespace CodeBase.UI.Windows.Common
{
    public class UIOutlineMover : MonoBehaviour
    {
        [SerializeField] private UIOutline _uiOutline;

        private const string CurrentTimeParameter = "_CurrentTime";

        private void Update()
        {
            _uiOutline.material.SetFloat(CurrentTimeParameter, Time.unscaledTime);
        }
    }
}