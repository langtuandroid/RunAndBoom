using System.Collections;
using Agava.YandexGames;
using CodeBase.Data;
using CodeBase.Services;
using CodeBase.Services.PlayerAuthorization;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows.Common;
using CodeBase.UI.Windows.GameEnd;
using CodeBase.UI.Windows.Gifts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.LeaderBoard
{
    public class LeaderBoardWindow : WindowBase
    {
        [SerializeField] private TextMeshProUGUI _rankText;
        [SerializeField] private RawImage _iconImage;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private GameObject _leaderBoard;
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private GameObject _playerContainer;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _toNextWindowButton;

        private IAuthorization _authorization;
        private Scene _nextScene;
        private int _maxPrice;

        private void Awake()
        {
            _leaderBoard.SetActive(false);
            _rankText.text = "";
            _iconImage.texture = null;
            _nameText.text = "";
            _scoreText.text = "";
            _playerContainer.SetActive(false);
        }

        private void OnEnable()
        {
            ClearLeaderBoard();
            _restartButton.onClick.AddListener(RestartLevel);

            if (Application.isEditor)
                return;

            if (AdsService != null)
            {
                AdsService.OnInitializeSuccess += AdsServiceInitializedSuccess;
                InitializeAdsSDK();
            }

            if (_authorization == null)
                _authorization = AllServices.Container.Single<IAuthorization>();

            _authorization.OnErrorCallback += ShowError;
        }

        private void OnDisable()
        {
            _restartButton.onClick.AddListener(RestartLevel);

            if (AdsService != null)
                AdsService.OnInitializeSuccess -= AdsServiceInitializedSuccess;

            if (_authorization != null)
                _authorization.OnErrorCallback -= ShowError;
        }

        public void Construct(GameObject hero) =>
            base.Construct(hero, WindowId.LeaderBoard);

        public void AddData(Scene nextLevel, int maxPrice)
        {
            _nextScene = nextLevel;
            _maxPrice = maxPrice;

            if (_nextScene == Scene.Initial)
                _toNextWindowButton.onClick.AddListener(ToGameEndWindow);
            else
                _toNextWindowButton.onClick.AddListener(ToGiftsWindow);
        }

        protected override void AdsServiceInitializedSuccess()
        {
            Debug.Log("TryAuthorize");
            if (_authorization.IsAuthorized())
            {
                _authorization.OnAuthorizeSuccessCallback += RequestPersonalProfileDataPermission;
                RequestPersonalProfileDataPermission();
            }
            else
            {
                Authorize();
            }
        }

        private void RequestLeaderBoardData()
        {
            Debug.Log($"RequestLeaderBoardData");
            LeaderBoardService.OnInitializeSuccess -= RequestLeaderBoardData;
            LeaderBoardService.OnSuccessGetEntries += FillLeaderBoard;
            LeaderBoardService.GetEntries(Progress.Stats.CurrentLevelStats.Scene.GetLeaderBoardName());

            LeaderBoardService.OnSuccessGetEntry += FillPlayerInfo;
            LeaderBoardService.GetPlayerEntry(Progress.Stats.CurrentLevelStats.Scene.GetLeaderBoardName());
        }

        private void Authorize()
        {
            Debug.Log("Authorize");
            _authorization.OnAuthorizeSuccessCallback += RequestPersonalProfileDataPermission;
            _authorization.OnErrorCallback += ShowError;
            _authorization.Authorize();
        }

        private void RequestPersonalProfileDataPermission()
        {
            Debug.Log("RequestPersonalProfileDataPermission");
            _authorization.OnRequestPersonalProfileDataPermissionSuccessCallback += InitializeLeaderBoard;
            _authorization.OnAuthorizeSuccessCallback -= RequestPersonalProfileDataPermission;
            _authorization.OnErrorCallback += ShowError;
            _authorization.RequestPersonalProfileDataPermission();
        }

        private void InitializeLeaderBoard()
        {
            Debug.Log("InitializeLeaderBoard");
            _authorization.OnRequestPersonalProfileDataPermissionSuccessCallback -= InitializeLeaderBoard;
            LeaderBoardService.OnInitializeSuccess += RequestLeaderBoardData;

            if (LeaderBoardService.IsInitialized())
                RequestLeaderBoardData();
            else
                StartCoroutine(LeaderBoardService.Initialize());
        }

        private void ShowError(string error)
        {
            Debug.Log($"ServiceAuthorization ShowError {error}");
            _authorization.OnErrorCallback -= ShowError;
        }

        private void ToGiftsWindow()
        {
            WindowBase giftsWindow = WindowService.Show<GiftsWindow>(WindowId.Gifts);
            (giftsWindow as GiftsWindow).AddData(_nextScene);
            GiftsGenerator giftsGenerator = (giftsWindow as GiftsWindow)?.gameObject.GetComponent<GiftsGenerator>();
            WindowService.HideOthers(WindowId.Gifts);
            giftsGenerator?.SetMaxPrice(_maxPrice);
            giftsGenerator?.Generate();
        }

        private void ToGameEndWindow() =>
            WindowService.Show<GameEndWindow>(WindowId.GameEnd);

        private void FillPlayerInfo(LeaderboardEntryResponse response)
        {
            Debug.Log("FillPlayerInfo");
            _rankText.text = $"#{response.rank}";
            StartCoroutine(LoadAvatar(response.player.scopePermissions.avatar, _iconImage, _playerContainer));
            _nameText.text = response.player.publicName;
            _scoreText.text = response.score.ToString();
            LeaderBoardService.OnSuccessGetEntry -= FillPlayerInfo;
        }

        private void FillLeaderBoard(LeaderboardGetEntriesResponse leaderboardGetEntriesResponse)
        {
            Debug.Log("FillLeaderBoard");
            LeaderboardEntryResponse[] leaderboardEntryResponses = leaderboardGetEntriesResponse.entries;

            foreach (var response in leaderboardEntryResponses)
            {
                var player = Instantiate(_playerPrefab, _leaderBoard.transform);
                player.SetActive(false);
                player.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                    response.rank.ToString();
                RawImage image = player.transform.GetChild(1).GetComponent<RawImage>();
                StartCoroutine(LoadAvatar(response.player.scopePermissions.avatar, image, player));
                player.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text =
                    response.player.publicName;
                player.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text =
                    response.score.ToString();
            }

            LeaderBoardService.OnSuccessGetEntries -= FillLeaderBoard;
            _leaderBoard.SetActive(true);
        }

        private void ClearLeaderBoard()
        {
            if (_leaderBoard.transform.childCount > 0)
            {
                foreach (Transform child in _leaderBoard.transform)
                    Destroy(child.gameObject);
            }
        }

        private IEnumerator LoadAvatar(string avatarUrl, RawImage image, GameObject gameObject)
        {
            WWW www = new WWW(avatarUrl);
            yield return null;
            image.texture = www.texture;
            gameObject.SetActive(true);
        }
    }
}