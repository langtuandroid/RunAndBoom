using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Data.Progress;
using CodeBase.Services.LeaderBoard;
using UnityEngine;

namespace CodeBase.Data
{
    public static class DataExtensions
    {
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

        public static string GetLeaderBoardName(this SceneId sceneId, bool isHardMode)
        {
            switch (sceneId)
            {
                case SceneId.Level_1:
                    if (isHardMode)
                        return LeaderboardsConstants.LeaderboardPlayersWhoPassedLevel1Hard;
                    else
                        return LeaderboardsConstants.LeaderboardPlayersWhoPassedLevel1;
                case SceneId.Level_2:
                    if (isHardMode)
                        return LeaderboardsConstants.LeaderboardPlayersWhoPassedLevel2Hard;
                    else
                        return LeaderboardsConstants.LeaderboardPlayersWhoPassedLevel2;
                case SceneId.Level_3:
                    if (isHardMode)
                        return LeaderboardsConstants.LeaderboardPlayersWhoPassedLevel3Hard;
                    else
                        return LeaderboardsConstants.LeaderboardPlayersWhoPassedLevel3;
                case SceneId.Level_4:
                    if (isHardMode)
                        return LeaderboardsConstants.LeaderboardPlayersWhoPassedLevel4Hard;
                    else
                        return LeaderboardsConstants.LeaderboardPlayersWhoPassedLevel4;
                case SceneId.Level_5:
                    if (isHardMode)
                        return LeaderboardsConstants.LeaderboardPlayersWhoPassedLevel5Hard;
                    else
                        return LeaderboardsConstants.LeaderboardPlayersWhoPassedLevel5;
                default:
                    if (isHardMode)
                        return LeaderboardsConstants.LeaderboardPlayersWhoPassedHardGameMode;
                    else
                        return LeaderboardsConstants.LeaderboardPlayersWhoPassedTheGame;
            }
        }
    }
}