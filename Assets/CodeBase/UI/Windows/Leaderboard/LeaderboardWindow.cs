using System.Collections;
using Agava.YandexGames;
using CodeBase.Data;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows.Common;
using CodeBase.UI.Windows.GameEnd;
using CodeBase.UI.Windows.Gifts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Leaderboard
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

            if (LeaderboardService == null)
                return;

            LeaderboardService.OnInitializeSuccess += RequestLeaderBoardData;
            InitializeLeaderboardSDK();
        }

        private void Start() =>
            ClearLeaderBoard();

        private void InitializeLeaderboardSDK()
        {
            if (IsLeaderboardInitialized())
                RequestLeaderBoardData();
            else
                StartCoroutine(CoroutineInitializeLeaderboardSDK());
        }

        private void OnEnable()
        {
            AddNextWindowListener();
            _restartButton.onClick.AddListener(RestartLevel);

            if (LeaderboardService != null)
                LeaderboardService.OnInitializeSuccess += RequestLeaderBoardData;
        }

        private void OnDisable()
        {
            RemoveNextWindowListener();
            _restartButton.onClick.AddListener(RestartLevel);

            if (LeaderboardService != null)
                LeaderboardService.OnInitializeSuccess -= RequestLeaderBoardData;
        }

        public void Construct(GameObject hero) =>
            base.Construct(hero, WindowId.LeaderBoard);

        public void AddData(Scene nextLevel, int maxPrice)
        {
            _nextScene = nextLevel;
            _maxPrice = maxPrice;
        }

        private bool IsLeaderboardInitialized() =>
            LeaderboardService.IsInitialized();

        private IEnumerator CoroutineInitializeLeaderboardSDK()
        {
            yield return LeaderboardService.Initialize();
        }

        private void AddNextWindowListener()
        {
            if (_nextScene == Scene.Initial)
                _toNextWindowButton.onClick.AddListener(ToGameEndWindow);
            else
                _toNextWindowButton.onClick.AddListener(ToGiftsWindow);
        }

        private void RemoveNextWindowListener()
        {
            if (_nextScene == Scene.Initial)
                _toNextWindowButton.onClick.RemoveListener(ToGameEndWindow);
            else
                _toNextWindowButton.onClick.RemoveListener(ToGiftsWindow);
        }

        private void ToGiftsWindow()
        {
            WindowBase giftsWindow = WindowService.Show<GiftsWindow>(WindowId.Gifts);
            GiftsGenerator giftsGenerator = (giftsWindow as GiftsWindow)?.gameObject.GetComponent<GiftsGenerator>();
            giftsGenerator?.SetMaxPrice(_maxPrice);
            giftsGenerator?.Generate();
        }

        private void ToGameEndWindow() =>
            WindowService.Show<GameEndWindow>(WindowId.GameEnd);

        private void RequestLeaderBoardData()
        {
            LeaderboardService.OnSuccessGetEntries += FillLeaderBoard;
            LeaderboardService.GetEntries(Progress.Stats.CurrentLevelStats.Scene.GetLeaderBoardName());

            LeaderboardService.OnSuccessGetEntry += FillPlayerInfo;
            LeaderboardService.GetPlayerEntry(Progress.Stats.CurrentLevelStats.Scene.GetLeaderBoardName());
        }

        private void FillPlayerInfo(LeaderboardEntryResponse response)
        {
            _rankText.text = $"#{response.rank}";
            StartCoroutine(LoadAvatar(response.player.scopePermissions.avatar, _iconImage, _playerContainer));
            _nameText.text = response.player.publicName;
            _scoreText.text = response.score.ToString();
            LeaderboardService.OnSuccessGetEntry -= FillPlayerInfo;
        }

        private void FillLeaderBoard(LeaderboardGetEntriesResponse leaderboardGetEntriesResponse)
        {
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

            LeaderboardService.OnSuccessGetEntries -= FillLeaderBoard;
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