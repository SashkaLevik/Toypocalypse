using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Data.StaticData
{
    [CreateAssetMenu(fileName = "PartData", menuName = "Parts")]
    public class PartData : ScriptableObject
    {
        public Sprite Icon;
        public string Name;        
        public float Health;
        public float Speed;
        public float Damage;
        public float MaterialAmount;
        public PartType Type;
    }
}
