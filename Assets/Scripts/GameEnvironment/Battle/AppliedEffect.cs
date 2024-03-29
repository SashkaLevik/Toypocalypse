using Assets.Scripts.Data.StaticData;
using Lean.Localization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GameEnvironment.Battle
{
    public class AppliedEffect : MonoBehaviour
    {
        [SerializeField] private TMP_Text _effectDescription;
        [SerializeField] private TMP_Text _skillValue;
        [SerializeField] private Image _effectImage;
        [SerializeField] private Image _descImage;

        private SkillData _skillData;

        public SkillData SkillEffect => _skillData;

        public void InitData(SkillData skillData)
        {
            _skillData = skillData;
            _effectImage.gameObject.SetActive(true);
            _effectImage.sprite = skillData.Icon;

            if (skillData.SkillType == SkillType.Attack)
                _skillValue.text = skillData.Damage.ToString();
            else if (skillData.SkillType == SkillType.Defence)
                _skillValue.text = skillData.Defence.ToString();
        }            

        public void ResetEffect()
        {
            _effectImage.gameObject.SetActive(false);
            _skillData = null;
        }

        public void OnEnter()
        {
            _descImage.gameObject.SetActive(true);
            _effectDescription.text = GetLocalizedDescription(_skillData);
        }

        public void OnExit()
            => _descImage.gameObject.SetActive(false);

        private string GetLocalizedDescription(SkillData skillData)
        {            
            if (Application.systemLanguage == SystemLanguage.Russian)
                return skillData.RuDescription;
            else
                return skillData.EnDescription;
        }
    }
}
