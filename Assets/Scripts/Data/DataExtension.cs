
using UnityEngine;

namespace Assets.Scripts.Data
{
    public static class DataExtension
    {
        public static string ToJson(this object obj) =>
            JsonUtility.ToJson(obj);

        public static T ToDeserialized<T>(this string json) =>
            JsonUtility.FromJson<T>(json);
    }
}