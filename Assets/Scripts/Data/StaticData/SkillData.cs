using UnityEngine;

namespace Assets.Scripts.Data.StaticData
{
    [CreateAssetMenu(fileName = "SkillData", menuName = "PartSkillData")]
    public class SkillData : ScriptableObject
    {
        public Sprite Icon;
        public string SkillDescription;
        public float DefaultDamage;
        public float Damage;
        public float Defence;
        public float DefaultCooldown;
        public float Cooldown;
        public float RequiredAP;
        public SkillType SkillType;
        public AttackType AttackType;

        public void ResetSkill()
        {
            Damage = DefaultDamage;
            Cooldown = DefaultCooldown;
        }
    }    
}
