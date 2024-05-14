using Assets.Scripts.Data.StaticData;
using Assets.Scripts.Player;
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
        //private bool _isActive;
        private SkillData _skillData;

        public float EffectDuration => _effectDuration;
        public SkillData SkillData => _skillData;

        public void InitData(SkillData skillData)
        {
            _skillData = skillData;
            _effectImage.gameObject.SetActive(true);
            _effectImage.sprite = skillData.EffectIcon;
            _effectDuration = _skillData.EffectValue;
            UpdateDuration();
            //_isActive = true;
        }                  

        public void Apply(IHealth health)
        {
            if (_skillData.AttackType == AttackType.Acid)
                health.BreakeDefence(_skillData.EffectValue);
            else if (_skillData.AttackType == AttackType.Burn)
                health.TakeDirectDamage(_skillData.EffectValue);            

            ReduceDuration();
        }

        public void ReduceDuration()
        {
            _effectDuration -= 1;
            UpdateDuration();

            if (_effectDuration == 0)
                ResetEffect();
        }

        public void Stack(SkillData skillData)
        {
            _effectDuration += skillData.EffectValue;
            UpdateDuration();
        }

        public void ResetEffect()
        {
            //_isActive = false;
            _effectImage.gameObject.SetActive(false);
            _skillData = null;
            _effectDuration = 0;
        }

        public void OnEnter()
        {
            _descriptionImage.gameObject.SetActive(true);
            _description.text = GetLocalizedDescription(_skillData);
        }

        public void OnExit()
            => _descriptionImage.gameObject.SetActive(false);

        private void UpdateDuration()
            => _effectValue.text = _effectDuration.ToString();

        private string GetLocalizedDescription(SkillData skillData)
        {
            if (Application.systemLanguage == SystemLanguage.Russian)
                return skillData.RuEffectDescription;
            else
                return skillData.EnEffectDescription;
        }
    }
}
