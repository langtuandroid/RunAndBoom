﻿using System.Collections;
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

            if (_authorization == null)
                _authorization = AllServices.Container.Single<IAuthorization>();

            if (AdsService != null)
            {
                AdsService.OnInitializeSuccess += AdsServiceInitializedSuccess;
                InitializeAdsSDK();
            }
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(Close);

            if (AdsService != null)
                AdsService.OnInitializeSuccess -= AdsServiceInitializedSuccess;
        }

        public void Construct(GameObject hero) =>
            base.Construct(hero, WindowId.LeaderBoard);

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

        private void Close() =>
            Hide();

        private void ClearLeaderBoard()
        {
            Debug.Log("ClearLeaderBoard");
            foreach (GameObject player in _players)
                player.SetActive(false);
        }

        protected override void AdsServiceInitializedSuccess()
        {
            Debug.Log("AdsServiceInitializedSuccess");
            LeaderBoardService.OnInitializeSuccess += TryAuthorize;

            if (LeaderBoardService.IsInitialized())
                TryAuthorize();
            else
                StartCoroutine(LeaderBoardService.Initialize());
        }

        private void TryAuthorize()
        {
            Debug.Log("TryAuthorize");
            LeaderBoardService.OnInitializeSuccess -= TryAuthorize;

            if (_authorization.IsAuthorized())
                RequestPersonalProfileDataPermission();
            else
                Authorize();
        }

        private void Authorize()
        {
            Debug.Log("Authorize");
            _authorization.OnAuthorizeSuccessCallback += RequestPersonalProfileDataPermission;
            _authorization.OnAuthorizeErrorCallback += ShowAuthorizeError;
            _authorization.Authorize();
        }

        private void RequestPersonalProfileDataPermission()
        {
            Debug.Log("RequestPersonalProfileDataPermission");
            _authorization.OnAuthorizeSuccessCallback -= RequestPersonalProfileDataPermission;
            _authorization.OnAuthorizeErrorCallback -= ShowAuthorizeError;
            _authorization.OnRequestErrorCallback += ShowRequestError;
            _authorization.OnRequestPersonalProfileDataPermissionSuccessCallback += RequestLeaderBoardData;
            _authorization.RequestPersonalProfileDataPermission();
        }

        private void ShowAuthorizeError(string error)
        {
            Debug.Log($"ShowAuthorizeError {error}");
            _authorization.OnAuthorizeErrorCallback -= ShowAuthorizeError;
        }

        private void ShowRequestError(string error)
        {
            Debug.Log($"ShowRequestError {error}");
            _authorization.OnRequestErrorCallback -= ShowRequestError;
            RequestLeaderBoardData();
        }

        private void ShowGetEntriesError(string error)
        {
            Debug.Log($"ShowGetEntriesError {error}");
            LeaderBoardService.OnGetEntriesError -= ShowGetEntriesError;
        }

        private void ShowGetEntryError(string error)
        {
            Debug.Log($"ShowGetEntryError {error}");
            LeaderBoardService.OnGetEntryError -= ShowGetEntryError;
        }

        private void RequestLeaderBoardData()
        {
            Debug.Log("RequestLeaderBoardData");
            _authorization.OnRequestPersonalProfileDataPermissionSuccessCallback -= RequestLeaderBoardData;
            _authorization.OnRequestErrorCallback -= ShowRequestError;
            LeaderBoardService.OnSuccessGetEntries += FillLeaderBoard;
            LeaderBoardService.OnSuccessGetEntry += FillPlayerInfo;
            Scene scene = Progress.AllStats.CurrentLevelStats.Scene;
            Debug.Log($"Scene {scene}");
            LeaderBoardService.OnGetEntriesError += ShowGetEntriesError;
            LeaderBoardService.OnGetEntryError += ShowGetEntryError;
            LeaderBoardService.GetEntries(scene.GetLeaderBoardName(Progress.IsHardMode));
            LeaderBoardService.GetPlayerEntry(scene.GetLeaderBoardName(Progress.IsHardMode));
        }

        private void FillLeaderBoard(LeaderboardGetEntriesResponse leaderboardGetEntriesResponse)
        {
            Debug.Log("FillLeaderBoard");
            LeaderboardEntryResponse[] leaderboardEntryResponses = leaderboardGetEntriesResponse.entries;
            Debug.Log($"leaderboardEntryResponses {leaderboardEntryResponses.Length}");
            LeaderboardEntryResponse response;
            PlayerItem playerItem;

            for (int i = 0; i < leaderboardEntryResponses.Length; i++)
            {
                if (i >= _players.Length)
                    return;

                response = leaderboardEntryResponses[i];
                playerItem = _players[i].GetComponent<PlayerItem>();
                playerItem.Rank.text = response.rank.ToString();

                if (!Application.isEditor)
                    StartCoroutine(LoadAvatar(response.player.scopePermissions.avatar, playerItem.Icon));

                playerItem.Name.text = response.player.publicName;
                playerItem.Score.text = response.score.ToString();
                playerItem.gameObject.SetActive(true);
                Debug.Log($"i {i}");
                Debug.Log($"publicName {response.player.publicName}");
                Debug.Log($"score {response.score}");
                Debug.Log($"rank {response.rank}");
            }

            if (LeaderBoardService != null)
                LeaderBoardService.OnSuccessGetEntries -= FillLeaderBoard;
        }

        private void FillPlayerInfo(LeaderboardEntryResponse response)
        {
            Debug.Log("FillPlayerInfo");
            if (!Application.isEditor)
                StartCoroutine(LoadAvatar(response.player.scopePermissions.avatar, _iconImage));

            _rankText.text = $"#{response.rank}";
            _nameText.text = response.player.publicName;
            _scoreText.text = response.score.ToString();

            if (LeaderBoardService != null)
                LeaderBoardService.OnSuccessGetEntry -= FillPlayerInfo;

            if (!string.IsNullOrEmpty(response.player.publicName))
                _playerDataContainer.SetActive(true);
            Debug.Log($"publicName {response.player.publicName}");
            Debug.Log($"score {response.score}");
            Debug.Log($"rank {response.rank}");
        }

        private IEnumerator LoadAvatar(string avatarUrl, RawImage image)
        {
            Debug.Log("LoadAvatar");
            image.gameObject.SetActive(false);
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(avatarUrl);
            yield return request.SendWebRequest();

            if (request.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log($"LoadAvatar {request.error}");
            }
            else
            {
                image.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
                image.gameObject.SetActive(true);
            }
        }
    }
}