﻿using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts.GameEnvironment.TreeHouse;

namespace Assets.Scripts.GameEnvironment.RoutEvents.EventWindows
{
    public class CooldownShrine : RoutEvent
    {
        [SerializeField] private Transform _shrineSlot;
        [SerializeField] private Image _plasticineIcon;
        [SerializeField] private Image _glueIcon;
        [SerializeField] private Image _screwIcon;
        [SerializeField] private Button _buffButton;
        [SerializeField] private TMP_Text _plasticineAmount;
        [SerializeField] private TMP_Text _glueAmount;
        [SerializeField] private TMP_Text _screwAmount;
        [SerializeField] private AudioSource _buffSound;

        private SkillView _choosedSkill;
        private ConnectingMaterial _currentMaterial;
        private ConnectingMaterial _previousMaterial;

        private void Awake()
        {
            _plasticineAmount.text = _plasticinePaymentAmount.ToString();
            _glueAmount.text = _gluePaymentAmount.ToString();
            _screwAmount.text = _screwPaymentAmount.ToString();
        }

        protected override void Start()
        {
            base.Start();
            _playerMoney.MaterialChoosed += GetPayment;
            _skillPanel.SkillChoosed += PrepareToBuff;
        }

        private void OnEnable()
        {
            _buffButton.onClick.AddListener(ApplyBuff);
            _close.onClick.AddListener(CloseEvent);
        }  

        protected override void GetPayment(ConnectingMaterial material)
        {
            if (_playerMoney.CurrentMaterialAmount >= GetPaymentValue(_playerMoney.CurrentMaterial))
            {
                if (_currentMaterial == material) return;
                if (_currentMaterial == null)
                {
                    _currentMaterial = material;
                    ChangeIcon(_currentMaterial);
                    _playerMoney.RemoveMaterialByType(_currentMaterial.Data.Type, GetPaymentValue(_currentMaterial));
                }
                else
                {
                    _previousMaterial = _currentMaterial;
                    _playerMoney.AddMaterialByType(_previousMaterial.Data.Type, GetPaymentValue(_previousMaterial));
                    _currentMaterial = material;
                    ChangeIcon(_currentMaterial);
                    _playerMoney.RemoveMaterialByType(_currentMaterial.Data.Type, GetPaymentValue(_currentMaterial));
                }
            }
            else
                _warning.Enable(_warning.NoMoney);
        }        

        protected override void PrepareToBuff(SkillView skill)
        {
            if (_currentMaterial == null)
            {
                _warning.Enable(_warning.NoMaterialChoosed);
                return;
            }

            if (_choosedSkill == null)
            {
                _choosedSkill = skill;
                _choosedSkill.transform.SetParent(_shrineSlot.transform);
                _choosedSkill.transform.position = _shrineSlot.position;
            }
            else
            {
                _nextSkill = skill;
                _skillPanel.TakeBack(_choosedSkill);
                _choosedSkill = _nextSkill;
                _choosedSkill.transform.SetParent(_shrineSlot.transform);
                _choosedSkill.transform.position = _shrineSlot.position;
            }
        }

        protected override void CloseEvent()
        {
            base.CloseEvent();
            if (_choosedSkill != null) _skillPanel.TakeBack(_choosedSkill);
            _buffButton.onClick.RemoveListener(ApplyBuff);
            _close.onClick.RemoveListener(CloseEvent);
            _playerMoney.MaterialChoosed -= GetPayment;
            _routMap.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }

        private void ApplyBuff()
        {
            if (_choosedSkill == null) return;
            _buffSound.Play();
            _choosedSkill.SkillData.Cooldown++;
            _choosedSkill.Init(_choosedSkill.SkillData);
            _skillPanel.TakeBack(_choosedSkill);
            _shrineSlot.gameObject.SetActive(false);
            _skillPanel.SkillChoosed -= PrepareToBuff;
            _choosedSkill = null;
            Invoke(nameof(CloseEvent), 1f);
        }                       

        private void ChangeIcon(ConnectingMaterial currentMaterial)
        {
            if (currentMaterial.Data.Type == MaterialType.Plasticine)
            {
                _plasticineIcon.gameObject.SetActive(true);
                _plasticineIcon.sprite = currentMaterial.Data.Icon;
                _glueIcon.gameObject.SetActive(false);
                _screwIcon.gameObject.SetActive(false);
            }
            else if (currentMaterial.Data.Type == MaterialType.Glue)
            {
                _glueIcon.gameObject.SetActive(true);
                _glueIcon.sprite = currentMaterial.Data.Icon;
                _plasticineIcon.gameObject.SetActive(false);
                _screwIcon.gameObject.SetActive(false);
            }
            else if (currentMaterial.Data.Type == MaterialType.Screw)
            {
                _screwIcon.gameObject.SetActive(true);
                _screwIcon.sprite = currentMaterial.Data.Icon;
                _plasticineIcon.gameObject.SetActive(false);
                _glueIcon.gameObject.SetActive(false);
            }
        }
    }
}
