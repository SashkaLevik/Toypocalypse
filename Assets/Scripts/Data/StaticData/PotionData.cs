using Assets.Scripts.GameEnvironment.TreeHouse;
using UnityEngine;

namespace Assets.Scripts.Data.StaticData
{
    [CreateAssetMenu(fileName = "PotionData", menuName = "PotionData")]
    public class PotionData : ScriptableObject
    {
        public Sprite Icon;
        public float ApplyedValue;
        public int Price;
        public PotionType PotionType;
        public ConnectingMaterial Material;
        public string RuDescription;
        public string EnDescription;
    }
}
