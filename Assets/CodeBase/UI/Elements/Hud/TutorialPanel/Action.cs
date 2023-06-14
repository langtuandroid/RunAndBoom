using System;
using CodeBase.UI.Services;
using UnityEditor.TextCore.Text;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements.Hud.TutorialPanel
{
    public class Action : MonoBehaviour
    {
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _image.ChangeImageAlpha(Constants.AlphaTutorialItem);
        }
    }
}