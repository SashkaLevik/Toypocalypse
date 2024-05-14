using Assets.Scripts.GameEnvironment.Dice;
using UnityEngine;

namespace Assets.Scripts.Data.StaticData
{
    [CreateAssetMenu(fileName = "SkillData", menuName = "PartSkillData")]
    public class SkillData : ScriptableObject
    {
        public Sprite Icon;
        public Sprite EffectIcon;
        public string RuDescription;
        public string EnDescription;
        public string RuEffectDescription;
        public string EnEffectDescription;
        public int EffectChance;
        public float EffectValue;
        public float EffectDuration;
        public float DefaultDamage;
        public float Damage;
        public float DefaultDefence;
        public float Defence;
        public float DefaultCooldown;
        public float Cooldown;
        public float RequiredAP;
        public int StunChance;
        public PartType PartType;
        public SkillType SkillType;
        public AttackType AttackType;
        public AreaDice AreaDice;

        public void ResetSkill()
        {
            Damage = DefaultDamage;
            Cooldown = DefaultCooldown;
            Defence = DefaultDefence;
        }
    }    
}
