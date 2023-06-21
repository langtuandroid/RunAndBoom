using System;
using Agava.YandexGames;
using CodeBase.UI.Windows.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Leaderboard
{
    public class LeaderboardWindow : WindowBase
    {
        [SerializeField] private Image _iconImage;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private GameObject _scrollView;
        [SerializeField] private GameObject _player;
        
        private void Start()
        {
        }
    }
}