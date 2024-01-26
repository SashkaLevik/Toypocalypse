using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Data.StaticData
{
    [CreateAssetMenu(fileName = "PartData", menuName = "Parts")]
    public class PartData : ScriptableObject
    {
        public Sprite Icon;
        public Sprite SkillImage;
        public string Name;    
        public float Health;
        public float Speed;
        public int MaterialAmount;
        public SkillData SkillData;
        public PartType Type;
    }
}
