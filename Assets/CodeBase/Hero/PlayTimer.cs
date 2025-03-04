﻿using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Hero
{
    public class PlayTimer : MonoBehaviour
    {
        private IPlayerProgressService _progressService;
        private bool _isPlaying;
        private float _playTime;

        private void Start() =>
            _progressService = AllServices.Container.Single<IPlayerProgressService>();

        private void OnEnable() =>
            _isPlaying = true;

        private void Update()
        {
            if (_isPlaying)
                _playTime += Time.deltaTime;
        }

        private void OnDisable()
        {
            _progressService.ProgressData.AllStats.CurrentLevelStats.PlayTimeData.Add(_playTime);
            _isPlaying = false;
            _playTime = Constants.Zero;
        }
    }
}