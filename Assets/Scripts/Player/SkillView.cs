using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Assets.Scripts.Data.StaticData;
using UnityEngine.Events;
using System;

namespace Assets.Scripts.Player
{
    public class SkillView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _damageText;
        [SerializeField] private TMP_Text _AP;
        [SerializeField] private Image _skillIcon;       
        [SerializeField] private Image _full;
        [SerializeField] private Image _empty;
        [SerializeField] private Image[] _fullImages;
        [SerializeField] private Button _skillButton;

        public float _cooldown;
        public float _damage;
        public float _requiredAP;
        public SkillData _skillData;
        public float _turnsToRecover;
        private bool _isAvailable = true;
        public float Damage => _damage;
        public float RequiredAP => _requiredAP;
        public SkillData SkillData => _skillData;
        public Button SkillButton => _skillButton;

        public bool IsAvailable => _isAvailable;

        public event UnityAction<SkillView> SkillButtonPressed;

        private void OnEnable()
        {
            _skillButton.onClick.AddListener(OnButtonPress);
        }        

        public void Init(SkillData skillData)
        {
            _skillData = skillData;

            if (skillData.Type == SkillType.Defence)
            {
                _damage = skillData.Defence;
                _damageText.text = _damage.ToString();
                _cooldown = skillData.Cooldown;
                _requiredAP = skillData.RequiredAP;
                _AP.text = _requiredAP.ToString();
                _skillIcon.sprite = skillData.Icon;
            }
            else if (skillData.Type == SkillType.Move)
            {
                _requiredAP = skillData.RequiredAP;
                _AP.text = _requiredAP.ToString();
            }
            else
            {
                _damage = skillData.Damage;
                _damageText.text = _damage.ToString();
                _cooldown = skillData.Cooldown;
                _requiredAP = skillData.RequiredAP;
                _AP.text = _requiredAP.ToString();
                _skillIcon.sprite = skillData.Icon;
            }

            UpdateCooldown();                                             
        }        

        public void ApplyCooldown()
        {
            for (int i = 0; i < _cooldown; i++)
                _fullImages[i].gameObject.SetActive(false);
            SkillButton.interactable = false;
            _isAvailable = false;
        }

        public void RecoverCooldown()
        {
            _turnsToRecover+=1;
            if (_turnsToRecover <= _cooldown)
            {
                for (int i = 0; i < _turnsToRecover; i++)
                    _fullImages[i].gameObject.SetActive(true);

                if (_turnsToRecover == _cooldown)
                {
                    _isAvailable = true;
                    SkillButton.interactable = true;
                    _turnsToRecover = 0;
                }
            }                       
        }

        private void UpdateCooldown()
        {
            for (int i = 0; i < _cooldown; i++)
                _fullImages[i].gameObject.SetActive(true);
        }

        private void OnButtonPress()
        {
            SkillButtonPressed?.Invoke(this);
            //Debug.Log("Skill");
        }
    }
}
