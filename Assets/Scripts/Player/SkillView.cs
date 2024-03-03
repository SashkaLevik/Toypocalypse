﻿using Assets.Scripts.Data.StaticData;
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
        private SkillData _skillData;
        private float _defaultDamage;
        private float _defaultDefence;

        public float Damage => _damage;
        public float Defence => _defence;
        public float RequiredAP => _requiredAP;
        public SkillData SkillData => _skillData;
        public Button SkillButton => _skillButton;

        public event UnityAction<SkillView> SkillButtonPressed;                   

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
                UpdateParameters(_skillData, _defence);                
            }            
            else if (_skillData.SkillType == SkillType.Attack)
            {
                _damage = _skillData.Damage;
                _defaultDamage = _skillData.Damage;
                UpdateParameters(_skillData, _damage);
            }

            UpdateCooldown();                                             
        }       

        public void ChangeOnArea(AreaType areaType)
        {
            if (areaType == AreaType.Attack)
            {                
                if (_skillData.SkillType == SkillType.Attack)
                {
                    _damage += 2;
                    _damageText.color = Color.green;
                    UpdateBattleValue(_damage);
                }
                else if (_skillData.SkillType == SkillType.Defence)
                {
                    _defence -= 1;
                    _damageText.color = Color.red;
                    UpdateBattleValue(_defence);
                }
            }
            else if (areaType == AreaType.Defence)
            {
                if (_skillData.SkillType == SkillType.Defence)
                {
                    _defence += 2;
                    _damageText.color = Color.green;
                    UpdateBattleValue(_defence);
                }
                else if (_skillData.SkillType == SkillType.Attack)
                {
                    _damage -= 1;
                    _damageText.color = Color.red;
                    UpdateBattleValue(_damage);
                }
            }
            else if (areaType == AreaType.Common)
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
        }

        public void IncreaseDamage()
        {

        }

        public void IncreaseDefence()
        {

        }

        public void DecreaseDamage()
        {
            _damage--;
        }

        public void DecreaseDefence()
        {
            _defence--;
        }

        public void ShowOnLeftButton(AreaType areaType)
        {            
            if (areaType == AreaType.Common)
            {
                float damage = _defaultDamage;
                float defence = _defaultDefence;

                if (_skillData.SkillType == SkillType.Attack)
                {
                    _damageText.color = Color.red;
                    _damageText.text = (damage - 1).ToString();
                }
                else if (_skillData.SkillType == SkillType.Defence)
                {
                    _damageText.color = Color.green;
                    _damageText.text = (defence + 2).ToString();
                }
            }
            else if (areaType == AreaType.Attack)
            {
                if (_skillData.SkillType == SkillType.Attack)
                {
                    _damageText.color = Color.white;
                    _damageText.text = _defaultDamage.ToString();
                }
                else if (_skillData.SkillType == SkillType.Defence)
                {
                    _damageText.color = Color.white;
                    _damageText.text = _defaultDefence.ToString();
                }
            }
        }

        public void ShowOnRightButton(AreaType areaType)
        {
            if (areaType == AreaType.Common)
            {
                float damage = _defaultDamage;
                float defence = _defaultDefence;

                if (_skillData.SkillType == SkillType.Attack)
                {
                    _damageText.color = Color.green;
                    _damageText.text = (damage + 2).ToString();
                }
                else if (_skillData.SkillType == SkillType.Defence)
                {
                    _damageText.color = Color.red;
                    _damageText.text = (defence - 1).ToString();
                }
            }
            else if (areaType == AreaType.Defence)
            {
                if (_skillData.SkillType == SkillType.Attack)
                {
                    _damageText.color = Color.white;
                    _damageText.text = _defaultDamage.ToString();
                }
                else if (_skillData.SkillType == SkillType.Defence)
                {
                    _damageText.color = Color.white;
                    _damageText.text = _defaultDefence.ToString();
                }
            }
        }

        public void SetDefaultLeft(AreaType areaType)
        {
            if (areaType == AreaType.Common)
            {                
                if (_skillData.SkillType == SkillType.Attack)
                {
                    _damageText.color = Color.white;
                    _damageText.text = _defaultDamage.ToString();
                }
                else if (_skillData.SkillType == SkillType.Defence)
                {
                    _damageText.color = Color.white;
                    _damageText.text = _defaultDefence.ToString();
                }
            }
            else if (areaType == AreaType.Attack)
            {                
                if (_skillData.SkillType == SkillType.Attack)
                {
                    _damageText.color = Color.green;
                    _damageText.text = (_damage).ToString();
                }
                else if (_skillData.SkillType == SkillType.Defence)
                {
                    _damageText.color = Color.red;
                    _damageText.text = (_defence).ToString();
                }
            }            
        }

        public void SetDefaultRight(AreaType areaType)
        {
            if (areaType == AreaType.Common)
            {
                if (_skillData.SkillType == SkillType.Attack)
                {
                    _damageText.color = Color.white;
                    _damageText.text = _defaultDamage.ToString();
                }
                else if (_skillData.SkillType == SkillType.Defence)
                {
                    _damageText.color = Color.white;
                    _damageText.text = _defaultDefence.ToString();
                }
            }
            else if (areaType == AreaType.Defence)
            {                
                if (_skillData.SkillType == SkillType.Attack)
                {
                    _damageText.color = Color.red;
                    _damageText.text = (_damage).ToString();
                }
                else if (_skillData.SkillType == SkillType.Defence)
                {
                    _damageText.color = Color.green;
                    _damageText.text = (_defence).ToString();
                }
            }
        }

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

        private void UpdateBattleValue(float value)
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
