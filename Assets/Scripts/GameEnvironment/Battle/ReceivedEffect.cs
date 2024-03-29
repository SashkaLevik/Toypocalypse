using Assets.Scripts.Data.StaticData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GameEnvironment.Battle
{
    public class ReceivedEffect : MonoBehaviour
    {
        [SerializeField] private Image _descriptionImage;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private TMP_Text _effectValue;
        [SerializeField] private Image _effectImage;

        private float _effectDuration;
        private bool _isActive;
        private SkillData _skillData;

        public float EffectDuration => _effectDuration;
        public SkillData SkillData => _skillData;

        public void InitData(SkillData skillData)
        {
            _skillData = skillData;
            _effectImage.gameObject.SetActive(true);
            _effectImage.sprite = skillData.EffectIcon;
            _effectDuration = _skillData.EffectValue;
            _effectValue.text = _effectDuration.ToString();
            _isActive = true;
        }            

        public bool IsActive()
        {
            return _isActive;
        }

        public void ReduceDuration()
        {
            _effectDuration -= 1;
            if (_effectDuration == 0)
                ResetEffect();
        }

        public void Stack(SkillData skillData)
        {
            _effectDuration += skillData.EffectValue;
            _effectValue.text = _effectDuration.ToString();
        }

        public void ResetEffect()
        {
            _isActive = false;
            _effectImage.gameObject.SetActive(false);
            _skillData = null;
        }

        public void OnEnter()
        {
            _descriptionImage.gameObject.SetActive(true);
            _description.text = GetLocalizedDescription(_skillData);
        }

        public void OnExit()
            => _descriptionImage.gameObject.SetActive(false);

        private string GetLocalizedDescription(SkillData skillData)
        {
            if (Application.systemLanguage == SystemLanguage.Russian)
                return skillData.RuEffectDescription;
            else
                return skillData.EnEffectDescription;
        }
    }
}
