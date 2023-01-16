using System.Collections;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using CodeBase.UI.Services.Windows;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

namespace CodeBase.UI.Windows.Finish
{
    public class FinishWindow : WindowBase
    {
        [SerializeField] private TextMeshProUGUI _scoreText;

        private ISaveLoadService _saveLoadService;

        private const string UpdateResultUrl = Constants.BaseUrl + "ScoreUpdate.php";

        protected override void OnAwake()
        {
            _saveLoadService.SaveProgress();
            base.OnAwake();
        }

        [Inject]
        public void Construct(IPlayerProgressService progressService, ISaveLoadService saveLoadService)
        {
            base.Construct(progressService);
            _saveLoadService = saveLoadService;
        }

        protected override void Initialize()
        {
            RefreshScoreText();
            StartCoroutine(UpdateLevelResult());
        }

        private IEnumerator UpdateLevelResult()
        {
            WWWForm form = new WWWForm();
            form.AddField("score", Progress.CurrentLevelStats.ScoreData.Score);
            UnityWebRequest request = UnityWebRequest.Post(UpdateResultUrl, form);

            using (request)
            {
                UnityWebRequestAsyncOperation unityWebRequestAsyncOperation = request.SendWebRequest();
                yield return unityWebRequestAsyncOperation;

                if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
                    Debug.Log(request.error);
                else
                    Debug.Log("Post Request Complete!");
            }
        }

        private void RefreshScoreText() =>
            _scoreText.text = Progress.CurrentLevelStats.ScoreData.Score.ToString();

        protected override void SubscribeUpdates()
        {
            Progress.CurrentLevelStats.ScoreData.ScoreChanged += RefreshScoreText;
        }

        protected override void CleanUp()
        {
            base.CleanUp();
            Progress.CurrentLevelStats.ScoreData.ScoreChanged -= RefreshScoreText;
        }

        public class Factory : PlaceholderFactory<IWindowService, FinishWindow>
        {
        }
    }
}