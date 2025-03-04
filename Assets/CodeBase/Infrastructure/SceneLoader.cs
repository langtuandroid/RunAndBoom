﻿using System;
using System.Collections;
using CodeBase.Data.Progress;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure
{
    public class SceneLoader : ISceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner) =>
            _coroutineRunner = coroutineRunner;

        public void Load(SceneId scene, Action<SceneId> onLoaded = null) =>
            _coroutineRunner.StartCoroutine(LoadScene(scene, onLoaded));

        public void Load(SceneId scene, Action onLoaded = null) =>
            _coroutineRunner.StartCoroutine(LoadScene(scene, onLoaded));

        private IEnumerator LoadScene(SceneId nextScene, Action<SceneId> onLoaded = null)
        {
            // if (SceneManager.GetActiveScene().name == nextScene)
            // {
            //     // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //     onLoaded?.Invoke(nextScene);
            //     yield break;
            // }

            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene.ToString());

            while (!waitNextScene.isDone)
                yield return null;

            onLoaded?.Invoke(nextScene);
        }

        private IEnumerator LoadScene(SceneId nextScene, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == nextScene.ToString())
            {
                onLoaded?.Invoke();
                yield break;
            }

            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene.ToString());

            while (!waitNextScene.isDone)
                yield return null;

            onLoaded?.Invoke();
        }
    }
}