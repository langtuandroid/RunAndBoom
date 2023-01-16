using CodeBase.UI.Services.Windows;
using UnityEngine;

namespace CodeBase.StaticData.Windows
{
    [CreateAssetMenu(fileName = "WindowData", menuName = "StaticData/Window")]
    public class WindowStaticData : ScriptableObject
    {
        public WindowId WindowId;
        public GameObject Prefab;
    }
}