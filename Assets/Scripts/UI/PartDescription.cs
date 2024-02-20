using Assets.Scripts.Data.StaticData;
using Assets.Scripts.Player;
using Lean.Localization;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class PartDescription : MonoBehaviour
    {
        private const string Russian = "Russian";

        [SerializeField] private LeanLocalization _leanLocalization;
        [SerializeField] private TMP_Text _partName;
        [SerializeField] private TMP_Text _health;
        [SerializeField] private TMP_Text _speed;
        [SerializeField] private TMP_Text _material;
        [SerializeField] private SkillView _skillView;
        [SerializeField] private TMP_Text _skillDescription;

        private PartData _choosedPartData;
        public PartData ChoosedPart => _choosedPartData;

        public void SetValues(PartData partData)
        {
            _choosedPartData = partData;
            _partName.text = GetLocalizedName(partData);
            _health.text = partData.Health.ToString();
            _speed.text = partData.Speed.ToString();
            _material.text = partData.MaterialAmount.ToString();
            _skillDescription.text = GetLocalizedSkill(partData.SkillData);
            _skillView.Init(partData.SkillData);            
        }

        private string GetLocalizedName(PartData partData)
        {
            if (_leanLocalization.CurrentLanguage == Russian)
                return partData.NameRu;
            else
                return partData.NameEn;
        }

        private string GetLocalizedSkill(SkillData skillData)
        {
            if (_leanLocalization.CurrentLanguage == "Russian")
                return skillData.RuDescription;
            else
                return skillData.EnDescription;
        }
    }
}
