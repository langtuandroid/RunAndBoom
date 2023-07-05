using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Services.Ads;
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

        public static string GetLeaderBoardName(this Scene scene)
        {
            switch (scene)
            {
                case Scene.Level_1:
                    return LeaderboardsConstants.LeaderboardPlayersWhoPassedLevel1;
                case Scene.Level_2:
                    return LeaderboardsConstants.LeaderboardPlayersWhoPassedLevel2;
                case Scene.Level_3:
                    return LeaderboardsConstants.LeaderboardPlayersWhoPassedLevel3;
                case Scene.Level_4:
                    return LeaderboardsConstants.LeaderboardPlayersWhoPassedLevel4;
                case Scene.Level_5:
                    return LeaderboardsConstants.LeaderboardPlayersWhoPassedLevel5;
                default:
                    return LeaderboardsConstants.LeaderboardPlayersWhoPassedTheGame;
            }
        }
    }
}