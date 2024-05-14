using UnityEngine;

namespace Assets.Scripts.Data.StaticData
{
    [CreateAssetMenu(fileName = "PartData", menuName = "PartData")]
    public class PartData : ScriptableObject
    {
        public Sprite Icon;
        public string NameEn;
        public string NameRu;
        public float Health;
        public float Speed;
        public int MaterialAmount;
        public SkillData SkillData;
        public PartType Type;
    }
}
