using UnityEngine;

namespace Assets.Scripts.Data.StaticData
{
    [CreateAssetMenu(fileName = "SkillData", menuName = "PartSkillData")]
    public class SkillData : ScriptableObject
    {
        public Sprite Icon;
        public string SkillDescription;
        public float Damage;
        public float Defence;
        public float Cooldown;
        public float RequiredAP;
        public SkillType Type;
    }    
}
