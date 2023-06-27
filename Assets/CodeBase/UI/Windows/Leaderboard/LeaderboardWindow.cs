using System.Collections;
using Agava.YandexGames;
using CodeBase.Services;
using CodeBase.Services.Ads;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Leaderboard
{
    public class LeaderboardWindow : WindowBase
    {
        [SerializeField] private TextMeshProUGUI _rankText;
        [SerializeField] private RawImage _iconImage;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private GameObject _leaderBoard;
        [SerializeField] private GameObject _playerPrefab;

        private IAdsService _adsService;

        private void Awake()
        {
            _leaderBoard.SetActive(false);
            _iconImage.texture = null;
            _nameText.text = "";
            _scoreText.text = "";
        }

        private void Start()
        {
            ClearLeaderBoard();
            _adsService = AllServices.Container.Single<IAdsService>();
            StartCoroutine(InitializeYandexSDK());
        }

        private void OnEnable()
        {
            _adsService.OnInitializeSuccess += RequestLeaderBoardData;
        }

        private void OnDisable()
        {
            _adsService.OnInitializeSuccess -= RequestLeaderBoardData;
        }

        public void Construct(GameObject hero) =>
            base.Construct(hero, WindowId.LeaderBoard);

        private IEnumerator InitializeYandexSDK()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            yield break;
#endif
            yield return YandexGamesSdk.Initialize();
        }

        private void RequestLeaderBoardData()
        {
            _adsService.OnSuccessGetEntries += FillLeaderBoard;
            _adsService.GetEntries();

            _adsService.OnSuccessGetEntry += FillPlayerInfo;
            _adsService.GetPlayerEntry();
        }

        private void FillPlayerInfo(LeaderboardEntryResponse response)
        {
            _rankText.text = response.rank.ToString();
            StartCoroutine(LoadAvatar(response.player.scopePermissions.avatar, _iconImage));
            _nameText.text = response.player.publicName;
            _scoreText.text = response.score.ToString();
            _adsService.OnSuccessGetEntry -= FillPlayerInfo;
        }

        private void FillLeaderBoard(LeaderboardGetEntriesResponse leaderboardGetEntriesResponse)
        {
            LeaderboardEntryResponse[] leaderboardEntryResponses = leaderboardGetEntriesResponse.entries;

            foreach (var response in leaderboardEntryResponses)
            {
                var player = Instantiate(_playerPrefab, _leaderBoard.transform);
                player.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                    response.rank.ToString();
                RawImage image = player.transform.GetChild(1).GetComponent<RawImage>();
                StartCoroutine(LoadAvatar(response.player.scopePermissions.avatar, image));
                player.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text =
                    response.player.publicName;
                player.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text =
                    response.score.ToString();
            }

            _leaderBoard.SetActive(true);
            _adsService.OnSuccessGetEntries -= FillLeaderBoard;
        }

        private void ClearLeaderBoard()
        {
            if (_leaderBoard.transform.childCount > 0)
            {
                foreach (Transform child in _leaderBoard.transform)
                    Destroy(child.gameObject);
            }
        }

        private IEnumerator LoadAvatar(string avatarUrl, RawImage image)
        {
            WWW www = new WWW(avatarUrl);
            yield return null;
            image.texture = www.texture;
        }
    }
}