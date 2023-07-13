using System.Collections;
using Agava.YandexGames;
using CodeBase.Data;
using CodeBase.Services;
using CodeBase.Services.PlayerAuthorization;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows.Common;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.LeaderBoard
{
    public class LeaderBoardWindow : WindowBase
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private TextMeshProUGUI _rankText;
        [SerializeField] private RawImage _iconImage;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private GameObject[] _players;
        [SerializeField] private GameObject _playerDataContainer;

        private IAuthorization _authorization;
        private Scene _nextScene;
        private int _maxPrice;

        private void OnEnable()
        {
            ClearLeaderBoard();
            ClearPlayerData();
            _closeButton.onClick.AddListener(Close);

            if (Application.isEditor)
            {
                AddTestData();
                return;
            }

            if (AdsService != null)
            {
                AdsService.OnInitializeSuccess += AdsServiceInitializedSuccess;
                InitializeAdsSDK();
            }

            if (_authorization == null)
                _authorization = AllServices.Container.Single<IAuthorization>();

            _authorization.OnErrorCallback += ShowError;
        }

        private void ClearPlayerData()
        {
            _rankText.text = "";
            _iconImage.texture = null;
            _nameText.text = "";
            _scoreText.text = "";
            _playerDataContainer.SetActive(false);
        }

        private void AddTestData()
        {
            LeaderboardEntryResponse leaderboardEntryResponse1 = new LeaderboardEntryResponse();
            leaderboardEntryResponse1.rank = 2;
            leaderboardEntryResponse1.score = 500;
            PlayerAccountProfileDataResponse playerAccountProfileDataResponse1 = new PlayerAccountProfileDataResponse();
            playerAccountProfileDataResponse1.publicName = "KuKu";
            leaderboardEntryResponse1.player = playerAccountProfileDataResponse1;
            FillPlayerInfo(leaderboardEntryResponse1);

            LeaderboardEntryResponse leaderboardEntryResponse2 = new LeaderboardEntryResponse();
            leaderboardEntryResponse2.rank = 1;
            leaderboardEntryResponse2.score = 300;
            PlayerAccountProfileDataResponse playerAccountProfileDataResponse2 = new PlayerAccountProfileDataResponse();
            playerAccountProfileDataResponse2.publicName = "Lilo";
            leaderboardEntryResponse2.player = playerAccountProfileDataResponse2;

            LeaderboardGetEntriesResponse leaderboardGetEntriesResponse = new LeaderboardGetEntriesResponse();
            leaderboardGetEntriesResponse.entries = new[] { leaderboardEntryResponse1, leaderboardEntryResponse2 };
            FillLeaderBoard(leaderboardGetEntriesResponse);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(Close);

            if (AdsService != null)
                AdsService.OnInitializeSuccess -= AdsServiceInitializedSuccess;

            if (_authorization != null)
                _authorization.OnErrorCallback -= ShowError;
        }

        public void Construct(GameObject hero) =>
            base.Construct(hero, WindowId.LeaderBoard);

        private void Close() =>
            gameObject.SetActive(false);

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

        private void ClearLeaderBoard()
        {
            foreach (GameObject player in _players)
                player.SetActive(false);
        }

        private void FillLeaderBoard(LeaderboardGetEntriesResponse leaderboardGetEntriesResponse)
        {
            Debug.Log("FillLeaderBoard");
            LeaderboardEntryResponse[] leaderboardEntryResponses = leaderboardGetEntriesResponse.entries;
            Debug.Log($"entries count {leaderboardGetEntriesResponse.entries.Length}");
            LeaderboardEntryResponse response;
            PlayerItem playerItem;

            for (int i = 0; i < leaderboardEntryResponses.Length; i++)
            {
                if (i >= _players.Length)
                    return;

                response = leaderboardEntryResponses[i];
                playerItem = _players[i].GetComponent<PlayerItem>();
                Debug.Log($"FillLeaderBoard rank {response.rank}");
                Debug.Log($"FillLeaderBoard publicName {response.player.publicName}");
                Debug.Log($"FillLeaderBoard score {response.score}");
                playerItem.Rank.text = response.rank.ToString();

                if (!Application.isEditor)
                    StartCoroutine(LoadAvatar(response.player.scopePermissions.avatar, playerItem.Icon));

                playerItem.Name.text = response.player.publicName;
                playerItem.Score.text = response.score.ToString();
                playerItem.gameObject.SetActive(true);
            }

            LeaderBoardService.OnSuccessGetEntries -= FillLeaderBoard;
        }

        private void FillPlayerInfo(LeaderboardEntryResponse response)
        {
            Debug.Log("FillPlayerInfo");
            // Debug.Log($"FillPlayerInfo rank {response.rank}");
            // Debug.Log($"FillPlayerInfo publicName {playerPublicName}");
            // Debug.Log($"FillPlayerInfo score {response.score}");
            if (!Application.isEditor)
                StartCoroutine(LoadAvatar(response.player.scopePermissions.avatar, _iconImage));

            _rankText.text = $"#{response.rank}";
            _nameText.text = response.player.publicName;
            _scoreText.text = response.score.ToString();
            LeaderBoardService.OnSuccessGetEntry -= FillPlayerInfo;

            if (!string.IsNullOrEmpty(response.player.publicName))
                _playerDataContainer.SetActive(true);
        }

        private IEnumerator LoadAvatar(string avatarUrl, RawImage image)
        {
            Debug.Log("LoadAvatar started");
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(avatarUrl);
            yield return request.SendWebRequest();

            if (request.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError)
                Debug.Log($"LoadAvatar {request.error}");
            else
                image.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;

            Debug.Log("LoadAvatar finished");
        }
    }
}