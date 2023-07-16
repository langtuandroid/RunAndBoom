using UnityEngine;

namespace CodeBase.UI
{
    public class OrientationSetter : MonoBehaviour
    {
        [SerializeField] private Orientation ScreenOrientation;

        private Orientation _currentOrientation;

        private void Awake()
        {
            GetCurrentOrientation();
        }

        private void GetCurrentOrientation()
        {
            switch (Input.deviceOrientation)
            {
                case DeviceOrientation.Portrait:
                    _currentOrientation = Orientation.PortraitFixed;
                    break;
                case DeviceOrientation.PortraitUpsideDown:
                    _currentOrientation = Orientation.Portrait;
                    break;
                case DeviceOrientation.LandscapeLeft:
                    _currentOrientation = Orientation.LandscapeFixed;
                    break;
                case DeviceOrientation.LandscapeRight:
                    _currentOrientation = Orientation.Landscape;
                    break;
            }
        }

        private void Start()
        {
            SetOrientationRules();

            if (_currentOrientation != ScreenOrientation)
                ChangeOrientation();

            Destroy(gameObject);
        }

        private void ChangeOrientation()
        {
            switch (ScreenOrientation)
            {
                case Orientation.Any: break;
                case Orientation.LandscapeFixed:
                    Screen.orientation = UnityEngine.ScreenOrientation.LandscapeLeft;
                    break;
                case Orientation.Landscape:
                    Screen.orientation = UnityEngine.ScreenOrientation.LandscapeLeft;
                    break;
                case Orientation.PortraitFixed:
                    Screen.orientation = UnityEngine.ScreenOrientation.Portrait;
                    break;
                case Orientation.Portrait:
                    Screen.orientation = UnityEngine.ScreenOrientation.Portrait;
                    break;
            }
        }

        private void SetOrientationRules()
        {
            switch (ScreenOrientation)
            {
                case Orientation.Any:
                    Screen.orientation = UnityEngine.ScreenOrientation.AutoRotation;

                    Screen.autorotateToPortrait = Screen.autorotateToPortraitUpsideDown = true;
                    Screen.autorotateToLandscapeLeft = Screen.autorotateToLandscapeRight = true;
                    break;

                case Orientation.Portrait:
                    // Force screen to orient right, then switch to Auto
                    Screen.orientation = UnityEngine.ScreenOrientation.Portrait;
                    Screen.orientation = UnityEngine.ScreenOrientation.AutoRotation;

                    Screen.autorotateToPortrait = Screen.autorotateToPortraitUpsideDown = true;
                    Screen.autorotateToLandscapeLeft =
                        Screen.autorotateToLandscapeRight = false;
                    break;

                case Orientation.PortraitFixed:
                    Screen.orientation = UnityEngine.ScreenOrientation.Portrait;
                    break;

                case Orientation.Landscape:
                    // Force screen to orient right, then switch to Auto
                    Screen.orientation = UnityEngine.ScreenOrientation.LandscapeLeft;
                    Screen.orientation = UnityEngine.ScreenOrientation.AutoRotation;

                    Screen.autorotateToPortrait = Screen.autorotateToPortraitUpsideDown = false;
                    Screen.autorotateToLandscapeLeft = Screen.autorotateToLandscapeRight = true;
                    break;

                case Orientation.LandscapeFixed:
                    Screen.orientation = UnityEngine.ScreenOrientation.LandscapeLeft;
                    break;
            }
        }

        private enum Orientation
        {
            Any,
            Portrait,
            PortraitFixed,
            Landscape,
            LandscapeFixed
        }
    }
}