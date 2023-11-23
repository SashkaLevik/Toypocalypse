using UnityEngine;

namespace Assets.Scripts.Data.StaticData
{
    [CreateAssetMenu(fileName = "MaterialData", menuName = "Materials")]
    public class MaterialData : ScriptableObject
    {
        public Sprite Icon;
        public string Name;
        public float Health;
        public MaterialType Type;
    }
}
