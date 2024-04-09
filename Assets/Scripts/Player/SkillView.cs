using Assets.Scripts.Data.StaticData;
using Assets.Scripts.GameEnvironment.Battle;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.Player
{
    public class SkillView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _damageText;
        [SerializeField] private TMP_Text _AP;
        [SerializeField] private Image _skillIcon;       
        [SerializeField] private Image[] _fullImages;
        [SerializeField] private Image[] _emptyImages;
        [SerializeField] private Button _skillButton;
        [SerializeField] private GameObject _imageContainer;
        [SerializeField] private Image _descriptionImage;
        [SerializeField] private TMP_Text _description;

        private float _cooldown;
        private float _damage;
        private float _defence;
        private float _requiredAP;
        private int _usePerTurn;
        private float _defaultDamage;
        private float _defaultDefence;
        private SkillData _skillData;
        private Area _currentArea;

        public float Damage => _damage;
        public float Defence => _defence;
        public float RequiredAP => _requiredAP;
        public SkillData SkillData => _skillData;
        public Button SkillButton => _skillButton;

        public event UnityAction<SkillView> SkillButtonPressed;
        public event UnityAction<float> DamageChanged;
        public event UnityAction<float> DefenceChanged;

        private void OnEnable()
            => _skillButton.onClick.AddListener(OnButtonPress);

        private void OnDestroy()
        {
            _skillButton.onClick.RemoveListener(OnButtonPress);
        }                    

        public void OnEnter()
        {
            _descriptionImage.gameObject.SetActive(true);
            _description.text = GetLocalizedDescription();
        }

        public void OnExit()
            => _descriptionImage.gameObject.SetActive(false);

        public void Init(SkillData skillData)
        {
            _skillData = skillData;

            if (_skillData.SkillType == SkillType.Defence)
            {
                _defence = _skillData.Defence;
                _defaultDefence = _skillData.Defence;
                DefenceChanged?.Invoke(_defence);
                UpdateParameters(_skillData, _defence);                
            }            
            else if (_skillData.SkillType == SkillType.Attack)
            {
                _damage = _skillData.Damage;
                _defaultDamage = _skillData.Damage;
                DamageChanged?.Invoke(_damage);
                UpdateParameters(_skillData, _damage);
            }

            UpdateCooldown();                                             
        }       

        public void ChangeOnArea(Area area)
        {
            if (_currentArea == area) return;
            else
                ChangeOnCommon();

            _currentArea = area;

            if (_currentArea.AreaType == AreaType.Attack)
                ChangeOnAttack(_currentArea);
            else if (_currentArea.AreaType == AreaType.Defence)
            {
                ChangeOnDefence(_currentArea);
            }
            else if (_currentArea.AreaType == AreaType.Common)
            {
                ChangeOnCommon();
            }
        }                

        private void ChangeOnAttack(Area area)
        {
            if (_skillData.SkillType == SkillType.Attack)
            {
                _damage += area.IncreasedValue;
                _damageText.color = Color.green;
                UpdateBattleValue(_damage);
            }
            else if (_skillData.SkillType == SkillType.Defence)
            {
                _defence -= area.DecreasedValue;
                _damageText.color = Color.red;

                UpdateBattleValue(_defence);
            }
        }

        private void ChangeOnDefence(Area area)
        {
            if (_skillData.SkillType == SkillType.Defence)
            {
                _defence += area.IncreasedValue;
                _damageText.color = Color.green;
                UpdateBattleValue(_defence);
            }
            else if (_skillData.SkillType == SkillType.Attack)
            {
                _damage -= area.DecreasedValue;
                _damageText.color = Color.red;                
                UpdateBattleValue(_damage);
            }
        }

        private void ChangeOnCommon()
        {
            if (_skillData.SkillType == SkillType.Defence)
            {
                _defence = _skillData.Defence;
                UpdateBattleValue(_defence);
            }
            else if (_skillData.SkillType == SkillType.Attack)
            {
                _damage = _skillData.Damage;
                UpdateBattleValue(_damage);
            }
            _damageText.color = Color.white;
        }

        public void Activate()
        {
            if(_usePerTurn != 0)
                _skillButton.interactable = true;
        }

        public void Disactivate()
            => _skillButton.interactable = false;

        public void DecreaseCooldown()
        {
            for (int i = 0; i < _usePerTurn; i++)
            {
                _fullImages[_usePerTurn-1].gameObject.SetActive(false);
                _emptyImages[_usePerTurn-1].gameObject.SetActive(true);
            }

            _usePerTurn--;

            if (_usePerTurn == 0) _skillButton.interactable = false;
        }             

        public void ResetCooldown()
        {
            SkillButton.interactable = true;
            _usePerTurn = (int)_cooldown;
            UpdateCooldown();
        }

        public void UpdateCooldown()
        {
            for (int i = 0; i < _emptyImages.Length; i++)
                _emptyImages[i].gameObject.SetActive(false);

            for (int i = 0; i < _fullImages.Length; i++)
                _fullImages[i].gameObject.SetActive(false);

            for (int i = 0; i < SkillData.Cooldown; i++)
                _fullImages[i].gameObject.SetActive(true);
        }

        public void SetToDefault()
            => _skillData.ResetSkill();

        public void UpdateBattleValue(float value)
            => _damageText.text = value.ToString();        

        public void UpdateParameters(SkillData skillData, float value)
        {
            _damageText.text = value.ToString();
            _cooldown = skillData.Cooldown;
            _usePerTurn = (int)_cooldown;
            _requiredAP = skillData.RequiredAP;
            _AP.text = _requiredAP.ToString();
            _skillIcon.sprite = skillData.Icon;
        }        

        private void OnButtonPress()
            => SkillButtonPressed?.Invoke(this);

        private string GetLocalizedDescription()
        {
            if (Application.systemLanguage == SystemLanguage.Russian)
                return _skillData.RuDescription;
            else
                return _skillData.EnDescription;
        }
    }
}
