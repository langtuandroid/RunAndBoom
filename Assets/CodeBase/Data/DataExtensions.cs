using UnityEngine;

namespace CodeBase.Data
{
    public static class DataExtensions
    {
        public static Vector3 FromScreenToWorld(this Vector3 position, Camera camera)
        {
            position.z = camera.nearClipPlane;
            return camera.ScreenToWorldPoint(position);
        }

        public static bool CompareByTag(this Collision other, string tag) =>
            other.gameObject.CompareTag(tag);

        public static bool CompareByTag(this Collider other, string tag) =>
            other.gameObject.CompareTag(tag);

        public static string ToJson(this object obj) =>
            JsonUtility.ToJson(obj);

        public static T ToDeserialized<T>(this string json) =>
            JsonUtility.FromJson<T>(json);
    }
}