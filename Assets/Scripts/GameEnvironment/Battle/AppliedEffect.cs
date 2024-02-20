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
        [SerializeField] private Image _effectImage;

        private SkillData _skillEffect;

        public SkillData SkillEffect => _skillEffect;

        public void InitData(SkillData skillData)
        {
            _skillEffect = skillData;
            _effectImage.gameObject.SetActive(true);
            _effectImage.sprite = skillData.Icon;
        }            

        public void ResetEffect()
        {
            _effectImage.gameObject.SetActive(false);
            _skillEffect = null;
        }

        public void OnEnter()
        {
            _effectDescription.gameObject.SetActive(true);
            _effectDescription.text = GetLocalizedDescription(_skillEffect);
        }

        public void OnExit()
            => _effectDescription.gameObject.SetActive(false);

        private string GetLocalizedDescription(SkillData skillData)
        {            
            if (Application.systemLanguage == SystemLanguage.Russian)
                return skillData.RuDescription;
            else
                return skillData.EnDescription;
        }
    }
}
