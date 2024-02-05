using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Data.Progress;
using CodeBase.Logic.EnemySpawners;
using CodeBase.Services.Input;
using CodeBase.Services.LeaderBoard;
using UnityEngine;

namespace CodeBase.Data
{
    public static class DataExtensions
    {
        public static Vector3Data AsVectorData(this Vector3 vector) =>
            new Vector3Data(vector.x, vector.y, vector.z);

        public static Vector3 AsUnityVector(this Vector3Data vector3Data) =>
            new Vector3(vector3Data.X, vector3Data.Y, vector3Data.Z);

        public static Vector3 AddY(this Vector3 position, float y) =>
            new Vector3(position.x, position.y + y, position.z);

        public static bool CompareByTag(this Collision other, string tag) =>
            other.gameObject.CompareTag(tag);

        public static bool CompareByTag(this Collider other, string tag) =>
            other.gameObject.CompareTag(tag);

        public static string ToJson(this object obj) =>
            JsonUtility.ToJson(obj);

        public static T ToDeserialized<T>(this string json) =>
            JsonUtility.FromJson<T>(json);

        public static IEnumerable<T> GetValues<T>() =>
            Enum.GetValues(typeof(T)).Cast<T>();

        public static string GetLeaderBoardName(this SceneId sceneId, bool isAsianMode)
        {
            switch (sceneId)
            {
                case SceneId.Level_1:
                    if (isAsianMode)
                        return LeaderboardsConstants.LeaderboardLevel1AsianDifficulty;
                    else
                        return LeaderboardsConstants.LeaderboardLevel1StandardDifficulty;
                case SceneId.Level_2:
                    if (isAsianMode)
                        return LeaderboardsConstants.LeaderboardLevel2AsianDifficulty;
                    else
                        return LeaderboardsConstants.LeaderboardLevel2StandardDifficulty;
                case SceneId.Level_3:
                    if (isAsianMode)
                        return LeaderboardsConstants.LeaderboardLevel3AsianDifficulty;
                    else
                        return LeaderboardsConstants.LeaderboardLevel3StandardDifficulty;
                case SceneId.Level_4:
                    if (isAsianMode)
                        return LeaderboardsConstants.LeaderboardLevel4AsianDifficulty;
                    else
                        return LeaderboardsConstants.LeaderboardLevel4StandardDifficulty;
                case SceneId.Level_5:
                    if (isAsianMode)
                        return LeaderboardsConstants.LeaderboardLevel5AsianDifficulty;
                    else
                        return LeaderboardsConstants.LeaderboardLevel5StandardDifficulty;
                default:
                    if (isAsianMode)
                        return LeaderboardsConstants.LeaderboardAsianGameDifficulty;
                    else
                        return LeaderboardsConstants.LeaderboardStandardGameDifficulty;
            }
        }

        public static int GetCount(this IInputService inputService, int baseCount)
        {
            if (inputService is MobileInputService)
                return (int)(baseCount * Constants.MobileAmmoMultiplier);
            else
                return baseCount;
        }
    }
}